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

        /// <summary>
        /// 重心から最も離れたメンバーのインデックスを返す。
        /// </summary>
        /// <returns>重心から最も離れたメンバーのインデックス</returns>
        public int GetRemoteMemberIndex()
        {
            double maxD = -1;
            int maxIndex = 0;
            for (int i = 0; i < Members.Length; i++)
            {
                double d = Distance(Members[i], Centroid);
                if (d > maxD)
                {
                    maxD = d;
                    maxIndex = i;
                }
            }
            return maxIndex;
        }

        /// <summary>
        /// 指定インデックスのメンバーを削除する。
        /// </summary>
        /// <param name="index">削除するメンバーのインデックス</param>
        public void RemoveMemberAt(int index)
        {
            try
            {
                Vector.Vector[] m = new Vector.Vector[Members.Length];
                Members.CopyTo(m, 0);
                Members = new Vector.Vector[m.Length - 1];
                for (int i = 0, j = 0; i < m.Length; i++)
                {
                    if (i != index)
                    {
                        Members[j] = m[i];
                        j++;
                    }
                }
                Centroid = new Vector.Vector(m[0].Elements.Length);
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

        /// <summary>
        /// 2つのベクトルのユークリッド距離を求める。
        /// </summary>
        /// <param name="x">ベクトル</param>
        /// <param name="y">ベクトル</param>
        /// <returns>ユークリッド距離</returns>
        private double Distance(Vector.Vector x, Vector.Vector y)
        {
            double sum = 0.0;
            for (int i = 0; i < x.Dimension; i++)
            {
                sum += Math.Pow(x.Elements[i] - y.Elements[i], 2.0);
            }
            return Math.Sqrt(sum);
        }
    }
}
