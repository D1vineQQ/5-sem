using System.Diagnostics;

namespace aboba
{
    class Program
    {
        static Double MySleep(int ms)
        {
            Double Sum = 0, Temp;
            for (int t = 0; t < ms; ++t)
            {
                Temp = 0.711 + (Double)t / 10000.0;
                Double a, b, c, d, e, nt;
                for (int k = 0; k < 9325; ++k)
                {
                    nt = Temp - k / 27000.0;
                    a = Math.Sin(nt);
                    b = Math.Cos(nt);
                    c = Math.Cos(nt / 2.0);
                    d = Math.Sin(nt / 2);
                    e = Math.Abs(1.0 - a * b) + Math.Abs(1.0 - c * c - d * d) + Math.Sqrt(Math.Pow(1.445d, Math.Log10((b / d / a))));
                    e = Math.Abs(1.0 - a - b * b) + Math.Pow(1.5d, 1.5);
                    Sum += e;
                }
            }
            return Sum;
        }
        static void Main()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            //Console.WriteLine("start");
            MySleep(115);
            //Console.WriteLine("stop");
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}