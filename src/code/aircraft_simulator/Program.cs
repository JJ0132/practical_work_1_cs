using System;
using System.Buffers;
using System.Data;
namespace airport_sim;

public class Program
{

    public static void PrintMenu()
    {
        Console.WriteLine("");
        Console.WriteLine("");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("Welcome to the airport simulator");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("");

        Console.WriteLine("Choose an option: ");
        Console.WriteLine("");
        Console.WriteLine("1. Load flights from file");
        Console.WriteLine("2. Load a flight manually");
        Console.WriteLine("3. Start simulation (manual)");
        Console.WriteLine("4. Start simulation (automatic)");
        Console.WriteLine("5. Exit");
        Console.WriteLine("");
    }

    public static void LoadFiles()
    {
        Console.WriteLine("Insert the file");

    }

    public static void LoadFlight()
    {
        Console.WriteLine("Loading your flights manually");
        Console.WriteLine("Select your aircraft");

        SubMenu();
    }

    public static void StartSimManual()
    {
        Console.WriteLine("Starting simulation (manual)... ");

    }

    public static void StartSimAuto()
    {
        Console.WriteLine("Starting simulation... ");

    }

    public static void SubMenu()
    {


        Console.Clear();
        Console.WriteLine("-------------------");
        Console.WriteLine("1. Commercial Aircraft");
        Console.WriteLine("2. Cargo Aircraft");
        Console.WriteLine("3. Private Aircraft");
        Console.WriteLine("-------------------");

        
        string? input = Console.ReadLine();
        Console.WriteLine("");
        int election;
        bool rightNumber = int.TryParse(input, out election); 
        int finalElection = election;

        while(!rightNumber)
        {
            
            Console.Clear();
            Console.WriteLine("-------------------");
            Console.WriteLine("1. Commercial Aircraft");
            Console.WriteLine("2. Cargo Aircraft");
            Console.WriteLine("3. Private Aircraft");
            Console.WriteLine("-------------------");
            Console.WriteLine("");
            Console.WriteLine("ERROR. Please insert a number: ");
            string? input2 = Console.ReadLine();
            Console.WriteLine("");
            int election2;
            bool rightNumber2 = int.TryParse(input2, out election2);

            if (rightNumber2)
            {
                finalElection = election2;
                break;
            }
        }

        if (finalElection < 0 || finalElection > 3)
        {
            Console.WriteLine("ERROR!! Invalid number. Try again later.");
            throw new Exception();
        }
       
    }


    public static void Main()
    {

        // The print of the menu
        PrintMenu();

        // Variables for the input
        string? input = Console.ReadLine();
        Console.WriteLine("");
        int election;
        bool rightNumber = int.TryParse(input, out election); // TryParse to verify that the input is a integer and not another type of data
        int finalElection; // This variable is for saving the final checked election


        // This logic checks the inputs of the user to not get wrong inputs such as an "A" instead of an integer or numbers going away of the range of the menu
        if (rightNumber) // This checks if the input is an integer or not
        {

            finalElection = election; 

            if (election < 0 || election > 5) // Here we check the range of the number given
            {

                // This is for giving another try of putting a valid number
                Console.WriteLine("Enter a valid number please (0-5)");
                string? input2 = Console.ReadLine(); 
                int election2;
                bool secondCheck = int.TryParse(input2, out election2);

                finalElection = election2;

                if (secondCheck)
                {

                    if (election2 < 0 || election2 > 5)
                    {
                        Console.WriteLine("Not a valid election. Please try again later.");
                        throw new ArgumentException(); // This throws an exception because the number was not in range of the menu
                    }

                } else {
                    Console.WriteLine("Not a number. Please try again later.");
                    throw new ArgumentOutOfRangeException(); // This and the next throw gives an exception because the input was away of the variable type
                }

            }

        } else {
            Console.WriteLine("Not a number. Please, try again later.");
            throw new ArgumentOutOfRangeException();
        }

        
        // Here some logic to control the user election, for example, if user select 3, this will call the StartSimManual method
        if (finalElection == 1)
        {
            LoadFiles();
        } else if (finalElection == 2) {
            LoadFlight();
        } else if (finalElection == 3) {
            StartSimManual();
        } else if (finalElection == 4) {
            StartSimAuto();
        } else {
            Console.WriteLine("Finishing the program...");
        }
        
        
    }
}