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
    public partial class FormMain : Form
    {
        /// <summary>Twitter接続・分析用のインスタンス</summary>
        public static TwitterAnalyzeEstimation twitterAnalyzeEstimator;
        /// <summary>認証画面のフォーム</summary>
        private FormConnect formConnect;
        /// <summary>WebBrowserに表示するHTMLへのパス</summary>
        private const string HTML_PATH = @"C:/Users/a0120051/Documents/Visual Studio 2013/Projects/EnshuFour/TwitterAnalyzeEstimation/html/chart.html";
        /// <summary>極性分布を吐き出すJSへのパス</summary>
        private const string CHART_JS_PATH = @"C:\Users\a0120051\Documents\Visual Studio 2013\Projects\EnshuFour\TwitterAnalyzeEstimation\html\orientation.js";
        /// <summary>分析結果を吐き出すJSへのパス</summary>
        private const string RESULT_JS_PATH = @"C:\Users\a0120051\Documents\Visual Studio 2013\Projects\EnshuFour\TwitterAnalyzeEstimation\html\result.js";
        /// <summary>極性辞書へのパス</summary>
        private const string DIC_PATH = @"../../dic/pn_ja.dic";

        /// <summary>
        /// 分析用フォームのコンストラクタ。
        /// </summary>
        /// <param name="fc">認証用フォーム</param>
        public FormMain(FormConnect fc)
        {
            InitializeComponent();
            formConnect = fc;
            comboBoxCount.SelectedIndex = 0;
            this.webBrowser.Navigate(HTML_PATH);
        }

        /// <summary>
        /// 分析用フォームが閉じられる時、認証用フォームも閉じる
        /// </summary>
        private void EventFormClosed(object sender, FormClosedEventArgs e)
        {
            formConnect.Close();
        }

        /// <summary>
        /// 入力された文字列でTwitterを検索し、webbrowserの内容も更新する。
        /// </summary>
        private void analyze()
        {
            SemanticOrientationEstimator.SemanticOrientationEstimator estimator = new SemanticOrientationEstimator.SemanticOrientationEstimator(DIC_PATH);
            List<TwitterConnector.Tweet> tweets = twitterAnalyzeEstimator.Search(this.searchText.Text, int.Parse(this.comboBoxCount.Text));
            Dictionary<int, int> orientations = twitterAnalyzeEstimator.CreateOrientationList(tweets);
            twitterAnalyzeEstimator.CulculateParameters(orientations);
            twitterAnalyzeEstimator.CreateChartJavaScript(this.searchText.Text, CHART_JS_PATH, orientations, tweets);
            twitterAnalyzeEstimator.Clustering(orientations);
            List<string> NG = new List<string>();
            NG.Add("*");
            NG.Add(this.searchText.Text);
            for (int i = 0; i < 3; i++)
            {
                twitterAnalyzeEstimator.GetTFIDFRanking(i, NG);
            }
            twitterAnalyzeEstimator.CreateResultJavaScript(RESULT_JS_PATH);
            this.webBrowser.Navigate(HTML_PATH);
        }

        // 「検索」ボタン
        private void searchButton_Click(object sender, EventArgs e)
        {
            analyze();
        }

        // 「検索」ボタンに対応する、テキストボックスのEnter
        private void searchText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                analyze();
            }
        }

    }
}
