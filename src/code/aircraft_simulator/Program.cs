using System;
using System.Buffers;
using System.Data;
namespace airport_sim;

public class Program
{
    static Airport airport = new Airport();

    //The menu
    public static void PrintMenu()
    {
        Console.Clear();
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

    //The selection to load a file (.csv) with flights 
    public static void LoadFiles()
    {
        Console.WriteLine("Insert the file");

        try
        {
            airport.LoadAircraftFromFile();
            Console.WriteLine("Files succesfully loaded from the file");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR!!!! {ex.Message}");
        }
    }
    
    //This is the selection of load manually 
    public static void LoadFlight()
    {
        Console.WriteLine("Loading your flights manually");
        Console.WriteLine("Select your aircraft");

        int finalElection = SubMenu(); 
        
        Console.Write("ID: ");
        string id = Console.ReadLine() ?? "";
        
        Console.Write("Distance (km): ");
        int distance = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Speed (km/h): ");
        int speed = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Fuel capacity (liters): ");
        double fuelCapacity = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Fuel consumption (liters/km): ");
        double fuelConsumption = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Current fuel (liters): ");
        double currentFuel = double.Parse(Console.ReadLine() ?? "0");

        
        Aircraft? aircraft = null;
        EStatus status = EStatus.InFlight;

        if (finalElection == 1)
        {
            Console.Write("Number of passengers: ");
            int numPassengers = int.Parse(Console.ReadLine() ?? "0");
            aircraft = new CommercialAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, currentFuel, numPassengers);
        }
        else if (finalElection == 2)
        {
            Console.Write("Maximum load (kg): ");
            double maxLoad = double.Parse(Console.ReadLine() ?? "0");
            aircraft = new CargoAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, currentFuel, maxLoad);
        }
        else if (finalElection == 3)
        {
            Console.Write("Owner name: ");
            string owner = Console.ReadLine() ?? "";
            aircraft = new PrivateAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, currentFuel, owner);
        }

        if (aircraft != null)
        {
            airport.AddAircraftManually(aircraft);
            Console.WriteLine("Aircraft added successfully!");
        }

        
    }                    

    public static void StartSimManual()
    {
        Console.Clear();
        Console.WriteLine("Starting simulation (manual)... ");
        Console.WriteLine("Press ENTER to advance one tick, or type 'exit' to finish.");

        string? input = "";

        do
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("------------------------------------");
            Console.WriteLine("Current Airport Status:");
            airport.ShowStatus();
            airport.ShowAircraftStatus(); 
            Console.WriteLine("------------------------------------");

            Console.WriteLine();
            Console.Write("Press ENTER to advance tick, or type 'exit': ");
            input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                airport.AdvanceTick(); 
            }

        } while (input != "exit");

        Console.WriteLine("Manual simulation finalized.");
    }

    public static void StartSimAuto() // Method for the automatic simulation
    {
        Console.Clear();
        Console.WriteLine("Starting simulation (automatic)... ");

        bool simulationRunning = true;
        int tickCount = 0;

        while (simulationRunning)
        {
            Console.Clear();
            Console.WriteLine($"Tick #{tickCount + 1}");

            airport.AdvanceTick(); 

            Console.WriteLine("------------------------------------"); 
            Console.WriteLine("Current Airport Status:");
            airport.ShowStatus(); //To show the runaway status
            airport.ShowAircraftStatus(); //To show the aircraft status
            Console.WriteLine("------------------------------------");

            //It delays 2.5s the ticks
            Thread.Sleep(2500);

            //See if all aircrafts are on ground
            simulationRunning = !AllAircraftsOnGround();

            tickCount++;
        }

        Console.WriteLine("All aircrafts are on the ground. Simulation finished!");
    }

    //Checks if all the aircraft are on ground
    public static bool AllAircraftsOnGround()
    {
        foreach (Aircraft aircraft in airport.GetAircraftList())
        {
            if (aircraft.getStatus() != EStatus.OnGround)
            {
                return false;
            }
        }
        return true;
    }

    //The submenu to create aircrafts manually
    public static int SubMenu()
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

        while (!rightNumber || finalElection < 1 || finalElection > 3)
        {
            Console.Clear();
            Console.WriteLine("-------------------");
            Console.WriteLine("1. Commercial Aircraft");
            Console.WriteLine("2. Cargo Aircraft");
            Console.WriteLine("3. Private Aircraft");
            Console.WriteLine("-------------------");
            Console.WriteLine("");
            Console.WriteLine("ERROR. Please insert a valid number: ");
            input = Console.ReadLine();
            Console.WriteLine("");
            rightNumber = int.TryParse(input, out finalElection);
        }

        return finalElection;
    }


    //Main
    public static void Main()
    {
        airport.setRunway(new Runway("Runway1"), 0, 0);

        int finalElection = 0;

        do
        {
            // Print the menu
            PrintMenu();

            // Input reading
            string? input = Console.ReadLine();
            Console.WriteLine("");

            bool rightNumber = int.TryParse(input, out finalElection);

            if (!rightNumber || finalElection < 1 || finalElection > 5)
            {
                Console.WriteLine("Invalid input. Please select a number between 1 and 5.");
                continue;
            }

            switch (finalElection)
            {
                case 1:
                    LoadFiles();
                    break;
                case 2:
                    LoadFlight();
                    break;
                case 3:
                    StartSimManual();
                    break;
                case 4:
                    StartSimAuto();
                    break;
                case 5:
                    Console.WriteLine("Exiting the program...");
                    break;
            }

        } while (finalElection != 5);
    }
}