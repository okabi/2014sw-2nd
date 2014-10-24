using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterConnector
{
    /// <summary>ツイート情報を保持するクラス</summary>
    public class Tweet
    {
        /// <summary>ツイートID</summary>
        public long Id { get; private set; }
        /// <summary>ツイート内容</summary>
        public string Text { get; private set; }
        /// <summary>ツイートユーザ</summary>
        public User User { get; private set; }

        /// <summary>
        /// ツイート情報を登録する。
        /// </summary>
        /// <param name="id">ツイートID</param>
        /// <param name="text">ツイート内容</param>
        /// <param name="user">ツイートユーザ</param>
        public Tweet(long id, string text, User user)
        {
            Id = id;
            Text = text;
            User = user;
        }

        public override string ToString()
        {
            return "ID: " + Id.ToString() + "\n" + User.ToString() + ": " + Text;
        }
    }
}
