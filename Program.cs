using System;

/// <summary>
/// Базовий абстрактний клас для 3D-фігур.
/// </summary>
public abstract class Shape3D
{
    /// <summary>
    /// Перевіряє, чи знаходиться точка всередині фігури.
    /// </summary>
    public abstract bool IsPointInside(double x, double y, double z);
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
        if (radius < 0)
            throw new ArgumentException("Радіус не може бути від'ємним.", nameof(radius));

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
}

public static class Program
{
    public static void Main()
    {
        Console.WriteLine("Введіть координати точки (x, y, z):");

        double x = ReadDouble("x = ");
        double y = ReadDouble("y = ");
        double z = ReadDouble("z = ");

        Shape3D sphere = new Sphere(5.0, 0.0, 0.0, 0.0);

        bool inside = sphere.IsPointInside(x, y, z);

        Console.WriteLine(inside
            ? "Точка знаходиться всередині кулі або на її поверхні."
            : "Точка знаходиться поза кулею.");
    }

    private static double ReadDouble(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (double.TryParse(input, out double value))
                return value;

            Console.WriteLine("Помилка! Введіть коректне число.");
        }
    }
}
