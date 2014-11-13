using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vector
{
    public class Vector
    {
        /// <summary>次元</summary>
        public int Dimension { get; private set; }
        /// <summary>各次元の要素を格納する配列</summary>
        public double[] Elements { get; private set; }

        /// <summary>
        /// 初期ベクトルを与えるコンストラクタ。
        /// </summary>
        /// <param name="vector">doubleの配列で表現されたベクトル</param>
        public Vector(double[] vector)
        {
            Elements = new double[vector.Length];
            vector.CopyTo(Elements, 0);
            Dimension = vector.Length;
        }

        /// <summary>
        /// 0ベクトルインスタンスを生成するコンストラクタ。
        /// </summary>
        /// <param name="dimension">次元</param>
        public Vector(int dimension)
        {
            Elements = new double[dimension];
        }

        /// <summary>
        /// 指定された次元の要素値を返す。
        /// </summary>
        /// <param name="dimension">次元</param>
        /// <returns>要素値</returns>
        public double GetValue(int dimension)
        {
            return Elements[dimension];
        }

        /// <summary>
        /// ベクトルを"(x1, x2, ...)"の形式の文字列で返す。
        /// </summary>
        /// <returns>"(x1, x2, ...)"の形式の文字列</returns>
        public override string ToString()
        {
            string result = "(";
            foreach (double elem in Elements)
            {
                result += elem.ToString() + ", ";
            }
            result = result.Remove(result.Length - 2) + ")";
            return result;
        }

        /// <summary>
        /// 自身と指定されたベクトルの和を返す。
        /// </summary>
        /// <param name="other">加えるベクトル</param>
        /// <returns>計算結果</returns>
        public Vector Add(Vector other)
        {
            if (this.Elements.Length != other.Elements.Length)
            {
                Console.Error.WriteLine("error:ベクトルサイズが一致しない。(Add)");
                return new Vector(other.Elements);
            }
            else
            {
                Vector result = new Vector(this.Elements);
                for (int i = 0; i < Elements.Length; i++)
                {
                    result.Elements[i] += other.Elements[i];
                }
                return result;
            }
        }

        /// <summary>
        /// 自身と指定されたベクトルの差を返す。
        /// </summary>
        /// <param name="other">引くベクトル</param>
        /// <returns>計差結果</returns>
        public Vector Sub(Vector other)
        {
            if (this.Elements.Length != other.Elements.Length)
            {
                Console.Error.WriteLine("error:ベクトルサイズが一致しない。(Sub)");
                return new Vector(other.Elements);
            }
            else
            {
                Vector result = new Vector(this.Elements);
                for (int i = 0; i < Elements.Length; i++)
                {
                    result.Elements[i] -= other.Elements[i];
                }
                return result;
            }
        }

        /// <summary>
        /// 自身と指定されたスカラ値の積を返す。
        /// </summary>
        /// <param name="d">掛けるスカラ値</param>
        public Vector ScalarMultiply(double d)
        {
            Vector result = new Vector(this.Elements);
            for (int i = 0; i < Elements.Length; i++)
            {
                result.Elements[i] *= d;
            }
            return result;
        }

        /// <summary>
        /// 自身と指定されたベクトルの内積を返す。
        /// </summary>
        /// <param name="other">自身とのない席を求めたいベクトル</param>
        /// <returns>内積</returns>
        public double InnerProduct(Vector other)
        {
            if (this.Elements.Length != other.Elements.Length)
            {
                Console.Error.WriteLine("error:ベクトルサイズが一致しない。(InnerProduct)");
                return 0.0;
            }
            else
            {
                double result = 0.0;
                for (int i = 0; i < Elements.Length; i++)
                {
                    result += this.Elements[i] * other.Elements[i];
                }
                return result;
            }
        }

        /// <summary>
        /// ノルムを返す。
        /// </summary>
        /// <returns>ノルム</returns>
        public double Norm()
        {
            double result = 0.0;
            for (int i = 0; i < Elements.Length; i++)
            {
                result += Elements[i] * Elements[i];
            }
            return System.Math.Sqrt(result);
        }
    }
}
