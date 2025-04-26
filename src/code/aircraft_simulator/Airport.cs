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

                // La pista existe
                if(r != null)
                {
                    Console.Write($"Pista {r.getID()}. Estado: ");
                    if(r.getStatus() == RunwayStatus.Occupied)
                    {
                        Console.Write("Ocupada. ");
                        Aircraft? currentAircraft = r.getAircraft();

                        if(currentAircraft != null)
                        {
                            Console.Write($"Avión: {currentAircraft.getID()}. Ticks restantes: {r.getCurrentTicks()}\n");
                        }
                    }
                    else 
                    {
                        Console.Write("Libre\n");
                    }
                }
            }
        }
    }

    public void AdvanceTick()
    {
        //Update Runway
        for(int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 10; j++)
            {
                Runway r = this.Runways[i, j];

                if(r != null)
                {
                    if(r.updateRunway() == 0 && r.getAircraft() != null)
                    {
                        r.ReleaseRunway();
                    }

                    r.ShowInfo();
                }
            }
        } 

        //Update Aircraft
        foreach(Aircraft r in this.Aircrafts)
        {
            //Distancia recorrida
            int distance = (int)(r.getSpeed() * 0.25);

            if(r.getStatus() == EStatus.InFlight || r.getStatus() == EStatus.Waiting)
            {
                r.setDistance(r.getDistance() - distance);
            }

            if(r.getStatus() != EStatus.OnGround)
            {
                double consumption = r.getFuelConsumption() * distance;
                r.setCurrentFuel(r.getCurrentFuel() - consumption);
            }
            //Distancia recorrida

            if(r.getDistance() == 0 && r.getStatus() == EStatus.InFlight)
            {
                r.setStatus(EStatus.Waiting);
            }
            else if(r.getStatus() == EStatus.Waiting)
            {
                //Search free runway
                Runway? freeRunway = SearchRunway();
                if(freeRunway != null)
                {
                    freeRunway.RequestRunway(r);
                }
            }

            r.ShowInfo();
        }
    }

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

    public void LoadAircraftFromFile()
    {
        StreamReader? sr = null;

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
            Console.WriteLine("ERROR: Fichero no encontrado");
        }
        catch(ArgumentException)
        {
            Console.WriteLine("ERROR: Fichero no introducido");
        }
        finally
        {
            if(sr != null) sr.Close();
        }
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

