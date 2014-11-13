using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitterAnalyzeEstimation
{
    public partial class FormConnect : Form
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new FormConnect());
        }

        public FormConnect()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ユーザ認証ページをWebBrowserに表示する。
        /// </summary>
        /// <param name="screenName">ユーザのscreen name</param>
        private void ShowConfirmPage(string screenName)
        {
            FormMain.twitterAnalyzeEstimator = new TwitterAnalyzeEstimation(screenName, "../../dic/pn_ja.dic");
            this.webBrowser.Navigate(FormMain.twitterAnalyzeEstimator.GetRequestUrl());
            this.textBoxPin.ReadOnly = false;
        }

        /// <summary>
        /// PINを受け取って、分析画面にフォームを遷移させる
        /// </summary>
        /// <param name="pin"></param>
        private void ChangeForm(string pin)
        {
            try
            {
                FormMain.twitterAnalyzeEstimator.GenerateAccessToken(pin);
                FormMain formMain = new FormMain(this);
                formMain.Show();
                this.Hide();
            }
            catch(Exception e)
            {
                if (e.GetType() == typeof(System.NullReferenceException))
                {
                    MessageBox.Show("左上の「認証」から始めてください。", "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (e.GetType() == typeof(System.Net.WebException))
                {
                    MessageBox.Show("PINに誤りがあります。認証をやり直してください。", "認証エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // 「認証」ボタン
        private void buttonId_Click(object sender, EventArgs e)
        {
            ShowConfirmPage(this.textBoxId.Text);
        }

        // 「認証」ボタンに対応する、テキストボックスのEnter
        private void textBoxId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ShowConfirmPage(this.textBoxId.Text);
            }
        }

        // 「開始」ボタン
        private void buttonPin_Click(object sender, EventArgs e)
        {
            ChangeForm(this.textBoxPin.Text);
        }

        // 「開始」ボタンに対応する、テキストボックスのEnter
        private void textBoxPin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChangeForm(this.textBoxPin.Text);
            }
        }
    }
}
