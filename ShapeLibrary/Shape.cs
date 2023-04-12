using static System.Math;

namespace ShapeLibrary
{
    public struct Vertex
    {
        public double X { get; }
        public double Y { get; }

        public Vertex(double x, double y)
        {
            X = x; Y = y;
        }
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }

    public interface IShape //базовая фигура с базовой функциональностью
    {
        public abstract double GetArea();
    }

    /// <summary>
    /// Класс для представления простых многоугольников (где грани не пересекаются друг с другом)
    /// </summary>
    public class SimplePolygon : IShape
    {
        public readonly List<Vertex> vertices = new List<Vertex>();

        protected SimplePolygon()
        {
        }
        public SimplePolygon(List<Vertex> v)
        {
            vertices.AddRange(v);
        }

        public double GetArea()
        {
            double area = 0;
            int j = vertices.Count - 1;
            for (int i = 0; i < vertices.Count; i++)
            {
                area += (vertices[j].X + vertices[i].X) * (vertices[j].Y - vertices[i].Y);
                j = i; //j - предыдущая по одношению к i вершина
            }
            return Abs(area/2);
        }
    }

    public class Triangle : SimplePolygon
    {
        //sides of a triangle
        public readonly double a = -1;
        public readonly double b = -1;
        public readonly double c = -1;

        public bool IsRight //является ли треугольник прямоугольным
        {
            get
            {
                double maxSide = Max(Max(a, b), c); //поиск потенциальной гипотенузы
                return a * a + b * b + c * c == 2 * maxSide * maxSide; //проверка через теорему Пифагора
            }
        }
        public Triangle(List<Vertex> v) : base(v) //создать треугольник через координаты вершин
        {
            if (!IsTriangle(v))
                throw new NotTriangleException();

            a = Sqrt(Pow((v[1].Y - v[0].Y), 2) + Pow((v[1].X - v[0].X), 2)); // |AB|² = (y2 - y1)² + (x2 - x1)²
            b = Sqrt(Pow((v[2].Y - v[1].Y), 2) + Pow((v[2].X - v[1].X), 2));
            c = Sqrt(Pow((v[2].Y - v[0].Y), 2) + Pow((v[2].X - v[0].X), 2));
        }

        public Triangle(double a, double b, double c)  //создать треугольник через стороны
        {
            if (!IsTriangle(a, b ,c))
                throw new NotTriangleException();
            this.a = a;
            this.b = b;
            this.c = c;

            double p = (a + b + c) / 2; //полупериметр
            double hA = 2 / a * Sqrt(p * (p - a) * (p - b) * (p - c)); //вычисление высоты через формулу Герона

            vertices.Add(new Vertex(0, 0));
            vertices.Add(new Vertex(0, a));
            vertices.Add(new Vertex(Sqrt(b*b - hA*hA), hA));
        }

        private bool IsTriangle(double a, double b, double c)
        {
            return a > 0 && b > 0 && c > 0 && a + b > c && a + c > b && b + c > a;
        }
        private bool IsTriangle(List<Vertex> v)
        {
            return v.Count == 3 && //вершин 3
                (v[2].X - v[0].X) * (v[1].Y - v[0].Y) != (v[2].Y - v[0].Y) * (v[1].X - v[0].X); //точки не лежат на одной прямой
        }

        public new double GetArea()
        {
            double p = (a + b + c) / 2; //полупериметр
            return Sqrt(p * (p - a) * (p - b) * (p - c)); //расчет площади треугольника по формуле Герона
        }
    }

    public class Circle : IShape
    {
        public Vertex Center { get; }
        public double Radius { get; }
        public Circle(Vertex center, double radius)
        {
            if (!IsCircle(radius))
                throw new NotCircleException();
            Center = center;
            Radius = radius;
        }
        public Circle(double radius) : this(new Vertex(0, 0), radius)
        { }
        private bool IsCircle(double radius)
        {
            return radius > 0;
        }

        public double GetArea()
        {
            return double.Pi * Radius * Radius; //или Math.Pow(Radius, 2)
        }
    }

    #region Exceptions
    public class NotTriangleException : Exception
    {
       public NotTriangleException() : base("The given Shape is not a Triangle!")
        { }
    }
    public class NotCircleException : Exception
    {
        public NotCircleException() : base("The given Shape is not a Circle!")
        { }
    }
    #endregion


}