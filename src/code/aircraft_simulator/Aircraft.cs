public enum EStatus
{
    InFlight,
    Waiting,
    Landing,
    OnGround
}


public abstract class Aircraft
{
    private string ID;
    private EStatus Status;
    
    private int Distance; // In km
    
    private int Speed; // In km/h
    
    private double FuelCapacity; // In liters
    
    private double FuelConsumption; // In liters/km
    
    private double CurrentFuel; // In liters

    
    public Aircraft(string id, int Distance, int Speed, double FuelCapacity, double FuelConsumption, EStatus status, double CurrentFuel)
    {
        this.Status = status;
        this.ID = id;
        this.Distance = Distance;
        this.Speed = Speed;
        this.FuelCapacity= FuelCapacity;
        this.FuelConsumption = FuelConsumption;
        this.CurrentFuel = CurrentFuel;
    }

    public Aircraft(string id,int Distance,int Speed, double FuelCapacity, double FuelConsumption, EStatus Status)
    {
        this.Status = Status;
        this.ID = id;
        this.Distance = Distance;
        this.Speed = Speed;
        this.FuelCapacity= FuelCapacity;
        this.FuelConsumption = FuelConsumption;
        this.CurrentFuel = FuelCapacity;
    }

    public string getID()
    {
        return this.ID;
    }
    public int getDistance()
    {
        return this.Distance;
    }
    public int getSpeed()
    {
        return this.Speed;
    }
    public double getFuelCapacity()
    {
        return this.FuelCapacity;
    }
    public double getFuelConsumption()
    {
        return this.FuelConsumption;
    }
    public double getCurrentFuel()
    {
        return this.CurrentFuel;
    }
    public void setID(string d)
    {
        this.ID = d;
    }
    public void setDistance(int d)
    {
        if(d < 0)
        {
            d = 0;
        }

        this.Distance = d;
    }
    public void setSpeed(int d)
    {
        this.Speed = d;
    }
    public void setFuelCapacity(double d)
    {
        this.FuelCapacity = d;
    }
    public void setFuelConsumption(double d)
    {
        this.FuelConsumption = d;
    }
    public void setCurrentFuel(double d)
    {
        if(d < 0)
        {
            d = 0;
        }
        
        this.CurrentFuel = d;
    }

    public void setStatus(EStatus e)
    {
        this.Status = e;
    }

    public EStatus getStatus()
    {
        return this.Status;
    }

    public abstract void ShowInfo();
}

public class CommercialAircraft : Aircraft //Commercial Aircraft subclass added by inheritance 
{
    private int numOfPassengers; //Specific attribute of the Commercial Aircraft

    public CommercialAircraft(string id, int Distance, int Speed, double FuelCapacity, double FuelConsumption, EStatus status, double CurrentFuel, int numOfPassengers) : base(id, Distance, Speed, FuelCapacity, FuelConsumption, status, CurrentFuel)
    {
        this.numOfPassengers = numOfPassengers;
    }

    public CommercialAircraft(string id, int Distance, int Speed, double FuelCapacity, double FuelConsumption, EStatus status, int numOfPassengers) : base(id, Distance, Speed, FuelCapacity, FuelConsumption, status)
    {
        this.numOfPassengers = numOfPassengers;
    }


    public int GetNumOfPassengers()
    {
        return this.numOfPassengers;
    }

    public override void ShowInfo() //We call the ShowInfo method in order to show the info about the Commercial Aircraft
    {
        Console.WriteLine($"Commercial Aircraft ID: {getID()}");
        Console.WriteLine($"Passengers: {numOfPassengers}");
        Console.WriteLine($"Commercial Aircraft speed: {getSpeed()}");
        Console.WriteLine($"Commercial Aircraft Status: {getStatus()}");
    }
}

public class CargoAircraft : Aircraft //Cargo Aircraft subclass added by inheritance
{
    private double maximumLoad; //Specific attribute of the Cargo Aircraft 

    public CargoAircraft(string id, int Distance, int Speed, double FuelCapacity, double FuelConsumption, EStatus status, double CurrentFuel, double maximumLoad) : base(id, Distance, Speed, FuelCapacity, FuelConsumption, status, CurrentFuel)
    {
        this.maximumLoad = maximumLoad;
    }

    public CargoAircraft(string id, int Distance, int Speed, double FuelCapacity, double FuelConsumption, EStatus status, double maximumLoad) : base(id, Distance, Speed, FuelCapacity, FuelConsumption, status)
    {
    this.maximumLoad = maximumLoad;
    }


    public double GetMaximumLoad()
    {
        return this.maximumLoad;
    }

    public override void ShowInfo() //We call the ShowInfo method in order to show info about the Cargo Aircraft
    {
        Console.WriteLine($"Cargo Aircraft ID: {getID()}");
        Console.WriteLine($"Maximum load of the Cargo Aircraft: {maximumLoad}");
        Console.WriteLine($"Cargo Aircraft speed: {getSpeed()}");
        Console.WriteLine($"Cargo Aircraft Status: {getStatus()}");
    }

}

public class PrivateAircraft : Aircraft //Private Aircraf subclass added by inheritance
{
    private string Owner; //Specific attribute of the Private Aircraft

    public PrivateAircraft(string id, int Distance, int Speed, double FuelCapacity, double FuelConsumption, EStatus status, double CurrentFuel, string Owner) : base(id, Distance, Speed, FuelCapacity, FuelConsumption, status, CurrentFuel)
    {
        this.Owner = Owner;
    }

    public PrivateAircraft(string id, int Distance, int Speed, double FuelCapacity, double FuelConsumption, EStatus status, string Owner) : base(id, Distance, Speed, FuelCapacity, FuelConsumption, status)
    {
        this.Owner = Owner;
    }


    public override void ShowInfo() //We call the ShowInfo method in order to show info about the Private Aircraft
    {
        Console.WriteLine($"Private Aircraft ID: {getID()}");
        Console.WriteLine($"Owner of the Private Aircraft: {Owner}");
        Console.WriteLine($"Private Aircraft speed: {getSpeed()}");
        Console.WriteLine($"Private Aircraft Status: {getStatus()}");
    }
}

