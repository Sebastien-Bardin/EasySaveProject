using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasySave.Display
{
    class FrenchView : IView
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
            Console.WriteLine("Bienvenue dans le logiciel EasySave");


        }

        //Method to show the software features menu and to collect the user choice
        public string Menu()
        {

            Console.WriteLine("Choisissez une action :");
            Console.WriteLine("1--Créer un nouveau travail de sauvegarde");
            Console.WriteLine("2--Executer un travail sauvegardé");
            Console.WriteLine("3--Executer tous les travaux de sauvegarde");
            Console.WriteLine("4--Supprimer un travail de sauvegarde");
            Console.WriteLine("5--Quitter EasySave");
            Console.WriteLine("6--Changer de langue");

            string choice = Console.ReadLine();


            return choice;
        }
        //Methode to collect the user task choice
        public int TaskChoice()
        {
            Console.WriteLine("Choisissez un travail de sauvegarde");
            return int.Parse(Console.ReadLine());
        }

        //Method to collect new task parameters
        public Array FormTask()
        {
            string[] informations = new string[5];
            Console.WriteLine("Entrez le nom du travail");
            informations[0] = Console.ReadLine();
            Console.WriteLine("Entrez le type de travail");
            informations[1] = SaveTypeChoice();
            Console.WriteLine("Entrez le chemin du répertoire source");
            informations[2] = Console.ReadLine();
            Console.WriteLine("Entrez le chemin du repertoire de destination");
            informations[3] = Console.ReadLine();
            Console.WriteLine("Choisissez le type de destination");
            informations[4] = DTypeChoice();

            return informations;

        }

        //Method to show save types options and collect the user choice
        public string SaveTypeChoice()
        {
            string UserChoice;
            string SaveChoice = "";
            Console.WriteLine("1--Mirroir");
            Console.WriteLine("2--Differentiel");
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
            Console.WriteLine("Voulez-vous faire autre chose ? O/N");
            string choice = Console.ReadLine();
            if (choice == "O")
            {
                choice = "Y";
            }
            return choice;
        }
        public void UncorrectChoice()
        {
            Console.WriteLine("Choix incorrecte, choisissez une possibilité listée si-dessous");
        }
        //Method to list drivers options 
        public string DTypeChoice()
        {

            string DTypeChoice = "";
            Console.WriteLine("1--Disque locale");
            Console.WriteLine("2--Disque amovible");
            Console.WriteLine("3--Lecteur réseau");

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
            Console.WriteLine("Vous n'avez aucun travail sauvegardé");

        }

        public int LanguageChoice()
        {
            Console.WriteLine("Choisissez une langue, le programme redémarrera avec la lanugue choisie");
            Console.WriteLine("1--Anglais");
            Console.WriteLine("2--Français");
            string choice = Console.ReadLine();
            int choicenum;
            if (!int.TryParse(choice, out choicenum))
            {
                Console.WriteLine("Entrez un nombre correct");
                TaskChoice();

            }
            return choicenum;
        }
    }
}
