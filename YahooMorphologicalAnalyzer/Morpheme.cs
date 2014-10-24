using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahooMorphologicalAnalyzer
{
    class Morpheme
    {
        // 語の表記
        public string Surface { get; private set; }
        // 語の品詞
        public string Pos { get; private set; }

        /// <summary>
        /// コンストラクタ。語の表記と品詞をセットする。
        /// </summary>
        /// <param name="surface">語の表記</param>
        /// <param name="pos">語の品詞</param>
        public Morpheme(string surface, string pos)
        {
            Surface = surface;
            Pos = pos;
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
