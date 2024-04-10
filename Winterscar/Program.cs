using Winterscar;

int mapSize = 11;
Console.WriteLine("enter seed:");
Random random = new Random(int.Parse(Console.ReadLine()));
Map map = new Map(mapSize, mapSize, 5);
AI ai = new AI(new List<int>() { map.MaxViewSquares * 4, 9 }, random, 2, 1);
map.PlayGame(ai);



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










