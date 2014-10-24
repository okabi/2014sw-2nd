using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextInputValidator
{
    class ConsoleDebug
    {
        static void Main(string[] args)
        {
            // インスタンス生成
            TextInputValidator validator = new TextInputValidator();

            // 入力例の結果を出力
            Console.WriteLine(validator.EmailValidate("xxx@dl.kuis.kyoto-u.ac.jp")); // true
            Console.WriteLine(validator.EmailValidate("x@xx@dl.kuis.kyoto-u.ac.jp")); // false
            Console.WriteLine(validator.EmailValidate("a@b")); // false
            Console.WriteLine(validator.EmailValidate("a@hoge.com")); // true
            Console.WriteLine(validator.EmailValidate("hoge@foo.bar.")); // false
            Console.WriteLine(validator.UrlValidate("http://www.dl.kuis.kyoto-u.ac.jp/")); // true
            Console.WriteLine(validator.UrlValidate("www.dl.kuis.kyoto-u.ac.jp/")); // false
            Console.WriteLine(validator.UrlValidate("http://")); // false
            Console.WriteLine(validator.UrlValidate("http://www.dl://kuis.kyoto-u.ac.jp")); // false
            Console.WriteLine(validator.UrlValidate("http://127.0.0.1/")); // true
            Console.WriteLine(validator.TelValidate("075-753-5959")); // true
            Console.WriteLine(validator.TelValidate("119")); // true
            Console.WriteLine(validator.TelValidate("1-1-1-1")); // false
            Console.WriteLine(validator.TelValidate("03-333-333")); // false
            Console.WriteLine(validator.TelValidate("090-111-11111111")); // false
            Console.WriteLine(validator.TelValidate("9999999999"));  // false

            // Enterで終了
            Console.ReadKey();
        }
    }
}
