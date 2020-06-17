using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EasySave.Model
{
    class DifferentialSave : ISave
    {
        public static SaveStat differential;
        public static JsonLog JsonLogD;


        public void save(Array TaskInfo)
        {
            //Object that allow to get the values for the Log files
            differential = new SaveStat();
            JsonLogD = new JsonLog();

            PathAsker(TaskInfo);
            FileDetector();
            FileCopier();
            JsonLogD.CreateLog(TaskInfo, differential);

            Console.ReadLine();

        }

        //Get paths from JsonFile Array
        public void PathAsker(Array TaskInfo)
        {
            Console.WriteLine("Getting source path...");
            differential.source = TaskInfo.GetValue(2).ToString();
            Console.WriteLine(differential.source);
            Console.WriteLine("Getting destination");
            differential.destination = TaskInfo.GetValue(3).ToString();
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

        void FileCopier()
        {
            differential.remainingfiles = differential.filecount;
            differential.remainingsize = differential.totalsize;
            DirectoryInfo path = new DirectoryInfo(differential.source);
            FileInfo[] files = path.GetFiles();
            Console.WriteLine("Starting process...");

            //Initializating the timer and starting it
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Create subdirectory structure in destination    
            foreach (string direction in System.IO.Directory.GetDirectories(differential.source, "*", System.IO.SearchOption.AllDirectories))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(differential.destination, direction.Substring(differential.source.Length)));
            }

            // Copy each file in its final direction
            foreach (string file in System.IO.Directory.GetFiles(differential.source, "*", System.IO.SearchOption.AllDirectories))
            {
                Console.WriteLine("copying " + file);
                differential.currentfiletocopy = file;

                //Create a FileInfo object "f" with its directory to get the f.Length object's size (in bytes)
                FileInfo fs = new FileInfo(file);
                string dfile = differential.destination + "/" + file.Remove(0, differential.source.Length);
                FileInfo fd = new FileInfo(dfile);
                if (fs.LastWriteTime > fd.LastWriteTime)
                {
                    //Copy File
                    Console.WriteLine("Source file is more recent ");
                    System.IO.File.Copy(file, System.IO.Path.Combine(differential.destination, file.Substring(differential.destination.Length - 2)), true);
                    Console.WriteLine("Copy has been to be executed");
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
            }

           
        }
    }
}