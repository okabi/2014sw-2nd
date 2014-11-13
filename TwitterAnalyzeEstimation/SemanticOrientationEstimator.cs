using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticOrientationEstimator
{
    /// <summary>
    /// 与えられた文章の極性（ポジティブかネガティブか）を、形態素解析を行いつつ判定するクラス。
    /// </summary>
    public class SemanticOrientationEstimator
    {
        /// <summary>単語の文字列(品詞)をキーとして、極性を値とする</summary>
        private Dictionary<string, double> word_data;
        /// <summary>MeCab本体</summary>
        private MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer mma;
        
        /// <summary>
        /// 辞書データ(http://www.lr.pi.titech.ac.jp/~takamura/pubs/pn_ja.dic)読み込む。
        /// </summary>
        /// <param name="dicPath">辞書のパス</param>
        public SemanticOrientationEstimator(string dicPath)
        {
            LoadDictionary(dicPath);
            mma = new MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer();
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
            List<MeCabMorphologicalAnalyzer.Morpheme> morphmes = mma.Analyse(text);
            foreach (MeCabMorphologicalAnalyzer.Morpheme morpheme in morphmes)
            {
                if (word_data.ContainsKey(morpheme.Baseform + "(" + morpheme.Pos + ")") == true)
                {
                    result += word_data[morpheme.Baseform + "(" + morpheme.Pos + ")"];
                    hit_num++;
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

                // 漢字(品詞) => 極性 と 読み(品詞) => 極性 を登録
                word_data[data[0] + "(" + data[2] + ")"] = double.Parse(data[3]);
                word_data[data[1] + "(" + data[2] + ")"] = double.Parse(data[3]);
            }
            file.Close();
        }
    }
}
