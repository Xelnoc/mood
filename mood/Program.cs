using System.Text;

namespace mood;

public static class Program {
    
    // ReSharper disable once InconsistentNaming
    private static readonly Player player = new Player();
    private const int Fov = 90;
    private const int WallHeight = 30;
    private const int ScreenX = 300;
    private const int ScreenY = 45;
    private const int MapX = 100;
    private const int MapY = 100;
    public static bool[,] WallMap = DrawWallMap();
    public static bool[,] EnemyMap = DrawWallMap();

    static void Main()
    {
        Items.Init();
        do
        {
            Print();
            Console.WriteLine(Move());

        } while (!player.Exit);
    }

    static string Move()
    {
        string message = "";
        ConsoleKeyInfo key = Console.ReadKey();
        const double moveStep = 5.0;
        const int sensitivity = 10;
        double radianDirection = player.Direction * (Math.PI / 180);

        switch (key.Key)
        {
            case ConsoleKey.Q:
                player.Exit = true;
                break;
            case ConsoleKey.RightArrow:
                player.Direction += sensitivity;
                break;
            case ConsoleKey.LeftArrow:
                player.Direction -= sensitivity;
                break;
            case ConsoleKey.W:
                player.X += Convert.ToInt32(moveStep * Math.Cos(radianDirection));
                player.Y += Convert.ToInt32(moveStep * Math.Sin(radianDirection));
                break;
            case ConsoleKey.S:
                player.X -= Convert.ToInt32(moveStep * Math.Cos(radianDirection));
                player.Y -= Convert.ToInt32(moveStep * Math.Sin(radianDirection));
                break;
            case ConsoleKey.A:
                player.X += Convert.ToInt32(moveStep * Math.Sin(radianDirection));
                player.Y -= Convert.ToInt32(moveStep * Math.Cos(radianDirection));
                break;
            case ConsoleKey.D:
                player.X -= Convert.ToInt32(moveStep * Math.Sin(radianDirection));
                player.Y += Convert.ToInt32(moveStep * Math.Cos(radianDirection));
                break;
            case ConsoleKey.D0:
                player.Selected = 9;
                break;
            case ConsoleKey.D1:
                player.Selected = 0;
                break;
            case ConsoleKey.D2:
                player.Selected = 1;
                break;
            case ConsoleKey.D3:
                player.Selected = 2;
                break;
            case ConsoleKey.D4:
                player.Selected = 3;
                break;
            case ConsoleKey.D5:
                player.Selected = 4;
                break;
            case ConsoleKey.D6:
                player.Selected = 5;
                break;
            case ConsoleKey.D7:
                player.Selected = 6;
                break;
            case ConsoleKey.D8:
                player.Selected = 7;
                break;
            case ConsoleKey.D9:
                player.Selected = 8;
                break;
            case ConsoleKey.Spacebar:
                Items.Use(Items.AllItems[player.Selected].Id, EnemyMap);
                break; 
        }
        // Ensure the player stays within bounds
        player.X = Math.Clamp(player.X, 0, MapX);
        player.Y = Math.Clamp(player.Y, 0, MapY);
        return message;
    }
    static bool[,] DrawWallMap()
    {
        bool[,] map = new bool[MapX, MapY];
        for (int x = 0; x < MapX; x++)
        {
            for (int y = 0; y < MapY; y++)
            {
                map[x, y] = false;
            }
        }
        
        map[25, 25] = true;
        map[75, 75] = true;
        
        return map;
    }
    
    static bool[,] DrawEnemyMap()
    {
        bool[,] map = new bool[MapX, MapY];
        for (int x = 0; x < MapX; x++)
        {
            for (int y = 0; y < MapY; y++)
            {
                map[x, y] = false;
            }
        }

        map[50, 50] = true;
        
        return map;
    }

    static void Print()
    {
        Console.Clear();
        bool[,] screen = new bool[ScreenX, ScreenY];
        double oldDir = player.Direction;
        
        player.Direction -= (Fov / 2f);
        for (int x = 0; x <= ScreenX - 1; x++)
        {
            double distance = player.CastRay(WallMap, oldDir);
            double lineRatio = ((0.5*WallHeight) / distance * Math.Tan(0.5 * Fov));
            
            for (int y = 0; y <= ScreenY - 1; y++)
            {
                bool valid = (y > (ScreenY / 2f) - (0.5 * (ScreenY * lineRatio))) && (y < (ScreenY / 2f) + (0.5 * (ScreenY * lineRatio)));
                if (valid)
                {
                    screen[x, y] = true; 
                }
                else
                {
                    screen[x, y] = false;
                }
            }

            player.Direction += Convert.ToDouble(Fov) / Convert.ToDouble(ScreenX);
        }
        player.Direction = oldDir;

        StringBuilder frame = new StringBuilder();
        for (int y = 0; y < ScreenY; y++)
        {
            for (int x = 0; x < ScreenX; x++)
            {
                frame.Append(screen[x, y] ? "█" : " ");
            }
            frame.AppendLine();
        }

        Console.Write(frame.ToString());
        Console.WriteLine();
        Console.WriteLine("██████████");
        Console.WriteLine("█");
        Console.WriteLine("█   " + Items.AllItems[player.Selected].Name);
        Console.WriteLine("█");
        Console.WriteLine("██████████");
    }
}