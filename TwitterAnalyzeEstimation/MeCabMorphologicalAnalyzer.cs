using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NMeCab;

namespace MeCabMorphologicalAnalyzer
{
    /// <summary>MeCabを利用して形態素分解するクラス。</summary>
    public class MeCabMorphologicalAnalyzer
    {
        /// <summary>形態素解析するためのインスタンス</summary>
        private MeCabTagger tagger;

        /// <summary>
        /// 形態素解析の準備を行う。
        /// </summary>
        public MeCabMorphologicalAnalyzer()
        {
            MeCabParam param = new MeCabParam();
            param.DicDir = @"../../dic/ipadic";
            tagger = MeCabTagger.Create(param);
        }

        /// <summary>
        /// 指定された文字列の形態素解析を行う。
        /// </summary>
        /// <param name="str">形態素解析を行う文字列</param>
        /// <returns>MorphemeクラスのList</returns>
        public List<Morpheme> Analyse(string str)
        {
            List<Morpheme> result = new List<Morpheme>();
            try
            {
                MeCabNode node = tagger.ParseToNode(str);
                while (node != null)
                {
                    if (node.CharType > 0)
                    {
                        string[] features = node.Feature.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        Morpheme m = new Morpheme(node.Surface, features[0], features[6]);
                        result.Add(m);
                    }
                    node = node.Next;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
