using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticOrientationEstimator
{
    /// <summary>
    /// 与えられた文章の極性（ポジティブかネガティブか）を判定するクラス。
    /// Yahooの形態素解析を利用することも可能です(その方が精度が上がります)。
    /// </summary>
    public class SemanticOrientationEstimator
    {
        /// <summary>単語の文字列をキーとして、極性を値とする</summary>
        private Dictionary<string, double> word_data;
        /// <summary>"YahooのアプリケーションID</summary>
        private string app_id;
        /// <summary>"YahooのアプリケーションIDが使用されていないときにAppIDに入れられる文字列</summary>
        private const string APP_ID_NOT_USED = "not used";

        
        /// <summary>
        /// 形態素解析を使用しないver。辞書データ(http://www.lr.pi.titech.ac.jp/~takamura/pubs/pn_ja.dic)読み込む。
        /// </summary>
        /// <param name="dicPath">辞書のパス</param>
        public SemanticOrientationEstimator(string dicPath)
        {
            app_id = APP_ID_NOT_USED;
            LoadDictionary(dicPath);
        }


        /// <summary>
        /// 形態素解析を使用するver。辞書データ(http://www.lr.pi.titech.ac.jp/~takamura/pubs/pn_ja.dic)読み込む。
        /// </summary>
        /// <param name="dicPath">辞書のパス</param>
        /// <param name="appID">YahooのアプリケーションID</param>
        public SemanticOrientationEstimator(string dicPath, string appID)
        {
            app_id = appID;
            LoadDictionary(dicPath);
        }


        /// <summary>
        /// 指定された文章の極性を判定する。
        /// </summary>
        /// <param name="text">極性判定したい文章</param>
        /// <returns>判定結果（最もポジティブなら+1、最もネガティブなら-1を返す）</returns>
        public double Estimate(string text)
        {
            double result = 0.0;
            int hit_num = 0;
            // 形態素解析を使用しない
            if (app_id == APP_ID_NOT_USED)
            {
                foreach (string str in word_data.Keys)
                {
                    if (text.Contains(str) == true)
                    {
                        result += word_data[str];
                        hit_num++;
                    }
                }
            }
            // 形態素解析を使用する
            else
            {
                YahooMorphologicalAnalyzer.MorphologicalAnalyzer mma = new YahooMorphologicalAnalyzer.MorphologicalAnalyzer(app_id);
                List<YahooMorphologicalAnalyzer.Morpheme> morphmes = mma.Analyse(text);
                foreach (YahooMorphologicalAnalyzer.Morpheme morpheme in morphmes)
                {
                    if (word_data.ContainsKey(morpheme.Baseform) == true)
                    {
                        result += word_data[morpheme.Baseform];
                        hit_num++;
                    }
                }
            }
            // 極性の平均値を返す
            if (hit_num != 0)
            {
                result /= hit_num;
            }
            return result;
        }


        /// <summary>
        /// 辞書データを読み込む。
        /// </summary>
        /// <param name="dicPath">辞書のパス</param>
        private void LoadDictionary(string dicPath)
        {
            // 辞書の初期化
            word_data = new Dictionary<string, double>();
            // 辞書データの読み込み
            System.IO.StreamReader file = new System.IO.StreamReader(dicPath, Encoding.Default);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                char[] delimiter = { ':' };
                string[] data = line.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);

                // 漢字 => 極性 を登録
                word_data[data[0]] = double.Parse(data[3]);
            }
            file.Close();
        }
    }
}
