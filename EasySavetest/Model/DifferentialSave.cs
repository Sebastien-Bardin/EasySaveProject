using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace EasySavetest.Model
{
    class DifferentialSave : ISave
    {
        //Objects for logs infomations
        public static SaveStat differential;
        public static JsonLog JsonLogD;
        //Array used to store extention that need to be encrypted
        public static string[] Exts;
        //List of running tasks 
        public static Tasks RunningTasks = new Tasks() { AllTasks = new List<Task>() };
        //mutex used for thread synchronisation 
        public static Mutex mutex = new Mutex();

        //method creating save's thread 
        public void save(Work work, ManualResetEvent ProcessVerif)
        {

            work.extentions.CopyTo((Exts = new string[work.extentions.Length]), 0);
            work.informations.thread = new Thread(new ThreadStart(() => { saveM(work, ProcessVerif); }));
            work.informations.thread.Start();
            work.informations.RunningState = new ManualResetEvent(true);
            work.informations.IsRunning = true;
            RunningTasks.AllTasks.Add(work.informations);

        }
        //method launching all necessary methods 
        public void saveM(Work work, ManualResetEvent ProcessVerif)
        {
            mutex.WaitOne();
            //Object that allow to get the values for the Log files
            differential = new SaveStat();
            JsonLogD = new JsonLog();
            PathAsker(work);
            FileDetector();
            FileCopier(ProcessVerif, work,true);
            FileCopier(ProcessVerif, work, false);
            JsonLogD.CreateLog(work, differential);
            mutex.ReleaseMutex();

        }

        //method putting on pause specified task or replaying them if they are already paused 
        public void PlayPause(string nom)
        {
            foreach (Task task in RunningTasks.AllTasks)
            {
                if (task.Name == nom && task.IsRunning == true)
                {
                    task.RunningState.Reset();
                    task.IsRunning = false;
                    Console.WriteLine("je m'arrete");
                }
                else if (task.IsRunning == false)
                {
                    task.IsRunning = true;
                    task.RunningState.Set();

                }
            }


        }
        //method canceling he specified task 
        public void Cancel(String name)
        {
            foreach (Task task in RunningTasks.AllTasks)
            {
                if (task.Name == name && task.thread.IsAlive == true)
                {
                    task.thread.Abort();
                }
            }
        }

        //Get paths from JsonFile Array
        public void PathAsker(Work work)
        {
            Console.WriteLine("Getting source path...");
            differential.source = work.informations.Source;
            Console.WriteLine(differential.source);
            Console.WriteLine("Getting destination");
            differential.destination = work.informations.Destination;
            Console.WriteLine(differential.destination);
        }


        //Listing and counting files in the source folder
        void FileDetector()
        {
            DirectoryInfo path = new DirectoryInfo(differential.source);
            FileInfo[] files = path.GetFiles();
            Console.WriteLine("The following files have been found:");
            foreach (string file in System.IO.Directory.GetFiles(differential.source, "*", System.IO.SearchOption.AllDirectories))
            {
                Console.WriteLine("found " + file);
                differential.filecount++;
                //Create a FileInfo object "f" with its directory to get the f.Length object's size (in bytes)
                FileInfo f = new FileInfo(file);
                differential.totalsize += f.Length;
            }
            Console.WriteLine();
            Console.WriteLine("File number: " + differential.filecount);
            Console.WriteLine("Files total size: " + differential.totalsize + "bytes");
            Console.WriteLine();
        }

        void FileCopier(ManualResetEvent ProcessVerif, Work work, bool priorities)
        {
            differential.remainingfiles = differential.filecount;
            differential.remainingsize = differential.totalsize;
            DirectoryInfo path = new DirectoryInfo(differential.source);
            FileInfo[] files = path.GetFiles();
            //Observer observable implementation 
            ProgressBarReporter reporter = new ProgressBarReporter();
            ProgressBarTracker provider = new ProgressBarTracker();
            reporter.Subscribe(provider);
            Console.WriteLine("Starting process...");

            //Initializating the timer and starting it
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Create subdirectory structure in destination    
            foreach (string direction in System.IO.Directory.GetDirectories(differential.source, "*", System.IO.SearchOption.AllDirectories))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(differential.destination, direction.Substring(differential.source.Length - 5)));
            }

            // Copy each file in its final direction
            foreach (string file in System.IO.Directory.GetFiles(differential.source, "*", System.IO.SearchOption.AllDirectories))
            {
                ProcessVerif.WaitOne();
                Console.WriteLine("copying " + file);
                differential.currentfiletocopy = file;

                //Create a FileInfo object "f" with its directory to get the f.Length object's size (in bytes)
                FileInfo fs = new FileInfo(file);
                string dfile = differential.destination + "/" + file.Remove(0, differential.source.Length);
                FileInfo fd = new FileInfo(dfile);
                //We first save file with a priority 
                if (priorities == true)
                {
                    foreach (string prio in work.priorities)
                    {

                        if (prio == (Path.Combine(differential.destination, file.Substring(differential.destination.Length - 5)).Split('.')[1]))
                        {

                            if (fs.LastWriteTime > fd.LastWriteTime)
                            {
                                //Copy File
                                Console.WriteLine("Source file is more recent ");
                                System.IO.File.Copy(file, System.IO.Path.Combine(differential.destination, file.Substring(differential.destination.Length - 5)), true);
                                Console.WriteLine("Copy has been to be executed");
                                CryptFile(Path.Combine(differential.destination, file.Substring(differential.destination.Length - 5)));

                            }
                            else
                            {
                                //Tellthe user no copy is needed
                                Console.WriteLine(" Destination file is the same than source file, no copy executed");
                            }
                            differential.remainingfiles--;
                            differential.remainingsize -= fs.Length;
                            differential.time = stopwatch.Elapsed.Seconds;
                            differential.RefreshSaveStat(differential);
                            RunningTasks.AllTasks.Remove(work.informations);
                            work.informations.Progress = 100 - ((differential.remainingsize * 100) / differential.totalsize);
                            provider.TrackProgress(new TaskProgressBar(work.informations.Progress.ToString()));
                        }

                    }
                    provider.EndTransmission();
                }
                else //then we relaunch the method to save all other files 
                {

                    foreach (string prio in work.priorities)
                    {

                        if (prio == (Path.Combine(differential.destination, file.Substring(differential.destination.Length - 5)).Split('.')[1]))
                        {

                            if (fs.LastWriteTime > fd.LastWriteTime)
                            {
                                //Copy File
                                Console.WriteLine("Source file is more recent ");
                                System.IO.File.Copy(file, System.IO.Path.Combine(differential.destination, file.Substring(differential.destination.Length - 5)), true);
                                Console.WriteLine("Copy has been to be executed");
                                CryptFile(Path.Combine(differential.destination, file.Substring(differential.destination.Length - 5)));

                            }
                            else
                            {
                                //Tellthe user no copy is needed
                                Console.WriteLine(" Destination file is the same than source file, no copy executed");
                            }
                            differential.remainingfiles--;
                            differential.remainingsize -= fs.Length;
                            differential.time = stopwatch.Elapsed.Seconds;
                            differential.RefreshSaveStat(differential);
                            RunningTasks.AllTasks.Remove(work.informations);
                            //notifying observers 
                            work.informations.Progress = 100 - ((differential.remainingsize * 100) / differential.totalsize);
                            provider.TrackProgress(new TaskProgressBar(work.informations.Progress.ToString()));
                        }

                    }
                    provider.EndTransmission();

                }
              
            }


        }

        //method crypting files with a specific extention 
        void CryptFile(string file)
        {
            foreach (string ext in Exts)
            {

                if (file.Contains("." + ext))
                {

                    DateTime start = DateTime.Now;
                    Process Cryptage = new Process();
                    Cryptage.StartInfo.FileName = @"C:\Users\Louis\source\repos\CryptoSoft\CryptoSoft\bin\Debug\netcoreapp3.0\CryptoSoft.exe";
                    Cryptage.StartInfo.Arguments = file;
                    Cryptage.Start();
                    TimeSpan duration = DateTime.Now - start;
                    differential.CryptTime = duration;
                    Cryptage.Close();
                }
            }
        }
    }
}


