using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex
{
    public class Complex
    {
        // 実部
        public double Real { get; private set; }
        // 虚部
        public double Imaginary { get; private set; }
        // 絶対値
        public double Z { get; private set; }
        // 偏角
        public float Arg { get; private set; }
        
        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="str">"a+bi"形式の文字列。必ずa・b共に記述されている必要あり</param>
        public Complex(string str)
        {
            // 実部と虚部に文字列を分割
            char[] delimiterChars = { '+', 'i' };
            string[] realAndImaginary = str.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            // プロパティの設定
            Real = Double.Parse(realAndImaginary[0]);
            Imaginary = Double.Parse(realAndImaginary[1]);
            OrthogonalToPolar(Real, Imaginary);
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="re">実部。</param>
        /// <param name="im">虚部。</param>
        public Complex(double re, double im)
        {
            Real = re;
            Imaginary = im;
            OrthogonalToPolar(re, im);
        }

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="z">絶対値</param>
        /// <param name="arg">偏角</param>
        public Complex(double z, float arg)
        {
            Z = z;
            Arg = arg;
            PolarToOrthogonal(z, arg);
        }

        /// <summary>
        /// 自身と指定された複素数の和を返す
        /// </summary>
        /// <param name="other">加える複素数</param>
        /// <returns>計算結果</returns>
        public Complex Add(Complex other)
        {
            Complex result = new Complex(this.Real, this.Imaginary);
            result.Real += other.Real;
            result.Imaginary += other.Imaginary;
            result.OrthogonalToPolar(result.Real, result.Imaginary);
            return result;
        }

        /// <summary>
        /// 自身と指定された複素数の差を返す
        /// </summary>
        /// <param name="other">引く複素数</param>
        /// <returns>計差結果</returns>
        public Complex Subtract(Complex other)
        {
            Complex result = new Complex(this.Real, this.Imaginary);
            result.Real -= other.Real;
            result.Imaginary -= other.Imaginary;
            result.OrthogonalToPolar(result.Real, result.Imaginary);
            return result;
        }

        /// <summary>
        /// 自身と指定された実数の積を返す
        /// </summary>
        /// <param name="d">掛ける実数</param>
        /// <returns>計算結果</returns>
        public Complex Multiply(double d)
        {
            Complex result = new Complex(this.Real, this.Imaginary);
            result.Real *= d;
            result.Imaginary *= d;
            result.OrthogonalToPolar(result.Real, result.Imaginary);
            return result;
        }

        /// <summary>
        /// 自身と指定された複素数の積を返す
        /// </summary>
        /// <param name="other">掛ける複素数</param>
        /// <returns>計算結果</returns>
        public Complex Multiply(Complex other)
        {
            Complex result = new Complex(this.Real, this.Imaginary);
            result.Z *= other.Z;
            result.Arg += other.Arg;
            result.PolarToOrthogonal(result.Z, result.Arg);
            return result;
        }

        /// <summary>
        /// 別のインスタンスを生成するメソッド
        /// </summary>
        /// <returns>複製された複素数クラス</returns>
        public Complex Clone()
        {
            Complex result = new Complex(this.Real, this.Imaginary);
            return result;
        }

        /// <summary>
        /// "a+bi"の形式の文字列を返すメソッド
        /// </summary>
        /// <returns>"a+bi"の形式の文字列</returns>
        public override string ToString()
        {
            string result = Real.ToString();
            if (Imaginary >= 0)
            {
                result += "+" + Imaginary + "i";
            }
            else
            {
                result += Imaginary + "i";
            }
            return result;
        }


        /// <summary>
        /// real(実部)とimaginary(虚部)からZ(絶対値)とArg(偏角)を設定する。
        /// </summary>
        /// <param name="real">実部</param>
        /// <param name="imaginary">虚部</param>
        private void OrthogonalToPolar(double real, double imaginary)
        {
            Z = Math.Sqrt(real * real + imaginary * imaginary);
            if (real == 0.0)
            {
                if (imaginary > 0)
                {
                    Arg = (float)(Math.PI / 2);
                }
                else
                {
                    Arg = (float)(-Math.PI / 2);
                }
            }
            else
            {
                Arg = (float)(Math.Atan(imaginary / real));
            }
        }

        /// <summary>
        /// Z(絶対値)とArg(偏角)からReal(実部)とImaginary(虚部)を設定する。
        /// </summary>
        /// <param name="z">絶対値</param>
        /// <param name="arg">偏角</param>
        private void PolarToOrthogonal(double z, float arg)
        {
            Real = z * Math.Cos(arg);
            Imaginary = z * Math.Sin(arg);
        }
    }
}
