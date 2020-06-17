using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EasySave.Model
{
    class MirrorSave : ISave
    {
        public static SaveStat mirror;
        public static JsonLog JsonLogM;

        public void save(Array TaskInfo)
        {
            mirror = new SaveStat();
            JsonLogM = new JsonLog();


            PathAsker(TaskInfo);
            FileDetector();
            FileCopier();



            JsonLogM.CreateLog(TaskInfo, mirror);
            Console.ReadLine();

        }

        //Asking user for source and destination path
        public void PathAsker(Array TaskInfo)
        {
            Console.WriteLine("Getting source path...");
            mirror.source = TaskInfo.GetValue(2).ToString();
            Console.WriteLine(mirror.source);
            Console.WriteLine("Getting destination");
            mirror.destination = TaskInfo.GetValue(3).ToString();
            Console.WriteLine(mirror.destination);
        }

        //Listing and counting files in the source folder
        void FileDetector()
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
            Console.WriteLine();
            Console.WriteLine("File number: " + mirror.filecount);
            Console.WriteLine("Files total size: " + mirror.totalsize + "bytes");
            Console.WriteLine();
        }

        //Copying files one by one from source to destination

        void FileCopier()
        {
            mirror.remainingfiles = mirror.filecount;
            mirror.remainingsize = mirror.totalsize;
            DirectoryInfo path = new DirectoryInfo(mirror.source);
            FileInfo[] files = path.GetFiles();
            Console.WriteLine("Starting process...");

            //Initializating the timer and starting it
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // Create subdirectory structure in destination    
            foreach (string direction in System.IO.Directory.GetDirectories(mirror.source, "*", System.IO.SearchOption.AllDirectories))
            {
                System.IO.Directory.CreateDirectory(System.IO.Path.Combine(mirror.destination, direction.Substring(mirror.source.Length)));
            }

            // Copy each file in its final direction
            foreach (string file in System.IO.Directory.GetFiles(mirror.source, "*", System.IO.SearchOption.AllDirectories))
            {
                Console.WriteLine("copying " + file);
                mirror.currentfiletocopy = file;
                Console.WriteLine(mirror.source.Length);
                System.IO.File.Copy(file, System.IO.Path.Combine(mirror.destination, file.Substring(mirror.destination.Length - 2)), true);
                mirror.remainingfiles--;
                //Create a FileInfo object "f" with its directory to get the f.Length object's size (in bytes)
                FileInfo f = new FileInfo(file);
                Console.WriteLine(f.Name);
                mirror.remainingsize -= f.Length;
                mirror.time = stopwatch.Elapsed.Seconds;
                mirror.RefreshSaveStat(mirror);
            }
            stopwatch.Stop();
        }
    }
}
