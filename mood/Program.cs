using System.Text;

namespace mood;

public static class Program {
    
    private static Player player = new Player();
    private const int fov = 90;
    private const int wallHeight = 30;
    private const int screenX = 256;
    private const int screenY = 35;

    static void Main()
    {
        bool[,] map = DrawMap();
        Items.Init();
        do
        {
            Print(map);
            Console.WriteLine(Move());

        } while (!player.exit);
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
                player.exit = true;
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
                message = player.UseItem();
                break; 
        }
        // Ensure the player stays within bounds
        player.X = Math.Clamp(player.X, 0, 100);
        player.Y = Math.Clamp(player.Y, 0, 100);
        return message;
    }
    static bool[,] DrawMap()
    {
        
        const int mapX = 100;
        const int mapY = 100;
        bool[,] map = new bool[mapX, mapY];

        for (int x = 0; x < mapX; x++)
        {
            for (int y = 0; y < mapY; y++)
            {
                map[x, y] = false;
            }
        }
        
        map[25, 25] = true;
        map[75, 75] = true;
        
        return map;
    }

    static void Print(bool[,] map)
    {
        Console.Clear();
        bool[,] screen = new bool[screenX, screenY];
        double oldDir = player.Direction;
        
        player.Direction -= (fov / 2f);
        for (int x = 0; x <= screenX - 1; x++)
        {
            double distance = player.CastRay(map, oldDir);
            double lineRatio = ((0.5*wallHeight) / distance * Math.Tan(0.5 * fov));
            
            for (int y = 0; y <= screenY - 1; y++)
            {
                bool valid = (y > (screenY / 2f) - (0.5 * (screenY * lineRatio))) && (y < (screenY / 2f) + (0.5 * (screenY * lineRatio)));
                if (valid)
                {
                    screen[x, y] = true; 
                }
                else
                {
                    screen[x, y] = false;
                }
            }

            player.Direction += Convert.ToDouble(fov) / Convert.ToDouble(screenX);
        }
        player.Direction = oldDir;

        StringBuilder frame = new StringBuilder();
        for (int y = 0; y < screenY; y++)
        {
            for (int x = 0; x < screenX; x++)
            {
                frame.Append(screen[x, y] ? "█" : " ");
            }
            frame.AppendLine();
        }

        Console.Write(frame.ToString());
        Console.WriteLine();
        Console.WriteLine("██████████");
        Console.WriteLine("█");
        Console.WriteLine("█   " + Items.allItems[player.Selected].Name);
        Console.WriteLine("█");
        Console.WriteLine("██████████");
    }
}