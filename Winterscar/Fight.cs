namespace Winterscar;

public class Fight
{
    public List<AI> Players;
    public Map CurrentMap;
    public List<Map> History;
    
    public Fight(List<AI> players, Map map)
    {
        Players = players;
        CurrentMap = map;
        History = new () { map };
    }

    public int PlayGame(bool displayGame, int maxTurns)
    {
        int winner = 0;
        while (winner == 0 && History.Count <= maxTurns)
        {
            winner = CurrentMap.PlayGame(Players, displayGame);
            //This might be very slow idk
            History.Add(CurrentMap.DeepCopy());
        }

        return winner;
    }
    
}