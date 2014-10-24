using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // 入力例の結果を出力
            Vector x = new Vector(new double[] { 1.0, 2.0 });
            Vector y = new Vector(new double[] { 3.0, 1.0 });
            Vector z = x.Add(y);
            Vector a = new Vector(2);
            a = a.Add(x);
            Vector b = a.ScalarMultiply(2.0);
            double c = b.InnerProduct(z);
            Vector d = x.Sub(y);
            Vector e = new Vector(new double[] { 1.0, 2.0, 3.0 });
            Vector f = new Vector(new double[] { 3.0, 2.0, 1.0 });
            Vector g = e.Sub(f);
            Vector h = e.Sub(x); // error!
            Console.WriteLine("x => " + x.ToString());  // (1.0, 2.0)
            Console.WriteLine("y => " + y.ToString());  // (3.0, 1.0)
            Console.WriteLine("z => " + z.ToString());  // (4.0, 3.0)
            Console.WriteLine("a => " + a.ToString());  // (1.0, 2.0)
            Console.WriteLine("b => " + b.ToString());  // (2.0, 4.0)
            Console.WriteLine("c => " + c.ToString());  // 20.0
            Console.WriteLine("d => " + d.ToString());  // (-2.0, 1.0)
            Console.WriteLine("e => " + e.ToString());  // (1.0, 2.0, 3.0)
            Console.WriteLine("f => " + f.ToString());  // (3.0, 2.0, 1.0)
            Console.WriteLine("g => " + g.ToString());  // (-2.0, 0.0, 2.0)
            Console.WriteLine("h => " + h.ToString());  // (1.0, 2.0)

            // Enterで終了
            Console.ReadKey();
        }
    }
}
