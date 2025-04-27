namespace airport_sim;
public class Airport
{
    private Runway[,] Runways;

    private List<Aircraft> Aircrafts;

    public Airport()
    {
        this.Runways = new Runway[10, 10];
        this.Aircrafts = new List<Aircraft>();
    }

    public void setRunway(Runway r, int i, int j)
    {
        this.Runways[i,j] = r;
    }

    public void ShowStatus()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                Runway r = Runways[i, j];

                // We confirm that the runway exists
                if(r != null)
                {
                    Console.Write($"Runaway {r.getID()}. Status: ");
                    if(r.getStatus() == RunwayStatus.Occupied)
                    {
                        Console.Write("Busy. ");
                        Aircraft? currentAircraft = r.getAircraft();

                        if(currentAircraft != null)
                        {
                            Console.Write($"Flight: {currentAircraft.getID()}. Remaining ticks: {r.getCurrentTicks()}\n");
                        }
                    }
                    else 
                    {
                        Console.Write("Free\n");
                    }
                }
            }
        }
    }

    //This method is needed to stop the automatic simulation when all the aircrafts are on ground
    public List<Aircraft> GetAircraftList()
    {
        return this.Aircrafts;
    }

    //To show the status if the aircrafts each tick
    public void ShowAircraftStatus()
    {
        Console.WriteLine();
        Console.WriteLine("Aircrafts Status:");

        foreach (Aircraft aircraft in this.Aircrafts)
        {
            Console.WriteLine("------------------------------------");
            Console.WriteLine($"Aircraft ID: {aircraft.getID()}");
            Console.WriteLine($"Status: {aircraft.getStatus()}");
            Console.WriteLine($"Distance remaining: {aircraft.getDistance()} km");
            Console.WriteLine($"Current fuel: {aircraft.getCurrentFuel()} liters");
        }
    }

    //The method for the manual simulation (third option in the menu)
    public void AdvanceTick()
    {
        // Update Runways first
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Runway r = this.Runways[i, j];

                if (r != null)
                {
                    if (r.updateRunway() == 0 && r.getAircraft() != null)
                    {
                        r.ReleaseRunway();
                    }
                }
            }
        }

        // Update Aircrafts
        foreach (Aircraft r in this.Aircrafts)
        {
            // Calculates the distance that the aircraft has followed in 15 minutes.
            int distance = (int)(r.getSpeed() * 0.25);

            // The distance is updated only if the aircraft is in Flight or Waiting.
            if (r.getStatus() == EStatus.InFlight || r.getStatus() == EStatus.Waiting)
            {
                r.setDistance(r.getDistance() - distance);

                // We confirm that there is no negative distance
                if (r.getDistance() < 0)
                    r.setDistance(0);
            }

            // It only consumes fuel if the aircraft is not on Ground
            if (r.getStatus() != EStatus.OnGround)
            {
                double consumption = r.getFuelConsumption() * distance;
                r.setCurrentFuel(r.getCurrentFuel() - consumption);

                if (r.getCurrentFuel() < 0)
                    r.setCurrentFuel(0);
            }

            // It changes the status of the aircraft depending on the distance
            if (r.getDistance() == 0 && r.getStatus() == EStatus.InFlight)
            {
                r.setStatus(EStatus.Waiting);
            }
            else if (r.getStatus() == EStatus.Waiting)
            {
                Runway? freeRunway = SearchRunway();

                if (freeRunway != null)
                {
                    freeRunway.RequestRunway(r); // It assigns a runway and changes to Landing
                }
                else
                {
                    Console.WriteLine($"[INFO] No free runway available for aircraft {r.getID()}. Still waiting...");
                }
            }

        }
    }


    //This method is for search runaways that are free due to arrive the airplanes
    private Runway? SearchRunway()
    {
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                Runway r = this.Runways[i,j];
                if(r != null)
                {
                    if(r.getStatus() == RunwayStatus.Free)
                    {
                        return this.Runways[i,j];
                    }
                }

            }
        }

        return null;
    }

    private int ReadInt(string message)
    {
        int result;
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            if (int.TryParse(input, out result))
                return result;
            Console.WriteLine("Por favor, introduce un número entero válido.");
        }
    }

    private double ReadDouble(string message)
    {
        double result;
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();
            if (double.TryParse(input, out result))
                return result;
            Console.WriteLine("Por favor, introduce un número decimal válido.");
        }
    }

    //This is the constructor of the aircrafts that are created to save all the values into it.
    private void GetDataByUser(ref string id, ref int distance, ref int speed, 
        ref double fuelCapacity, ref double fuelConsumption, ref EStatus status, ref double currentFuel)
    {
            //Pedir usuario datos avión
            Console.Write("ID: ");
            id = Console.ReadLine() ?? string.Empty;
            distance = ReadInt("Distance: ");
            speed = ReadInt("Speed: ");
            fuelCapacity = ReadDouble("Fuel Capacity: ");
            fuelConsumption = ReadDouble("Fuel Consumption: ");
            if(fuelConsumption > fuelCapacity)
            {
                fuelConsumption = fuelCapacity;
            }
            currentFuel = ReadDouble("Current Fuel: ");
    }

    //This method is for the creation of the aircrafts manually
    public void AddAircraft()
    {
        bool secondRound = false;

        do
        {
            Console.WriteLine();
            Console.WriteLine("1. Cargo Aircraft");
            Console.WriteLine("2. Commercial Aircraft");
            Console.WriteLine("3. Private Aircraft");
            Console.Write("Select a type aircraft: ");

            if(int.TryParse(Console.ReadLine(), out int option))
            {
                if(option < 1 || option > 3)
                {
                    if (secondRound)
                    {
                        throw new ArgumentException("Opción fuera de rango");
                    }
                    secondRound = true;
                }
                else
                {
                    string id = "";
                    int distance = 0, speed = 0;
                    double fuelCapacity = 0, fuelConsumption = 0, currentFuel = 0;
                    EStatus status = EStatus.InFlight;

                    GetDataByUser(ref id, ref distance, ref speed, ref fuelCapacity, ref fuelConsumption, ref status, ref currentFuel);
                    Aircraft? r = null;

                    switch(option)
                    {
                        case 1:
                            double cargo = ReadDouble("Máximo cargo: ");
                            
                            //Creas objeto
                            r = new CargoAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, currentFuel, cargo);
                            break;
                        case 2:
                            int numOfPassengers = ReadInt("Num. of passengers: ");

                            r = new CommercialAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, currentFuel, numOfPassengers);
                            break;
                        case 3:
                            Console.Write("Owner: ");
                            string owner = Console.ReadLine() ?? string.Empty;

                            r = new PrivateAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, currentFuel, owner);
                            break;
                    }

                    if(r != null)
                    {
                        this.Aircrafts.Add(r);
                    }
                }
            }
            else
            {
                if(secondRound)
                {
                    throw new ArgumentOutOfRangeException("Caracter no válido");
                }
                secondRound = true;
            }
        } while(secondRound);   
    }

    //This method is for the first option of the menu
    public void LoadAircraftFromFile()
    {
        StreamReader? sr = null;

        //With the try and catches we ensure that the file is correct
        try
        {
            Console.Write("Input a file path: ");
            string? path = Console.ReadLine();
            string separator = ",";

            if(path == null)
            {
                throw new ArgumentException("No file indicated");
            }

            sr = File.OpenText(path);
            string? line = "";

            while((line = sr.ReadLine()) != null)
            {
                LoadLine(separator, line);
            }
        }
        catch(FileNotFoundException)
        {
            Console.WriteLine("ERROR: Unfound file");
        }
        catch(ArgumentException)
        {
            Console.WriteLine("ERROR: Unfound file");
        }
        finally
        {
            if(sr != null) sr.Close();
        }
    }

    //This method is for the second one in the menu. This add the aircraft created manually to the list of aircrafts to simule later.
    public void AddAircraftManually(Aircraft aircraft)
    {
        this.Aircrafts.Add(aircraft);
    }


    private void LoadLine(string separator, string line)
    {
        string[] values = line.Split(separator);
        string id = values[0];

        string statusString = values[1];
        EStatus status;

        switch (statusString)
        {
            case "InFlight":
                status = EStatus.InFlight;
                break;
            case "Waiting":
                status = EStatus.Waiting;
                break;
            case "Landing":
                status = EStatus.Landing;
                break;
            case "OnGround":
                status = EStatus.OnGround;
                break;
            default:
                throw new ArgumentException("Status invalid");
        }

        int distance = int.Parse(values[2]);
        int speed = int.Parse(values[3]);
        string type = values[4];
        double fuelCapacity = double.Parse(values[5]);
        double fuelConsumption = double.Parse(values[6]);

        Aircraft r;

        switch (type)
        {
            case "Commercial":
                int numOfPassengers = int.Parse(values[7]);
                r = new CommercialAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, numOfPassengers);
                break;

            case "Cargo":
                double maximumLoad = double.Parse(values[7]);
                r = new CargoAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, maximumLoad);
                break;
                
            case "Private":
                string owner = values[7];
                r = new PrivateAircraft(id, distance, speed, fuelCapacity, fuelConsumption, status, owner);
                break;
            default:
                throw new ArgumentException("Type of aircraft invalid");
        }

        this.Aircrafts.Add(r);
    }
}

