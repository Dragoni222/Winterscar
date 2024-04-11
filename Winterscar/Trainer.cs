namespace Winterscar;

//Trainer Logic: 
/* 1. If enemy and sword not in vision cone, turn in the direction where they are
 * 2. If enemy are greater than 4 spaces away, move forwards
 * 3. In the event of a loss, find a path that either lets the AI win or survive, then teach that path
 * 4. In the event of a draw or victory, look for places where an attack would have landed if they tried it
 *
 * Trainer can probably utilize a Map to experiment on, since all the tick logic is already there. Use the Tick function.  
 * 
 */

public class Trainer
{
    public void Train(List<AI> ai, List<Map> history)
    {
        //If there are 2 AI, p1 is [0] and p2 is [1] otherwise p2 is [0]
        //map goes: baselayer, players, swords, damagezones
        //important p-input codes for Tick(): 10(wait) 0-3(WASD)
        
        //1. If enemy and sword not in vision cone, turn in the direction where they are

        foreach (Map map in history)
        {
            List<int> p1Vision = map.VisionConeData(1);
            List<int> p2Vision = map.VisionConeData(-1);
            
            List<int> p1baselayer = new List<int>();
            List<int> p2baselayer = new List<int>();
            List<int> p1players = new List<int>();
            List<int> p2players = new List<int>();
            List<int> p1swords = new List<int>();
            List<int> p2swords = new List<int>();
            List<int> p1damagezones = new List<int>();
            List<int> p2damagezones = new List<int>();
            
            
            
            //1.
            if (!p1players.Contains(-1))
            {
                Map mapCopy = map.DeepCopy();
                mapCopy.Tick(1, 0);
                if (mapCopy.VisionConeData(1))
            }

        }
        
        
        
        
        
        
        
    }

    private List<List<int>> DecodeVisionData(List<int> p1Vision, List<int> p2Vision)//every other is each player ([0] = p1, [1] = p2, and so on), goes in order of layer
    {
        List<List<int>> final = new List<List<int>>();
        final.Add(new List<int>()); //p1base
        final.Add(new List<int>()); //p2base
        final.Add(new List<int>()); //p1players
        final.Add(new List<int>()); //p2players
        final.Add(new List<int>()); //p1swords
        final.Add(new List<int>()); //p2swords
        final.Add(new List<int>()); //p1damage
        final.Add(new List<int>()); //p2damage
        //populates vision layers
        for (int i = 0; i < p1Vision.Count; i++)
        {
            if (i % 4 == 0)
            {
                p1baselayer.Add(p1Vision[i]);
                p2baselayer.Add(p2Vision[i]);
            }
            else if (i % 4 == 1)
            {
                p1players.Add(p1Vision[i]);
                p2players.Add(p2Vision[i]);
            }
            else if (i % 4 == 2)
            {
                p1swords.Add(p1Vision[i]);
                p2swords.Add(p2Vision[i]);
            }
            else if (i % 4 == 3)
            {
                p1damagezones.Add(p1Vision[i]);
                p2damagezones.Add(p2Vision[i]);
            }
                
        }
    }
}