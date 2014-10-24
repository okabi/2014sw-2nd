using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    /// <summary>クラスタを表すクラス。</summary>
    public class Cluster
    {
        /// <summary>クラスタの重心</summary>
        public Vector.Vector Centroid { get; private set; }
        /// <summary>クラスタを構成するベクトル群</summary>
        public Vector.Vector[] Members { get; private set; }

        /// <summary>
        /// 与えられたベクトルリストから1つのクラスタを形成する。
        /// </summary>
        /// <param name="members">クラスタを構成するベクトル群</param>
        public Cluster(Vector.Vector[] members)
        {
            try
            {
                Members = new Vector.Vector[members.Length];
                members.CopyTo(Members, 0);
                int dimension = members[0].Dimension;
                Centroid = new Vector.Vector(dimension);
                foreach (Vector.Vector elem in Members)
                {
                    Centroid = Centroid.Add(elem);
                }
                Centroid = Centroid.ScalarMultiply(1.0 / (double)Members.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
