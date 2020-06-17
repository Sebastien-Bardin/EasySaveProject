using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasySavetest.ViewModel;


namespace EasySavetest.Display
{
     //Method to Start the view with the banner and a space
        class View
        {
           /* public void Start()
            {

               
                Console.SetCursorPosition(0, 15);


            }

           

            public string Menu()
            {

                Console.WriteLine("Choose an action :");
                Console.WriteLine("1--Creat a new save task");
                Console.WriteLine("2--Execute a saved task");
                Console.WriteLine("3--Execute all saved tasks");
                Console.WriteLine("4--Delete a saved task");
                Console.WriteLine("5--Exit EasySave");

                string choice = Console.ReadLine();


                return choice;
            }

            public int TaskChoice()
            {
                Console.WriteLine("Choose a task");
                return int.Parse(Console.ReadLine());
            }

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

            public void ListTasks(Array ListTasks)
            {
                for (int i = 0; i < ListTasks.Length; i++)
                {
                    Console.WriteLine(i + "--" + ListTasks.GetValue(i).ToString());
                }

            }

            public string ContinuChoice()
            {
                Console.WriteLine("Do you want to do somthing else ? Y/N");
                return Console.ReadLine();
            }
            public void UncorrectChoice()
            {
                Console.WriteLine("UncorrectChoice please choose one listed below");
            }

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

            public string MaxTask()
            {
                Console.WriteLine("You already have five taks you need to delete one before adding another");
                Console.WriteLine("Do you want to delete a task Y/N");
                return Console.ReadLine();

            }*/
        }
    }
