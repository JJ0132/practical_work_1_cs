using System.Data.Common;

public enum EStatus
{
    InFlight,
    Waiting,
    Landing,
    OnGround
};


public abstract class Aircraft
{
    private string ID;
    private EStatus Status;
    // in km
    private int Distance;
    //in km/h
    private int Speed; 
    //in liters
    private double FuelCapacity;
    //in liters/km
    private double FuelConsumption;
    //in liters
    private double CurrentFuel;


    public Aircraft(string id,int Distance,int Speed, double FuelCapacity,double FuelConsumption,double CurrentFuel)
    {
        this.Status = EStatus.InFlight;
        this.ID = id;
        this.Distance = Distance;
        this.Speed = Speed;
        this.FuelCapacity= FuelCapacity;
        this.FuelConsumption = FuelConsumption;
        this.CurrentFuel = CurrentFuel;
        
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
        this.CurrentFuel = d;
    }

}