using System;
using System.Globalization;

/// <summary>
/// Базовый абстрактный класс для всех 3D-фигур.
/// </summary>
public abstract class Shape3D : IDisposable
{
    private bool _disposed;

    /// <summary>
    /// Проверяет, находится ли точка внутри фигуры.
    /// </summary>
    public abstract bool IsPointInside(double x, double y, double z);

    /// <summary>
    /// Освобождение ресурсов (упрощённый Dispose-паттерн).
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        // Здесь могли бы освобождаться ресурсы при наличии
        _disposed = true;
    }
}

/// <summary>
/// Класс «Сфера».
—/summary>
public class Sphere : Shape3D
{
    public double Radius { get; }
    public double CenterX { get; }
    public double CenterY { get; }
    public double CenterZ { get; }

    public Sphere(double radius, double x, double y, double z)
    {
        if (radius < 0)
            throw new ArgumentException("Радиус не может быть отрицательным.", nameof(radius));

        Radius = radius;
        CenterX = x;
        CenterY = y;
        CenterZ = z;
    }

    public override bool IsPointInside(double x, double y, double z)
    {
        if (double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z))
            return false;

        double dx = x - CenterX;
        double dy = y - CenterY;
        double dz = z - CenterZ;

        double distanceSquared = dx * dx + dy * dy + dz * dz;

        // Можно учитывать погрешность double:
        const double epsilon = 1e-10;

        return distanceSquared <= Radius * Radius + epsilon;
    }

    protected override void Dispose(bool disposing)
    {
        // Никаких ресурсов нет, но структура метода правильная
        base.Dispose(disposing);
    }
}

public static class Program
{
    public static void Main()
    {
        using Sphere sphere = new Sphere(5.0, 0.0, 0.0, 0.0);

        Console.WriteLine("Введите координаты точки (x, y, z):");

        double x = ReadDouble("x = ");
        double y = ReadDouble("y = ");
        double z = ReadDouble("z = ");

        bool inside = sphere.IsPointInside(x, y, z);

        Console.WriteLine(
            inside
            ? "Точка находится внутри сферы или на её поверхности."
            : "Точка находится вне сферы."
        );
    }

    /// <summary>
    /// Безопасное считывание double с проверкой ошибок.
    /// </summary>
    private static double ReadDouble(string message)
    {
        while (true)
        {
            Console.Write(message);

            string? input = Console.ReadLine();

            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
                return result;

            Console.WriteLine("Ошибка ввода! Введите число в формате 0.0");
        }
    }
}
