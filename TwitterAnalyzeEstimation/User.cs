using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConnector
{
    /// <summary>ユーザ情報を保持するクラス</summary>
    public class User
    {
        /// <summary>ユーザ名</summary>
        public string Name { get; private set; }
        /// <summary>ユーザのスクリーン名</summary>
        public string ScreenName { get; private set; }

        /// <summary>
        /// ユーザ情報を登録する。
        /// </summary>
        /// <param name="name">ユーザ名</param>
        /// <param name="screenName">ユーザのスクリーン名</param>
        public User(string name, string screenName)
        {
            Name = name;
            ScreenName = screenName;
        }

        public override string ToString()
        {
            return "@" + ScreenName + "(" + Name + ")";
        }
    }
}
