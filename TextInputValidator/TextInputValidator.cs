using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TextInputValidator
{
    class TextInputValidator
    {
        /// <summary>
        /// なにもしないコンストラクタ
        /// </summary>
        public TextInputValidator()
        {

        }

        /// <summary>
        /// eメールの検証を行う
        /// </summary>
        /// <param name="str">検証を行うeメールアドレスの文字列</param>
        /// <returns>検証結果</returns>
        public bool EmailValidate(string str)
        {
            // メールアドレスの正規表現
            Regex reg = new Regex("^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\\.[a-zA-Z0-9-]+)+$");

            // 入力された文字列が正規表現にマッチするか？
            return reg.IsMatch(str);
        }

        /// <summary>
        /// URLの検証を行う（httpプロトコルだけ考慮）
        /// </summary>
        /// <param name="str">検証を行うURL文字列</param>
        /// <returns>検証結果</returns>
        public bool UrlValidate(string str)
        {
            // URLの正規表現
            Regex reg = new Regex("^http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?$");

            // 入力された文字列が正規表現にマッチするか？
            return reg.IsMatch(str);
        }

        /// <summary>
        /// 電話番号の検証を行う
        /// </summary>
        /// <param name="str">検証を行う電話番号の文字列</param>
        /// <returns>検証結果</returns>
        public bool TelValidate(string str)
        {
            // ハイフンを消す
            str = str.Replace("-", "");

            // 電話番号の正規表現
            Regex reg = new Regex("^((0\\d{9,10})|(110)|(119))$");

            // 入力された文字列が正規表現にマッチするか？
            return reg.IsMatch(str);
        }
    }
}
