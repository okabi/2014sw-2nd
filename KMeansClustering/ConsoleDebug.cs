using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            KMeansClustering clustering = new KMeansClustering(2);
            List<Vector.Vector> vectors = new List<Vector.Vector>();
            vectors.Add(new Vector.Vector(new double[] { 1.0, 2.0 }));
            vectors.Add(new Vector.Vector(new double[] { 3.0, 1.0 }));
            vectors.Add(new Vector.Vector(new double[] { -3.0, -1.0 }));
            vectors.Add(new Vector.Vector(new double[] { 2.0, 0.0 }));
            vectors.Add(new Vector.Vector(new double[] { 2.0, -1.0 }));
            vectors.Add(new Vector.Vector(new double[] { 3.0, -1.0 }));
            vectors.Add(new Vector.Vector(new double[] { -2.0, -2.0 }));
            vectors.Add(new Vector.Vector(new double[] { 0.0, 0.0 }));
            vectors.Add(new Vector.Vector(new double[] { -3.0, 3.0 }));
            List<Cluster> result = clustering.Execute(vectors);
            Console.WriteLine("最終結果");
            foreach (Cluster elem in result)
            {
                Console.WriteLine("重心 => " + elem.Centroid.ToString());
                for (int i = 0; i < elem.Members.Length; i++)
                {
                    Console.WriteLine("メンバー" + i +  " => " + elem.Members[i].ToString());
                }
            }

            Console.ReadKey();
        }
    }
}
