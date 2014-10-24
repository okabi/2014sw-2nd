using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooMorphologicalAnalyzer
{
    public class Morpheme
    {
        /// <summary>語の表記</summary>
        public string Surface { get; private set; }
        /// <summary>語の品詞</summary>
        public string Pos { get; private set; }
        /// <summary>語の基本形</summary>
        public string Baseform { get; private set; }

        /// <summary>
        /// コンストラクタ。語の表記と品詞と基本形をセットする。
        /// </summary>
        /// <param name="surface">語の表記</param>
        /// <param name="pos">語の品詞</param>
        /// <param name="baseform">語の基本形</param>
        public Morpheme(string surface, string pos, string baseform)
        {
            Surface = surface;
            Pos = pos;
            Baseform = baseform;
        }

        /// <summary>
        /// "表記(品詞)"という文字列を返す
        /// </summary>
        /// <returns>"表記(品詞)"という文字列</returns>
        public override string ToString()
        {
            string result = Surface + "(" + Pos + ")";
            return result;
        }
    }
}
