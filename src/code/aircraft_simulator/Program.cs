using System;
namespace airport_sim;

public class Program
{
    public static void Main()
    {

        // The print of the menu
        Console.WriteLine("Welcome to the airport simulator");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("");

        Console.WriteLine("Choose an option: ");
        Console.WriteLine("");
        Console.WriteLine("1. Load flights from file");
        Console.WriteLine("2. Lead a flight manueally");
        Console.WriteLine("3. Start simulation (manual)");
        Console.WriteLine("4. Start simulation (automatic)");
        Console.WriteLine("5. Exit");

        // Variables for the input
        string input = Console.ReadLine();
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
                string input2 = Console.ReadLine(); 
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
                    throw new ArgumentOutOfRangeException(); // This and the new throw give an exeption because the input was away of the variable type
                }

            }

        } else {
            Console.WriteLine("Not a number. Please, try again later.");
            throw new ArgumentOutOfRangeException();
        }
        
    }
}