using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeansClustering
{
    /// <summary>K平均法でベクトルをクラスタリングするクラス。</summary>
    public class KMeansClustering
    {
        /// <summary>分割するクラスタ数</summary>
        private int clusterNum;

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="num">分割するクラスタ数</param>
        public KMeansClustering(int num)
        {
            clusterNum = num;
        }

        /// <summary>
        /// 指定されたベクトル群を指定したクラスタ数でクラスタリングする。
        /// </summary>
        /// <param name="vectors">クラスタリングするベクトル群</param>
        /// <returns>ClusterクラスのList</returns>
        public List<Cluster> Execute(List<Vector.Vector> vectors)
        {
            List<Vector.Vector>[] vs = new List<Vector.Vector>[clusterNum];
            for (int i = 0; i < vs.Length; i++)
            {
                vs[i] = new List<Vector.Vector>();
            }

            // 各ベクトルに適当にクラスタ番号を振る
            for (int i = 0; i < vectors.Count; i++)
            {
                vs[i % clusterNum].Add(vectors[i]);
            }

            // クラスタリングを実行する。
            List<Cluster> cluster = new List<Cluster>();
            for (int i = 0; i < vs.Length; i++)
            {
                Cluster c = new Cluster(vs[i].ToArray());
                cluster.Add(c);
            }
            return Clustering(cluster, 0, 1);
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

        /// <summary>
        /// クラスタリングを繰り返す再帰関数。
        /// </summary>
        /// <param name="cluster">現在のクラスタリスト</param>
        /// <param name="threshold">再割り当てを許す回数</param>
        /// <returns>クラスタリング結果</returns>
        private List<Cluster> Clustering(List<Cluster> cluster, int threshold = 0, int count = 0)
        {
            List<Vector.Vector>[] nextClusterVector = new List<Vector.Vector>[clusterNum];
            for (int i = 0; i < nextClusterVector.Length; i++)
            {
                nextClusterVector[i] = new List<Vector.Vector>();
            }
            int reclusterCount = 0;

            // 各ベクトルとクラス重心との距離を求め、再割り当てを行う。
            for (int i = 0; i < cluster.Count; i++)
            {
                foreach (Vector.Vector vec in cluster[i].Members)
                {
                    double minD = Distance(vec, cluster[i].Centroid);
                    int nextCluster = i;
                    for (int j = 0; j < cluster.Count; j++)
                    {
                        double d = Distance(vec, cluster[j].Centroid);
                        if (d < minD)
                        {
                            minD = d;
                            nextCluster = j;
                        }
                    }
                    if (nextCluster != i)
                    {
                        reclusterCount++;
                    }
                    nextClusterVector[nextCluster].Add(vec);
                }
            }

            // 空クラスタが生じた場合、最初に見つけた空でないクラスタ内の、重心から最も遠いデータを空クラスタに移動させる。
            List<int> emptyClusterIndex = new List<int>();
            for (int i = 0; i < nextClusterVector.Length; i++)
            {
                if (nextClusterVector[i].Count == 0)
                {
                    emptyClusterIndex.Add(i);
                }
            }
            if (emptyClusterIndex.Count > 0)
            {
                List<Cluster> clusterList = new List<Cluster>();
                for (int i = 0; i < nextClusterVector.Length; i++)
                {
                    Cluster c = new Cluster(nextClusterVector[i].ToArray());
                    clusterList.Add(c);
                }
                for (int i = 0; i < emptyClusterIndex.Count; i++)
                {
                    for (int j = 0; j < clusterList.Count; j++)
                    {
                        if (clusterList[j].Members.Length > 1)
                        {
                            int index = clusterList[j].GetRemoteMemberIndex();
                            Vector.Vector v = new Vector.Vector(clusterList[j].Members[index].Elements);
                            clusterList[emptyClusterIndex[i]] = new Cluster(new Vector.Vector[] { v });
                            clusterList[j].RemoveMemberAt(index);
                        }
                    }
                }
                return Clustering(clusterList, threshold, count + 1);
            }
            // 再割り当てしたベクトルの個数が閾値以下ならば終了。
            else if (reclusterCount <= threshold)
            {
                return cluster;
            }
            else
            {
                List<Cluster> clusterList = new List<Cluster>();
                for (int i = 0; i < nextClusterVector.Length; i++)
                {
                    Cluster c = new Cluster(nextClusterVector[i].ToArray());
                    clusterList.Add(c);
                }
                return Clustering(clusterList, threshold, count + 1);
            }
        }
    }
}
