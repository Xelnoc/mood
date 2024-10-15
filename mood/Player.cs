namespace mood;

public class Player
{
    public int X = 5;
    public int Y = 95;
    public double Direction = 300;
    // public int Health = 100;
    // public string Name = "";
    // public int Level = 1;
    // public int Xp = 0;
    // public int[,] Items = { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } }; //item id, item count.

    public double CastRay(bool[,] map)
    {
    double xCalc, yCalc, distCalc, xStep, yStep, xInitial, yInitial, xDiff, yDiff;

    // Vertical collision detection
    xCalc = X + 0.5;
    yCalc = Y + 0.5;
    xStep = Math.Cos(Direction * (Math.PI / 180)) > 0 ? 1 : -1;
    yStep = Math.Tan(Direction * (Math.PI / 180)) * xStep;

    xInitial = xStep / 2;
    yInitial = Math.Tan(Direction * (Math.PI / 180)) * xInitial;
    xCalc += xInitial;
    yCalc += yInitial;

    if (xCalc > map.GetLength(0) - 1) xCalc = map.GetLength(0) - 1;
    else if (xCalc < 0) xCalc = 0;

    if (yCalc > map.GetLength(1) - 1) yCalc = map.GetLength(1) - 1;
    else if (yCalc < 0) yCalc = 0;

    if (!map[Convert.ToInt32(xCalc), Convert.ToInt32(yCalc)])
    {
        bool collided = false;
        while (!collided)
        {
            xCalc += xStep;
            yCalc += yStep;
            if (yCalc <= 0 || yCalc >= map.GetLength(1) - 1 || xCalc < 0 || xCalc >= map.GetLength(0) - 1)
            {
                collided = true;
            }
            else if (map[Convert.ToInt32(xCalc), Convert.ToInt32(yCalc)])
            {
                collided = true;
            }
        }
    }
    xDiff = xCalc - X;
    distCalc = xDiff / Math.Cos(Direction * (Math.PI / 180));
    double vdistance = Math.Truncate(distCalc);

    // Horizontal collision detection
    xCalc = X + 0.5;
    yCalc = Y + 0.5;
    yStep = Math.Sin(Direction * (Math.PI / 180)) > 0 ? 1 : -1;
    xStep = yStep / Math.Tan(Direction * (Math.PI / 180));

    yInitial = yStep / 2;
    xInitial = yInitial / Math.Tan(Direction * (Math.PI / 180));
    xCalc += xInitial;
    yCalc += yInitial;

    if (xCalc > map.GetLength(0) - 1) xCalc = map.GetLength(0) - 1;
    else if (xCalc < 0) xCalc = 0;

    if (yCalc > map.GetLength(1) - 1) yCalc = map.GetLength(1) - 1;
    else if (yCalc < 0) yCalc = 0;

    if (!map[Convert.ToInt32(xCalc), Convert.ToInt32(yCalc)])
    {
        bool collided = false;
        while (!collided)
        {
            xCalc += xStep;
            yCalc += yStep;
            if (yCalc <= 0 || yCalc >= map.GetLength(1) - 1 || xCalc < 0 || xCalc >= map.GetLength(0) - 1)
            {
                collided = true;
            }
            else if (map[Convert.ToInt32(xCalc), Convert.ToInt32(yCalc)])
            {
                collided = true;
            }
        }
    }
    yDiff = yCalc - Y;
    distCalc = yDiff / Math.Sin(Direction * (Math.PI / 180));
    double hdistance = Math.Truncate(distCalc);

    double distance = Math.Min(Math.Abs(hdistance), Math.Abs(vdistance));
    return distance; 
    }
}
