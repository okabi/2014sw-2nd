using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeCabMorphologicalAnalyzer
{
    /// <summary>形態素を制御するクラス。</summary>
    public class Morpheme
    {
        /// <summary>語の表記</summary>
        public string Surface { get; private set; }
        /// <summary>語の品詞</summary>
        public string Pos { get; private set; }

        /// <summary>
        /// 形態素情報を渡してインスタンスを生成する。
        /// </summary>
        /// <param name="surface">語の表記</param>
        /// <param name="pos">語の品詞</param>
        public Morpheme(string surface, string pos)
        {
            Surface = surface;
            Pos = pos;
        }

        /// <summary>
        /// 表記(品詞) 形式の文字列を返す。
        /// </summary>
        /// <returns>表記(品詞) 形式の文字列</returns>
        public override string ToString()
        {
            return Surface + "(" + Pos + ")";
        }
    }
}
