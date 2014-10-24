using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            Complex a = new Complex(1.0, 2.0);
            Complex b = new Complex("4.0+1.0i");
            Complex c = new Complex(1.414, (float)(-Math.PI / 4.0));  // 1.0 - 1.0i
            Complex d = a.Add(b);  // 5.0 + 3.0i
            Complex e = a.Subtract(b);  // -3.0 + 1.0i
            Complex f = a.Multiply(b);  // 2.0 + 9.0i
            Complex g = a.Clone();  // 1.0 + 2.0i
            Console.WriteLine(a.ToString());
            Console.WriteLine(b.ToString());
            Console.WriteLine(c.ToString());
            Console.WriteLine(d.ToString());
            Console.WriteLine(e.ToString());
            Console.WriteLine(f.ToString());
            Console.WriteLine(g.ToString());

            // Enterで終了
            Console.ReadKey();
        }
    }
}
