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
    private int CurrentTicks;

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

    public string getID()
    {
        return this.id;
    }

    public Aircraft? getAircraft()
    {
        return this.CurrentAircraft;
    }

    public int getCurrentTicks()
    {
        return this.CurrentTicks;
    }

    //It assigns an aircraft to land on this runway.
    public void RequestRunway(Aircraft s)
    {
        if(Status == RunwayStatus.Free)
        {
            Status = RunwayStatus.Occupied;
            CurrentAircraft = s;
            this.CurrentTicks = this.TicksAvailability;
            this.CurrentAircraft.setStatus(EStatus.Landing);
        }
    }

    //It frees the runway once the aircraft has landed and cleared it.
    public void ReleaseRunway()
    {
        if (Status == RunwayStatus.Occupied && CurrentAircraft != null)
        {
            Status = RunwayStatus.Free;
            CurrentAircraft.setStatus(EStatus.OnGround);
            CurrentAircraft = null;
            this.CurrentTicks = 0;
        }
    }

    public int updateRunway()
    {
        if(this.CurrentAircraft != null)
        {
            if(this.CurrentAircraft.getStatus() == EStatus.Landing)
            {
                if (this.CurrentTicks > 0)
                {
                    this.CurrentTicks--;
                }
            }
        }

        return this.CurrentTicks;
    }

    public void ShowInfo()
    {
        Console.Write(id);
        switch(Status)
        {
            case RunwayStatus.Free:
                Console.WriteLine(" Status: Free");
                break;
            case RunwayStatus.Occupied:
                Console.WriteLine(" Status: Occupied");
                if(this.CurrentAircraft != null) this.CurrentAircraft.ShowInfo();
                break;
        }
    }
}