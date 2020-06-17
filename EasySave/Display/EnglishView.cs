using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Display
{
    class EnglishView : IView
    {
        //Method to Start the view with the banner and a space
        public void Start()
        {

            Banner();
            Console.SetCursorPosition(0, 15);


        }
        // Method use to creat the EasySave banner
        public void Banner()
        {
            Console.WriteLine("########    ###     ######  ##    ##  ######     ###    ##     ## ########");
            Console.WriteLine("##         ## ##   ##    ##  ##  ##  ##    ##   ## ##   ##     ## ##");
            Console.WriteLine("##        ##   ##  ##         ####   ##        ##   ##  ##     ## ##");
            Console.WriteLine("######   ##     ##  ######     ##     ######  ##     ## ##     ## ######");
            Console.WriteLine("##       #########       ##    ##          ## #########  ##   ##  ##");
            Console.WriteLine("##       ##     ## ##    ##    ##    ##    ## ##     ##   ## ##   ##");
            Console.WriteLine("######## ##     ##  ######     ##     ######  ##     ##    ###    ########");

            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Welcome to our save software EasySave");


        }

        //Method to show the software features menu and to collect the user choice
        public string Menu()
        {

            Console.WriteLine("Choose an action :");
            Console.WriteLine("1--Creat a new save task");
            Console.WriteLine("2--Execute a saved task");
            Console.WriteLine("3--Execute all saved tasks");
            Console.WriteLine("4--Delete a saved task");
            Console.WriteLine("5--Exit EasySave");
            Console.WriteLine("6--Change Language");

            string choice = Console.ReadLine();


            return choice;
        }
        //Methode to collect the user task choice
        public int TaskChoice()
        {
            Console.WriteLine("Choose a task");
            return int.Parse(Console.ReadLine());
        }

        //Method to collect new task parameters
        public Array FormTask()
        {
            string[] informations = new string[5];
            Console.WriteLine("Enter the task name");
            informations[0] = Console.ReadLine();
            Console.WriteLine("Enter the save type");
            informations[1] = SaveTypeChoice();
            Console.WriteLine("Please enter the source folder");
            informations[2] = Console.ReadLine();
            Console.WriteLine("Please enter the destination folder");
            informations[3] = Console.ReadLine();
            Console.WriteLine("Please choose the kind of Destination");
            informations[4] = DTypeChoice();

            return informations;

        }

        //Method to show save types options and collect the user choice
        public string SaveTypeChoice()
        {
            string UserChoice;
            string SaveChoice = "";
            Console.WriteLine("1--Mirror");
            Console.WriteLine("2--Differential");
            UserChoice = Console.ReadLine();

            switch (UserChoice)
            {
                case "1":
                    SaveChoice = "Mirror";
                    break;
                case "2":
                    SaveChoice = "Differential";
                    break;
                default:
                    UncorrectChoice();
                    SaveTypeChoice();
                    break;
            }

            return SaveChoice;
        }

        //Method to list all saved tasks
        public void ListTasks(Array ListTasks)
        {
            for (int i = 0; i < ListTasks.Length; i++)
            {
                Console.WriteLine(i + "--" + ListTasks.GetValue(i).ToString());
            }

        }

        //Method to show continue dialog 
        public string ContinuChoice()
        {
            Console.WriteLine("Do you want to do somthing else ? Y/N");
            return Console.ReadLine();
        }
        public void UncorrectChoice()
        {
            Console.WriteLine("UncorrectChoice please choose one listed below");
        }
        //Method to list drivers options 
        public string DTypeChoice()
        {

            string DTypeChoice = "";
            Console.WriteLine("1--Local Disk");
            Console.WriteLine("2--Removable Device");
            Console.WriteLine("3--Remote Disk");

            switch (Console.ReadLine())
            {
                case "1":
                    DTypeChoice = "Local";
                    break;
                case "2":
                    DTypeChoice = "Removable";
                    break;
                case "3":
                    DTypeChoice = "Remote";
                    break;
                default:
                    UncorrectChoice();
                    SaveTypeChoice();
                    break;
            }
            return DTypeChoice;
        }

        //Method to show that 5 tasks have already been saved
        public void NoTasks()
        {
            Console.WriteLine("You don't have any saved task");

        }

        public int LanguageChoice()
        {
            Console.WriteLine("Choose a language, the program will restart with the correct language");
            Console.WriteLine("1--English");
            Console.WriteLine("2--French");
            string choice = Console.ReadLine();
            int choicenum;
            if (!int.TryParse(choice, out choicenum))
            {
                Console.WriteLine("please enter a correct number");
                TaskChoice();

            }
            return choicenum;

        }
    }
}
