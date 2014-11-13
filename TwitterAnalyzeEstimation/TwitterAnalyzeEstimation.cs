using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterAnalyzeEstimation
{
    /// <summary>Twitterを語句検索し、得られたツイートからその語句に対する評価を得るクラス。</summary>
    public class TwitterAnalyzeEstimation
    {
        /// <summary>Twitter接続を担当する</summary>
        private TwitterConnector.TwitterConnector connector;
        /// <summary>極性計算を担当する</summary>
        private SemanticOrientationEstimator.SemanticOrientationEstimator estimator;
        /// <summary>クラスタリングを担当する</summary>
        private KMeansClustering.KMeansClustering clustering;
        /// <summary>TF-IDF計算を担当する</summary>
        private TfIdf.TfIdf tfidf;
        /// <summary>[ツイート, 極性(整数値)]</summary>
        public List<string[]> tweetWithOrientation;
        /// <summary>極性平均</summary>
        public double Average { get; private set; }
        /// <summary>極性分散</summary>
        public double Variance { get; private set; }
        /// <summary>極性標準偏差</summary>
        public double StandardDeviation { get; private set; }
        /// <summary>極性によって3分割したクラスタ</summary>
        public List<KMeansClustering.Cluster> Cluster { get; private set; }
        /// <summary>クラスタごとの出現ワードランキング</summary>
        public List<List<KeyValuePair<string, double>>> WordRanking { get; private set; }


        /// <summary>
        /// 分析の準備を行う。
        /// </summary>
        /// <param name="screenName">利用者のTwitterのScreen Name</param>
        /// <param name="dicPath">極性辞書のパス</param>
        public TwitterAnalyzeEstimation(string screenName, string dicPath)
        {
            connector = new TwitterConnector.TwitterConnector(screenName);
            estimator = new SemanticOrientationEstimator.SemanticOrientationEstimator(dicPath);
            clustering = new KMeansClustering.KMeansClustering(3);
        }

        /// <summary>
        /// Twitter接続に必要なリクエストトークン発行画面へのURLを返す。
        /// </summary>
        /// <returns>リクエストトークン発行画面へのURL</returns>
        public string GetRequestUrl()
        {
            return connector.GetRequestUrl();
        }

        /// <summary>
        /// PINを入力してアクセストークンを得る。
        /// </summary>
        /// <param name="pin">PIN</param>
        public void GenerateAccessToken(string pin)
        {
            connector.GenerateAccessToken(pin);
        }

        /// <summary>
        /// TwitterSearchAPIを利用して、指定した語句を含むツイートを最大1000件取得する。
        /// </summary>
        /// <param name="q">検索語句</param>
        /// <param name="count">取得件数(100件ずつ指定可能)</param>
        /// <returns>指定した語句を含むツイート(最大1000件)</returns>
        public List<TwitterConnector.Tweet> Search(string q, int count)
        {
            List<TwitterConnector.Tweet> result = new List<TwitterConnector.Tweet>();
            count = count / 100;
            for (int i = 0; i < count + 1; i++)
            {
                long id = 0;
                if (result.Count > 0)
                {
                    id = result[result.Count - 1].Id;
                    result.RemoveAt(result.Count - 1);
                }
                int c = 100;
                if (i == count)
                {
                    c = count - 1;
                }
                result.AddRange(connector.GetSearchTweets(q, c, id));
            }
            return result;
        }

        /// <summary>
        /// ツイートのリストを極性(-100～100)のリストに変換する。
        /// </summary>
        /// <param name="tweet">ツイートのリスト</param>
        /// <returns></returns>
        public Dictionary<int, int> CreateOrientationList(List<TwitterConnector.Tweet> tweet)
        {
            tweetWithOrientation = new List<string[]>();
            Dictionary<int, int> result = new Dictionary<int, int>();
            for (int i = -100; i <= 100; i++)
            {
                result[i] = 0;
            }
            foreach (TwitterConnector.Tweet elem in tweet)
            {
                double orientation = estimator.Estimate(elem.Text);
                int iOrientation = (int)(orientation * 100.0);
                tweetWithOrientation.Add(new string[2] { elem.Text, iOrientation.ToString() });
                result[iOrientation] += 1;
            }
            return result;
        }

        /// <summary>
        /// グラフ表示用のJSファイルを吐き出す。
        /// </summary>
        /// <param name="q">検索語</param>
        /// <param name="jsPath">吐き出すJSファイルのパス</param>
        /// <param name="orientation">極性分布</param>
        /// <param name="tweet">デバッグで吐き出す、取得ツイート</param>
        public void CreateChartJavaScript(string q, string jsPath, Dictionary<int, int> orientation, List<TwitterConnector.Tweet> tweet = null)
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter("debug.txt"))
            {
                if (tweet != null)
                {
                    for (int i = 0; i < tweet.Count; i++)
                    {
                        file.WriteLine(tweet[i].Text);
                    }
                }
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(jsPath))
            {
                file.WriteLine("var titleTxt = '\"" + q + "\"ツイートの極性分布';");
                file.WriteLine("var dataArr = [['極性', 'ツイート件数'],");
                for (int i = -100; i <= 100; i++)
                {
                    string output = "\t\t[" + i.ToString() + ", " + orientation[i].ToString() + "]";
                    if (i < 100)
                    {
                        output += ",";
                    }
                    else
                    {
                        output += "];";
                    }
                    file.WriteLine(output);
                }
            }
        }

        /// <summary>
        /// 平均・分散・標準偏差を計算する。
        /// </summary>
        /// <param name="orientation">極性(-100~100) -> 件数 の辞書</param>
        public void CulculateParameters(Dictionary<int, int> orientation)
        {
            // 平均の計算
            int count = 0;
            double sum = 0;
            for (int i = -100; i <= 100; i++)
            {
                count += orientation[i];
                sum += i * orientation[i];
            }
            Average = sum / (double)count;
            // 分散の計算
            sum = 0;
            for (int i = -100; i <= 100; i++)
            {
                sum += Math.Pow((double)i - Average, 2.0) * orientation[i];
            }
            Variance = sum / (double)count;
            // 標準偏差の計算
            StandardDeviation = Math.Sqrt(Variance);
        }

        /// <summary>
        /// 極性分布を3クラスタに分割する。
        /// </summary>
        /// <param name="orientation">極性分布</param>
        public void Clustering(Dictionary<int, int> orientation)
        {
            List<Vector.Vector> vectors = new List<Vector.Vector>();
            for (int i = -100; i <= 100; i++ )
            {
                for (int j = 0; j < orientation[i]; j++)
                {
                    vectors.Add(new Vector.Vector(new double[1] { i }));
                }      
            }
            Cluster = clustering.Execute(vectors);
            WordRanking = new List<List<KeyValuePair<string, double>>>();
            WordRanking.Add(new List<KeyValuePair<string, double>>());
            WordRanking.Add(new List<KeyValuePair<string, double>>());
            WordRanking.Add(new List<KeyValuePair<string, double>>());
        }

        /// <summary>
        /// 分析結果表示用のJSファイルを吐き出す。
        /// </summary>
        /// <param name="jsPath">吐き出すJSファイルのパス</param>
        public void CreateResultJavaScript(string jsPath)
        {
            // クラスタ平均・分散・標準偏差の算出
            double[] average = new double[Cluster.Count];
            double[] variance = new double[Cluster.Count];
            double[] standardDeviation = new double[Cluster.Count];
            for (int i = 0; i < Cluster.Count; i++)
            {
                double sum = 0;
                for (int j = 0; j < Cluster[i].Members.Length; j++)
                {
                    sum += Cluster[i].Members[j].Elements[0];
                }
                average[i] = sum / (double)Cluster[i].Members.Length;
                sum = 0;
                for (int j = 0; j < Cluster[i].Members.Length; j++)
                {
                    sum += Math.Pow(Cluster[i].Members[j].Elements[0] - Average, 2.0);
                }
                variance[i] = sum / (double)Cluster[i].Members.Length;
                standardDeviation[i] = Math.Sqrt(variance[i]);
            }
            // JSファイルの吐き出し
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(jsPath))
            {
                file.WriteLine("var average = " + Average.ToString() + ";");
                file.WriteLine("var variance = " + Variance.ToString() + ";");
                file.WriteLine("var standardDeviation = " + StandardDeviation.ToString() + ";");
                file.WriteLine("var cluster = [");
                for (int i = 0; i < Cluster.Count; i++)
                {
                    file.WriteLine("\t{");
                    file.WriteLine("\t\t\"count\": " + Cluster[i].Members.Length.ToString() + ",");
                    file.WriteLine("\t\t\"average\": " + average[i].ToString() + ",");
                    file.WriteLine("\t\t\"variance\": " + variance[i].ToString() + ",");
                    file.WriteLine("\t\t\"standardDeviation\": " + standardDeviation[i].ToString() + ",");
                    file.WriteLine("\t\t\"wordRanking\": [");
                    for (int j = 0; j < WordRanking[i].Count; j++)
                    {
                        string output = "\t\t\t{ \"word\": \"" + WordRanking[i][j].Key + "\", \"value\": " + WordRanking[i][j].Value.ToString() + " }";
                        if (j < WordRanking[i].Count - 1)
                        {
                            output += ",";
                        }
                        else
                        {
                            if (i < Cluster.Count - 1)
                            {
                                output += "]},";
                            }
                            else
                            {
                                output += "]}];";
                            }
                        }
                        file.WriteLine(output);
                    }
                }
            }
        }

        /// <summary>
        /// TF-IDFの文章集合を空にする
        /// </summary>
        public void ResetTFIDF()
        {
            tfidf = new TfIdf.TfIdf();
        }

        /// <summary>
        /// クラスタごとの文章集合中のTF-IDFの平均値が大きいものから順に返す。
        /// </summary>
        /// <param name="clusterIndex">TF-IDFを求めたいクラスタ番号</param>
        /// <param name="NG">ランキングから除外する単語のリスト</param>
        public void GetTFIDFRanking(int clusterIndex, List<string> NG)
        {
            List<string> universe = new List<string>();
            List<string> data = new List<string>();
            ResetTFIDF();
            foreach (string[] elem in tweetWithOrientation)
            {
                universe.Add(elem[0]);
                int index = WhichCluster(int.Parse(elem[1]));
                if (clusterIndex == index)
                {
                    data.Add(elem[0]);
                }
            }
            Dictionary<string, double> tfidfData = AnalyzeTFIDF(universe, data, true);
            List<KeyValuePair<string, double>> result = new List<KeyValuePair<string, double>>();
            foreach (KeyValuePair<string, double> elem in tfidfData)
            {
                double v = elem.Value / (double)Cluster[clusterIndex].Members.Count();
                string[] ngWord = elem.Key.Split(new char[] { '(' });
                if (NG.Contains(ngWord[0]) == false)
                {
                    result.Add(new KeyValuePair<string, double>(elem.Key, v));
                }
            }
            result.Sort(delegate(KeyValuePair<string, double> a, KeyValuePair<string, double> b)
            {
                if (a.Value - b.Value > 0)
                {
                    return -1;
                }
                else if (a.Value - b.Value < 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            });
            WordRanking[clusterIndex] = result;
        }

        /// <summary>
        /// その極性がどのクラスタに属するかを返す。
        /// </summary>
        /// <param name="orientation">極性</param>
        /// <returns>クラスタ番号</returns>
        public int WhichCluster(int orientation)
        {
            for (int i = 0; i < Cluster.Count; i++)
            {
                foreach (Vector.Vector v in Cluster[i].Members)
                {
                    if (orientation == (int)v.Elements[0])
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// 文章の全集合を元にして、ある文章集合内のTF-IDFの合計値を返す。
        /// </summary>
        /// <param name="universe">文章の全集合</param>
        /// <param name="data">TF-IDFを求めたい文章集合</param>
        /// <param name="add">trueならuniverseをTF-IDFのリストに追加する</param>
        /// <returns>文章集合内でのTF-IDFの合計値を表す辞書</returns>
        private Dictionary<string, double> AnalyzeTFIDF(List<string> universe, List<string> data, bool add = false)
        {
            if (add == true)
            {
                foreach (string elem in universe)
                {
                    tfidf.Add(elem);
                }
            }
            Dictionary<string, double> result = new Dictionary<string, double>();
            foreach (string elem in data)
            {
                Dictionary<string, double> r = tfidf.analyzeTFIDF(elem, false);
                foreach (KeyValuePair<string, double> e in r)
                {
                    if (result.ContainsKey(e.Key) == true)
                    {
                        result[e.Key] += e.Value;
                    }
                    else
                    {
                        result[e.Key] = e.Value;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 与えられた文字列の極性を返す。
        /// </summary>
        /// <param name="text">極性判断したい文字列</param>
        /// <returns>極性値(-1.0～1.0)</returns>
        private double EstimateSemanticOrientation(string text)
        {
            return estimator.Estimate(text);
        }

    }
}
