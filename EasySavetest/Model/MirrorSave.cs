using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace EasySavetest.Model
{
    class MirrorSave : ISave
    {
        //Objects used for logs
        public static SaveStat mirror;
        public static JsonLog JsonLogM;
        //Array use to store crypted extention
        public static string[] Exts;
        //List of running tasks
        public static Tasks RunningTasks = new Tasks() { AllTasks = new List<Task>() };

        //mutex used for thread synchronisation
        private static Mutex mutex = new Mutex();


        //method creating save thread
        public void save(Work work, ManualResetEvent ProcessVerif)
        {
            work.extentions.CopyTo((Exts = new string[work.extentions.Length]), 0);
            work.informations.thread = new Thread(new ThreadStart(() => { saveM(work, ProcessVerif); }));
            work.informations.thread.Start();
            work.informations.RunningState = new ManualResetEvent(true);
            work.informations.IsRunning = true;
            RunningTasks.AllTasks.Add(work.informations);
        }

        // method to put on pause specified task or replay them 
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
        //Cancel the specified task 
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

        //Launch all saves method
        public void saveM(Work work, ManualResetEvent ProcessVerif)
        {
            mutex.WaitOne();
            mirror = new SaveStat();
            JsonLogM = new JsonLog();
            PathAsker(work);
            FileDetector(work);
            FileCopier(ProcessVerif, work,true);
            FileCopier(ProcessVerif, work, false);
            JsonLogM.CreateLog(work, mirror);
            mutex.ReleaseMutex();
        }

        //Getting source and destination paths
        public void PathAsker(Work work)
        {
            Console.WriteLine("{0} is requesting the mutex", Thread.CurrentThread.Name);
            Console.WriteLine("{0} has entered the protected area", Thread.CurrentThread.Name);
            Console.WriteLine("Getting source path...");
            mirror.source = work.informations.Source;
            Console.WriteLine(mirror.source);
            Console.WriteLine("Getting destination");
            mirror.destination = work.informations.Destination;
            Console.WriteLine(mirror.destination);
            Console.WriteLine("{0} has released the mutex",
            Thread.CurrentThread.Name);
        }

        //Listing and counting files in the source folder
        void FileDetector(Work work)
        {

            DirectoryInfo path = new DirectoryInfo(mirror.source);
            FileInfo[] files = path.GetFiles();
            Console.WriteLine("The following files have been found:");
            foreach (string file in System.IO.Directory.GetFiles(mirror.source, "*", System.IO.SearchOption.AllDirectories))
            {
                Console.WriteLine("found " + file);
                mirror.filecount++;
                //Create a FileInfo object "f" with its directory to get the f.Length object's size (in bytes)
                FileInfo f = new FileInfo(file);
                mirror.totalsize += f.Length;
            }
            Console.WriteLine("File number: " + mirror.filecount);
            Console.WriteLine("Files total size: " + mirror.totalsize + "bytes");

        }

        //Copying files one by one from source to destination

        void FileCopier(ManualResetEvent ProcessVerif, Work work,bool priorities)
        {

            mirror.remainingfiles = mirror.filecount;
            mirror.remainingsize = mirror.totalsize;
            DirectoryInfo path = new DirectoryInfo(mirror.source);
            FileInfo[] files = path.GetFiles();
            Console.WriteLine("Starting process...");

            //Initializating the timer and starting it
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Implementing observer and observable
            ProgressBarReporter reporter = new ProgressBarReporter();
            ProgressBarTracker provider = new ProgressBarTracker();
            reporter.Subscribe(provider);

            // Create subdirectory structure in destination    
            foreach (string direction in System.IO.Directory.GetDirectories(mirror.source, "*", System.IO.SearchOption.AllDirectories))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(mirror.destination, direction.Substring(mirror.source.Length - 5)));
            }
            //We first save file with a priority
            if (priorities) {
                foreach (string file in System.IO.Directory.GetFiles(mirror.source, "*", System.IO.SearchOption.AllDirectories))
                {
                    if (work.informations.IsRunning == false)
                    {
                        work.informations.RunningState.WaitOne();
                    }

                    foreach (string prio in work.priorities)
                    {

                        if (prio == (Path.Combine(mirror.destination, file.Substring(mirror.destination.Length - 5)).Split('.')[1]))
                        {

                            ProcessVerif.WaitOne();
                            Console.WriteLine("copying " + file);
                            mirror.currentfiletocopy = file;
                            System.IO.File.Copy(file, System.IO.Path.Combine(mirror.destination, file.Substring(mirror.destination.Length - 5)), true);
                            mirror.remainingfiles--;
                            //Create a FileInfo object "f" with its directory to get the f.Length object's size (in bytes)
                            FileInfo f = new FileInfo(file);
                            work.informations.Progress = 100 - ((mirror.remainingsize * 100) / mirror.totalsize);
                            //notifying progress to the observer
                            provider.TrackProgress(new TaskProgressBar(work.informations.Progress.ToString()));
                            mirror.remainingsize -= f.Length;
                            mirror.time = stopwatch.Elapsed.Seconds;
                            CryptFile(Path.Combine(mirror.destination, file.Substring(mirror.destination.Length - 5)));
                            mirror.RefreshSaveStat(mirror);
                        }
                    }



                }
                provider.TrackProgress(new TaskProgressBar("100"));
                provider.EndTransmission();
            }
            //Then we save all other files 
            else
            {
                foreach (string file in System.IO.Directory.GetFiles(mirror.source, "*", System.IO.SearchOption.AllDirectories))
                {
                    if (work.informations.IsRunning == false)
                    {
                        work.informations.RunningState.WaitOne();
                    }

                    foreach (string prio in work.priorities)
                    {

                        if (prio != (Path.Combine(mirror.destination, file.Substring(mirror.destination.Length - 5)).Split('.')[1]))
                        {

                            ProcessVerif.WaitOne();
                            Console.WriteLine("copying " + file);
                            mirror.currentfiletocopy = file;
                            System.IO.File.Copy(file, System.IO.Path.Combine(mirror.destination, file.Substring(mirror.destination.Length - 5)), true);
                            mirror.remainingfiles--;
                            //Create a FileInfo object "f" with its directory to get the f.Length object's size (in bytes)
                            FileInfo f = new FileInfo(file);
                            work.informations.Progress = 100 - ((mirror.remainingsize * 100) / mirror.totalsize);
                            provider.TrackProgress(new TaskProgressBar(work.informations.Progress.ToString()));
                            mirror.remainingsize -= f.Length;
                            mirror.time = stopwatch.Elapsed.Seconds;
                            CryptFile(Path.Combine(mirror.destination, file.Substring(mirror.destination.Length - 5)));
                            mirror.RefreshSaveStat(mirror);
                        }
                    }



                }
                provider.TrackProgress(new TaskProgressBar("100"));
                provider.EndTransmission();
            }
            

        }

       

        //method to crypt files with a specific extention

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
                    mirror.CryptTime = duration;
                    Cryptage.Close();
                }
            }
        }
    }
}
