using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextSimilarity
{
    /// <summary>2つの文章の類似度を測るクラス。</summary>
    class TextSimilarity
    {
        /// <summary>類似度計算を行う文字列その１</summary>
        public string Text1 { get; private set; }
        /// <summary>類似度計算を行う文字列その２</summary>
        public string Text2 { get; private set; }
        
        /// <summary>
        /// 類似度計算を行う文字列を指定する。
        /// </summary>
        /// <param name="text1">類似度計算を行う文字列</param>
        /// <param name="text2">類似度計算を行う文字列</param>
        public TextSimilarity(string text1, string text2)
        {
            Text1 = text1;
            Text2 = text2;
        }

        /// <summary>
        /// Jaccard係数により類似度計算を行う。
        /// |X∩Y|/|X∪Y|
        /// </summary>
        /// <returns>類似度(0.0～1.0)</returns>
        public double Jaccard()
        {
            // 重複要素の削除
            List<string> x = GetWords(Text1).Distinct().ToList();
            List<string> y = GetWords(Text2).Distinct().ToList();

            // 積集合の計算
            List<string> intersect = new List<string>();
            foreach (string elem in x)
            {
                if (y.Contains(elem) == true)
                {
                    intersect.Add(elem);
                }
            }
        
            // 和集合の計算
            List<string> union = new List<string>();
            union = x.Concat(y).Distinct().ToList();

            return (double)intersect.Count / (double)union.Count;
        }

        /// <summary>
        /// コサイン類似度計算を行う。
        /// (x・y)/(|x||y|)
        /// </summary>
        /// <returns>類似度</returns>
        public double Cosine()
        {
            List<string> x = GetWords(Text1);
            List<string> y = GetWords(Text2);

            // 和集合の計算
            List<string> union = new List<string>();
            union = x.Concat(y).Distinct().ToList();

            // それぞれのベクトルを生成
            Vector.Vector xVec = new Vector.Vector(union.Count);
            Vector.Vector yVec = new Vector.Vector(union.Count);
            for (int i = 0; i < union.Count; i++)
            {
                double[] one = new double[union.Count];
                one[i] = 1.0;
                Vector.Vector oneVec = new Vector.Vector(one);
                while (x.Contains(union[i]) == true)
                {
                    x.Remove(union[i]);
                    xVec = xVec.Add(oneVec);
                }
                while (y.Contains(union[i]) == true)
                {
                    y.Remove(union[i]);
                    yVec = yVec.Add(oneVec);
                }
            }

            // コサイン類似度の計算
            return xVec.InnerProduct(yVec) / (xVec.Norm() * yVec.Norm());
        }

        /// <summary>
        /// 文字列中の名詞のみを抽出する(重複あり)。
        /// </summary>
        /// <param name="str">名詞を抽出したい文字列</param>
        /// <returns>抽出された名詞(重複あり)</returns>
        private List<string> GetWords(string str)
        {
            MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer analyzer = new MeCabMorphologicalAnalyzer.MeCabMorphologicalAnalyzer();
            List<MeCabMorphologicalAnalyzer.Morpheme> morphemes = analyzer.Analyse(str);
            List<string> result = new List<string>();
            foreach (MeCabMorphologicalAnalyzer.Morpheme elem in morphemes)
            {
                if (elem.Pos == "名詞" || elem.Pos == "動詞" || elem.Pos == "形容詞" || elem.Pos == "副詞" || elem.Pos == "連体詞")
                {
                    result.Add(elem.Surface);
                }
            }
            return result;
        }
    }
}
