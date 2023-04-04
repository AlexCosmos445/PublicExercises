using ShapeLibrary;
public class Program
{
    public static void Main(string[] args)
    {
        //var v = new List<Vertex>() { new Vertex (0,0) , new Vertex(1,1), new Vertex(2,2)};
        //var v = new List<Vertex>() { new Vertex (0,0) , new Vertex(0,3), new Vertex(4,0)};
        var v = new List<Vertex>() { new Vertex (0,0) , new Vertex(0,0), new Vertex(0,3)};
        
        SimplePolygon sp = new SimplePolygon(v);
        //Triangle tr = new Triangle(v);
        Console.WriteLine(sp.GetArea());
        //Console.WriteLine(tr.GetArea());
        //Console.WriteLine(sp.IsRight);
    }
}
