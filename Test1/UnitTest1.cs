using ShapeLibrary;

namespace TestShapeSpace
{
    [TestFixture]
    public class TestShapeClass
    {
        private const double DELTA = 0.0000000001; //приемлемая для тестов ошибка сравнения чисел double

        [SetUp]
        public void Setup()
        { }
        
        #region TriangleTests
        [Test]
        public void CreateTriangleTest1() //тест создания треугольника с некорректными длинами сторон
        {

            Assert.Catch<NotTriangleException>(() => new Triangle(3, 4, 8)); //сумма двух сторон меньше третьей
            Assert.Catch<NotTriangleException>(() => new Triangle(3, 4, 7)); //сумма двух сторон равна третьей 
            Assert.Catch<NotTriangleException>(() => new Triangle(3, -4, 5)); //одна из сторон отрицательная 
            Assert.Catch<NotTriangleException>(() => new Triangle(0, 4, 5)); //одна из сторон имеет нулевую длину 
        }
        [Test]
        public void CreateTriangleTest2() //тест создания треугольника с неправильными координатами
        {         
            Assert.Catch<NotTriangleException>(() => new Triangle(new List<Vertex>() { new Vertex(0, 0), new Vertex(1, 1), new Vertex(2, 2) })); //три точки на одной прямой
            Assert.Catch<NotTriangleException>(() => new Triangle(new List<Vertex>() { new Vertex(0, 0), new Vertex(0, 1), new Vertex(0, 2) })); //три точки на оси
            Assert.Catch<NotTriangleException>(() => new Triangle(new List<Vertex>() { new Vertex(0, 0), new Vertex(1, 1), new Vertex(1, 1) })); //две точки одинаковые
            Assert.Catch<NotTriangleException>(() => new Triangle(new List<Vertex>() { new Vertex(0, 0), new Vertex(0, 0), new Vertex(0, 0) })); //три точки одинаковые
        }

        [Test] 
        public void CalculationAccuracyTest() //тест на достаточность точности вычислений
        {
            //создаю два равных треугольника разными способами
            var tr1 = new Triangle(3, 4, 5);
            var tr2 = new Triangle(new List<Vertex>() { new Vertex(0.5, 0.5), new Vertex(0.5, 3.5), new Vertex(4.5, 0.5) }); //в этом треугольнике стороны рассчитываются в конструкторе
            double[] tr1Sides = {tr1.a, tr1.b, tr1.c};
            double[] tr2Sides = {tr2.a, tr2.b, tr2.c};
            Array.Sort(tr1Sides);
            Array.Sort(tr2Sides);

            Assert.AreEqual(tr1Sides[0], tr2Sides[0], DELTA);
            Assert.AreEqual(tr1Sides[1], tr2Sides[1], DELTA);
            Assert.AreEqual(tr1Sides[2], tr2Sides[2], DELTA);
            Assert.AreEqual(tr1.GetArea(), tr2.GetArea(), DELTA);
        }

        [Test]
        public void RightAngleTriangleTest1() //проверяет прямоугольность треугольника, заданного через стороны
        {
            var rightTr = new Triangle(3, 4, 5);
            Assert.IsTrue(rightTr.IsRight);
        }
        [Test]
        public void RightAngleTriangleTest2() //проверяет прямоугольность треугольника, заданного через координаты
        {
            var rightTr = new Triangle(new List<Vertex>() { new Vertex(0, 0), new Vertex(0, 3), new Vertex(4, 0) });
            Assert.IsTrue(rightTr.IsRight);
        }
        [Test]
        public void TriangleAreaTest() 
        {
            var tr = new Triangle(3, 4, 5);
            Assert.AreEqual(6, tr.GetArea(), DELTA);
        }
        #endregion

        #region SimplePolygonTests
        [Test]
        public void PolygonAreaTest()  
        {
            var tr = new Triangle(new List<Vertex>() { new Vertex(0.5, 0.5), new Vertex(0.5, 3.5), new Vertex(4.5, 0.5) }); 
            var tr_sp = new SimplePolygon(new List<Vertex>() { new Vertex(0.5, 0.5), new Vertex(0.5, 3.5), new Vertex(4.5, 0.5) }); //треугольник как частный случай многоугольника
            var sq = new SimplePolygon(new List<Vertex>() { new Vertex(0, 0), new Vertex(3, 4), new Vertex(6, 4), new Vertex(3, 0) }); //параллелограмм
            Assert.AreEqual(6, tr_sp.GetArea(), DELTA);
            Assert.AreEqual(tr.GetArea(), tr_sp.GetArea(), DELTA);
            Assert.AreEqual(12, sq.GetArea(), DELTA);
        }
        #endregion

        #region CircleTests
        [Test]
        public void CreateCircleTest()
        {
            Assert.Catch<NotCircleException>(() => new Circle(-4));
            Assert.Catch<NotCircleException>(() => new Circle(0));
        }
        [Test]
        public void CircleAreaTest()
        {
            var circle = new Circle(3);
            Assert.AreEqual(double.Pi * 9, circle.GetArea(), DELTA);
        }
        #endregion
    }
}