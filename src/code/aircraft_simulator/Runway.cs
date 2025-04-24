namespace airport_sim;

public enum RunwayStatus
{
    Free,
    Occupied
}

public class Runway
{
    private string id;
    private RunwayStatus Status;

    private Aircraft? CurrentAircraft;
    private int TicksAvailability;
    private int currentTicks;
    private int num;

    public Runway(string id)
    {
        this.id = id;
        TicksAvailability = 3;
        Status = RunwayStatus.Free;
        this.CurrentAircraft = null;
    }

    public RunwayStatus getStatus()
    {
        return this.Status;
    }

    public void RequestRunway(Aircraft s)
    {
        if(Status == RunwayStatus.Free)
        {
            Status = RunwayStatus.Occupied;
            CurrentAircraft = s;

        }
    }
    public void ReleaseRunway()
    {
        if (Status == RunwayStatus.Occupied)
        {
            Status = RunwayStatus.Free;
            CurrentAircraft = null;
            this.TicksAvailability = 3;
        }
    }

    public int updateRunway()
    {
        if (this.TicksAvailability > 0)
        {
            this.TicksAvailability--;
        }

        return this.TicksAvailability;
    }
}
