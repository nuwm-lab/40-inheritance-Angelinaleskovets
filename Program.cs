using System;

/// <summary>
/// Базовий клас для 3D-фігур.
/// </summary>
public abstract class Shape3D : IDisposable
{
    public abstract bool IsPointInside(double x, double y, double z);

    public virtual void Dispose()
    {
        // Якщо були б ресурси — тут вони б вивільнялися
    }
}

/// <summary>
/// Клас сфери.
/// </summary>
public class Sphere : Shape3D
{
    public double Radius { get; }
    public double CenterX { get; }
    public double CenterY { get; }
    public double CenterZ { get; }

    public Sphere(double radius, double x, double y, double z)
    {
        Radius = radius;
        CenterX = x;
        CenterY = y;
        CenterZ = z;
    }

    public override bool IsPointInside(double x, double y, double z)
    {
        double dx = x - CenterX;
        double dy = y - CenterY;
        double dz = z - CenterZ;
        double distanceSquared = dx * dx + dy * dy + dz * dz;
        return distanceSquared <= Radius * Radius;
    }

    public override void Dispose()
    {
        // Тут могла б бути очистка ресурсів
        base.Dispose();
    }
}

public static class Program
{
    public static void Main()
    {
        using Sphere sphere = new Sphere(5.0, 0.0, 0.0, 0.0);

        Console.WriteLine("Введіть координати точки (x, y, z):");
        Console.Write("x = ");
        double x = Convert.ToDouble(Console.ReadLine());

        Console.Write("y = ");
        double y = Convert.ToDouble(Console.ReadLine());

        Console.Write("z = ");
        double z = Convert.ToDouble(Console.ReadLine());

        if (sphere.IsPointInside(x, y, z))
        {
            Console.WriteLine("Точка знаходиться всередині кулі або на її поверхні.");
        }
        else
        {
            Console.WriteLine("Точка знаходиться поза кулею.");
        }
    }
}

