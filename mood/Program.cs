﻿using System.Text;

namespace mood;

public static class Program {
    
    private static Player player = new Player();
    private static ConsoleColor background = ConsoleColor.Gray;
    private const int fov = 90;
    private const int wallHeight = 60;
    //screen = 50x20

    static void Main()
    {
        bool[,] map = DrawMap();
        do
        {
            Print(map);
            Move();

        } while (true);
    }

    static void Move()
    {
        ConsoleKeyInfo key = Console.ReadKey();
        const double moveStep = 5.0;
        const int sensitivity = 10;
        double radianDirection = player.Direction * (Math.PI / 180);

        switch (key.Key)
        {
            case ConsoleKey.Escape:
                return;
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
        }

        // Ensure the player stays within bounds
        player.X = Math.Clamp(player.X, 0, 100);
        player.Y = Math.Clamp(player.Y, 0, 100);
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
        const int screenX = 256;
        const int screenY = 40;
        bool[,] screen = new bool[screenX, screenY];
        double oldDir = player.Direction;
        
        
        player.Direction -= (fov / 2f);
        for (int x = 0; x <= screenX - 1; x++)
        {
            double step = Convert.ToDouble(fov) / Convert.ToDouble(screenX);
            double distance = player.CastRay(map);
            double halfLine = distance * Math.Tan(0.5 * fov);
            double lineRatio = ((0.5*wallHeight) / halfLine);

            
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

            player.Direction += step;
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
        Console.WriteLine(player.Direction);
    }
}