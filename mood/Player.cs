namespace mood;

public class Player
{
    public int X = 5;
    public int Y = 95;
    public double Direction = 300;
    public bool Exit = false;
    // public int Health = 100;
    // public string Name = "";
    // public int Level = 1;
    // public int Xp = 0;
    public int Selected = 0;

    public double CastRay(bool[,] map, double centre)
    {
        PrecomputeTrigValues(Direction, out double cosDir, out double sinDir);

        double mapX = X;
        double mapY = Y;
        double sideDistX, sideDistY;
        double deltaDistX = Math.Abs(1 / cosDir);
        double deltaDistY = Math.Abs(1 / sinDir);
        double perpWallDist;
        double stepX, stepY;
        bool hit = false;
        int side = 0;

        if (cosDir < 0)
        {
            stepX = -1;
            sideDistX = (X - mapX) * deltaDistX;
        }
        else
        {
            stepX = 1;
            sideDistX = (mapX + 1.0 - X) * deltaDistX;
        }

        if (sinDir < 0)
        {
            stepY = -1;
            sideDistY = (Y - mapY) * deltaDistY;
        }
        else
        {
            stepY = 1;
            sideDistY = (mapY + 1.0 - Y) * deltaDistY;
        }

        while (!hit)
        {
            if (sideDistX < sideDistY)
            {
                sideDistX += deltaDistX;
                mapX += stepX;
                side = 0;
            }
            else
            {
                sideDistY += deltaDistY;
                mapY += stepY;
                side = 1;
            }

            if (mapX < 0 || mapX >= map.GetLength(0) || mapY < 0 || mapY >= map.GetLength(1))
            {
                return -1; // Ray goes out of bounds
            }

            if (map[(int)mapX, (int)mapY])
            {
                hit = true;
            }
        }

        if (side == 0)
        {
            perpWallDist = (mapX - X + (1 - stepX) / 2) / cosDir;
        }
        else
        {
            perpWallDist = (mapY - Y + (1 - stepY) / 2) / sinDir;
        }

        double distance = perpWallDist;
        if (distance == double.MaxValue)
        {
            distance = -1;
        }
        else
        {
            distance *= Math.Cos((centre - Direction) * (Math.PI / 180));
        }
        return distance;
    }
    private static void PrecomputeTrigValues(double direction, out double cosDir, out double sinDir)
    {
        double radianDirection = direction * (Math.PI / 180);
        cosDir = Math.Cos(radianDirection);
        sinDir = Math.Sin(radianDirection);
    }
}
