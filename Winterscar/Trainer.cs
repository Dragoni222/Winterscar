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
    public static void Train(List<AI> ai, List<Map> history, double step)
    {
        //If there are 2 AI, p1 is [0] and p2 is [1] otherwise p2 is [0]
        //map goes: baselayer, players, swords, damagezones
        //important p-input codes for Tick(): 10(wait) 0-3(WASD)
        //note, this is suboptimal to an extreme degree. It re-asks the ai for the node data, just because I didn't think
        //to route that data through earlier methods. It has the positive side effect that if some other
        //training changes the weights in the right direction, it will impact it less, and the reverse.

        foreach (Map map in history)
        {
            List<int> p1Vision = map.VisionConeData(1);
            List<int> p2Vision = map.VisionConeData(-1);
            List<List<int>> decodedp1 = DecodeVisionData(p1Vision);
            List<List<int>> decodedp2 = DecodeVisionData(p2Vision);

            List<int> p1baselayer = decodedp1[0];
            List<int> p2baselayer = decodedp2[0];
            List<int> p1players = decodedp1[1];
            List<int> p2players = decodedp2[1];
            List<int> p1swords = decodedp1[2];
            List<int> p2swords = decodedp2[2];
            List<int> p1damagezones = decodedp1[3];
            List<int> p2damagezones = decodedp2[3];
            
            
            
            //1.
            if (!p1players.Contains(-1))
            {
                Console.WriteLine("TRAIN");
                Map mapCopy = map.DeepCopy();
                mapCopy.Tick(1, 0);
                if (DecodeVisionData(mapCopy.VisionConeData(1))[2].Contains(-1))
                {
                    ai[0].Train(ai[0].AskNodeData(p1Vision),new List<double>() {0,1,0,0,0,0,0,0,0}, step);
                }
                else // could potentially be improved if AI seems to face the wrong direction often
                {
                    ai[0].Train(ai[0].AskNodeData(p1Vision),new List<double>() {0,0,0,1,0,0,0,0,0}, step);
                }
            }

        }
        
        
        
        
        
        
        
    }

    private static List<List<int>> DecodeVisionData(List<int> p1Vision)
    {
        List<List<int>> final = new List<List<int>>();
        final.Add(new List<int>()); //p1base
        final.Add(new List<int>()); //p1players
        final.Add(new List<int>()); //p1swords
        final.Add(new List<int>()); //p1damage
        //populates vision layers
        for (int i = 0; i < p1Vision.Count; i++)
        {
            if (i % 4 == 0)
            {
                final[0].Add(p1Vision[i]);
            }
            else if (i % 4 == 1)
            {
                final[1].Add(p1Vision[i]);
            }
            else if (i % 4 == 2)
            {
                final[2].Add(p1Vision[i]);
            }
            else if (i % 4 == 3)
            {
                final[3].Add(p1Vision[i]);
            }
                
        }

        return final;
    }
}