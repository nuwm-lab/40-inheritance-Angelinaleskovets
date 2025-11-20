using System;

/// <summary>
/// Абстрактний базовий клас для тривимірних фігур.
/// Служить основою для реалізації різних 3D-об’єктів.
/// </summary>
public abstract class Shape3D
{
    /// <summary>
    /// Метод, який визначає, чи знаходиться точка всередині фігури.
    /// </summary>
    public abstract bool IsPointInside(double x, double y, double z);
}

/// <summary>
/// Клас, що представляє сферу.
/// </summary>
public class Sphere : Shape3D
{
    /// <summary> Радіус сфери. </summary>
    public double Radius { get; }

    /// <summary> Координати центру сфери. </summary>
    public double CenterX { get; }
    public double CenterY { get; }
    public double CenterZ { get; }

    /// <summary>
    /// Конструктор сфери.
    /// </summary>
    /// <param name="radius">Радіус (має бути невід’ємним).</param>
    /// <param name="x">X-координата центру.</param>
    /// <param name="y">Y-координата центру.</param>
    /// <param name="z">Z-координата центру.</param>
    public Sphere(double radius, double x, double y, double z)
    {
        if (radius < 0)
            throw new ArgumentException("Радіус не може бути від'ємним.", nameof(radius));

        Radius = radius;
        CenterX = x;
        CenterY = y;
        CenterZ = z;
    }

    /// <summary>
    /// Визначає, чи задана точка належить сфері.
    /// </summary>
    public override bool IsPointInside(double x, double y, double z)
    {
        double dx = x - CenterX;
        double dy = y - CenterY;
        double dz = z - CenterZ;

        double distanceSquared = dx * dx + dy * dy + dz * dz;
        double radiusSquared = Radius * Radius;

        return distanceSquared <= radiusSquared;
    }
}

/// <summary>
/// Головний клас програми.
/// </summary>
public static class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Перевірка належності точки сфері ===");
        Console.WriteLine("Сфера: центр (0,0,0), радіус = 5\n");

        // Поліморфізм: змінна базового типу містить сферу
        Shape3D sphere = new Sphere(5.0, 0.0, 0.0, 0.0);

        double x = ReadDouble("Введіть x: ");
        double y = ReadDouble("Введіть y: ");
        double z = ReadDouble("Введіть z: ");

        Console.WriteLine();

        if (sphere.IsPointInside(x, y, z))
            Console.WriteLine("➡ Точка знаходиться всередині сфери або на її поверхні.");
        else
            Console.WriteLine("⬅ Точка знаходиться поза сферою.");

        Console.WriteLine("\nГотово.");
    }

    /// <summary>
    /// Спроба безпечного зчитування числа з клавіатури.
    /// </summary>
    private static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (double.TryParse(input, out double value))
                return value;

            Console.WriteLine("Помилка вводу. Спробуйте ще раз.");
        }
    }
}
