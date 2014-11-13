using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfIdf
{
    /// <summary>TF-IDFによって文書内の単語に重み付けをするクラス。</summary>
    public class TfIdf
    {
        /// <summary>形態素解析を行う</summary>
        private MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer analyzer;
        /// <summary>文章集合</summary>
        public List<string> Document { get; private set; }
        /// <summary>「単語(品詞)」が含まれる文章の数(文章集合中)</summary>
        public Dictionary<string, int> DF { get; private set; }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public TfIdf()
        {
            analyzer = new MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer();
            Document = new List<string>();
            DF = new Dictionary<string, int>();
        }

        /// <summary>
        /// 文章集合に指定文章を追加する。
        /// </summary>
        /// <param name="text">追加する文章</param>
        public void Add(string text)
        {
            Document.Add(text);
            List<MeCabMorphologicalAnalyzer.Morpheme> morphemes = analyzer.Analyse(text);
            List<string> set = new List<string>();
            foreach (MeCabMorphologicalAnalyzer.Morpheme elem in morphemes)
            {
                string str = elem.Baseform + "(" + elem.Pos + ")";
                if (set.Contains(str) == false && (elem.Pos == "名詞" || elem.Pos == "形容詞" || elem.Pos == "形容動詞" || elem.Pos == "動詞"))
                {
                    set.Add(str);
                }
            }
            foreach (string elem in set)
            {                
                if (DF.ContainsKey(elem) == true)
                {
                    DF[elem]++;
                }
                else
                {
                    DF[elem] = 1;
                }
            }
        }

        /// <summary>
        /// 指定した文章に含まれる各単語のTFを返す。
        /// </summary>
        /// <param name="text">TFを解析したい文章</param>
        /// <param name="add">trueなら文章集合に解析文章を追加する</param>
        /// <returns>単語(品詞) => TFの辞書</returns>
        public Dictionary<string, double> analyzeTF(string text, bool add = true)
        {
            if (add == true)
            {
                Add(text);
            }
            // ある単語の、textでの出現回数
            Dictionary<string, int> n = new Dictionary<string, int>();
            // textでの単語の個数
            int count = 0;
            List<MeCabMorphologicalAnalyzer.Morpheme> morphemes = analyzer.Analyse(text);
            foreach (MeCabMorphologicalAnalyzer.Morpheme elem in morphemes)
            {
                string str = elem.Baseform + "(" + elem.Pos + ")";
                if (elem.Pos == "名詞" || elem.Pos == "形容詞" || elem.Pos == "形容動詞" || elem.Pos == "動詞")
                {
                    count++;
                    if (n.ContainsKey(str) == true)
                    {
                        n[str]++;
                    }
                    else
                    {
                        n[str] = 1;
                    }
                }
            }
            Dictionary<string, double> result = new Dictionary<string, double>();
            foreach (KeyValuePair<string, int> elem in n)
            {
                double tf = (double)elem.Value / (double)count;
                result[elem.Key] = tf;
            }
            return result;
        }

        /// <summary>
        /// "単語(品詞)"のIDFを返す。
        /// </summary>
        /// <param name="key">IDFを調べたい"単語(品詞)"</param>
        /// <returns>IDF</returns>
        public double analyzeIDF(string key)
        {
            double result = Math.Log((double)Document.Count / (double)DF[key], 10) + 1;
            return result;
        }

        /// <summary>
        /// 指定した文章に含まれる単語のTF-IDFを返す。同時に、文章集合に解析文章を追加する。
        /// </summary>
        /// <param name="text">TF-IDFを解析したい文章</param>
        /// <param name="add">trueなら文章集合に解析文章を追加する</param>
        /// <returns>単語(品詞) => TF-IDFの辞書</returns>
        public Dictionary<string, double> analyzeTFIDF(string text, bool add = true)
        {
            Dictionary<string, double> tf = analyzeTF(text, add);
            Dictionary<string, double> result = new Dictionary<string, double>();
            foreach (KeyValuePair<string, double> elem in tf)
            {
                double idf = analyzeIDF(elem.Key);
                result[elem.Key] = elem.Value * idf;
            }
            return result;
        }
    }
}
