using Winterscar;

//todo: fix player colision, fix pushing against wall immortality, fix seemingly imperminant training, fix training going in the wrong direction, fix NaN in training

int mapSize = 11;
Console.WriteLine("enter seed:");
Random random = new Random(int.Parse(Console.ReadLine()));
Console.WriteLine("enter cycles:");
int cycles = int.Parse(Console.ReadLine());
Console.WriteLine("enter step:");
double step = double.Parse(Console.ReadLine());

Map map = new Map(mapSize, mapSize, 5);
AI ai1 = new AI(new List<int>() { map.MaxViewSquares * 4, 9 }, random, 2, 1);
AI ai2 = new AI(new List<int>() { map.MaxViewSquares * 4, 3, 8, 9 }, random, 2, 1);
List<AI> ais = new List<AI>() { ai1, ai2 };

for (int i = 0; i < cycles; i++)
{
    Console.WriteLine(i);
    map = new Map(mapSize, mapSize, 5);
    Fight fight = new Fight(ais, map);
    Console.WriteLine("winner: " + fight.PlayGame(false, 10));
    Trainer.Train(ais, fight.History, step);
}
Console.WriteLine("training complete");
map = new Map(mapSize, mapSize, 5);
Fight playerFight = new Fight(new List<AI>() { ai1 }, map);
playerFight.PlayGame(true, 1000);
map = new Map(mapSize, mapSize, 5);
playerFight = new Fight(new List<AI>() { ai2 }, map);
playerFight.PlayGame(true, 1000);


/*
 * Player input conversion:
 *
 *  w (forward) = 0
 *  d (turn left) = 1
 *  s (backward) = 2
 *  a (turn right) = 3
 *  i (swing forward) = 4
 *  l (swing right) = 5
 *  k (arrow) = 6
 *  j (swing left) = 7
 *  Space (dodge) = 8
 */










