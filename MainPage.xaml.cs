using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 を参照してください

namespace VoiceItemSelectSample
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            webview.Navigate(StartURL);
            foreach (string item in responses)
                listView.Items.Add(item);

            var listConstraint = new Windows.Media.SpeechRecognition.SpeechRecognitionListConstraint(responses, "yesOrNo");

            speechRecognizer.UIOptions.ExampleText = @"左の商品から音声で選択してください？";
            speechRecognizer.Constraints.Add(listConstraint);
            speechRecognizer.UIOptions.ShowConfirmation = true;

            // Compile the constraint.
            await speechRecognizer.CompileConstraintsAsync();

        }

        SpeechRecognizer speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();

        Uri StartURL = new Uri("http://Bing.com/");

        string[] responses = {
                "オランジーナ",
                "午後の紅茶レモンティー",
                "午後の紅茶ストレート",
                "午後の紅茶ミルクティー",
                "充実野菜緑黄色野菜ＭＩＸ",
                "ファイブミニ",
                "オロナミンＣ",
                "コカコーラ",
                "ペプシ",
                "ダイエットペプシ",
                "ペプシNEXT",
                "ドクターペッパー",
                "アクエリアス",
                "ファンタオレンジ",
                "ファンタグレープ",
                "ジョージアオリジナル",
                "紅茶花伝ストレートティー",
                "ヤクルト",
                "ミルミル",
                "トマトジュース",
                "充実野菜緑黄色ミックス",
                "野菜生活",
                "ポカリスウェット",
                "カルビスウォーター",
                "ＣＣレモン",
                "ＢＯＳＳスーパーブレンド",
                "桃の天然水",
                "ビタミンパーラー",
                "ビタミンレモン",
                "おーいお茶",
                "爽健美茶",
                "伊右衛門",
                "お茶",
                "綾鷹",
                "伊右衛門玉露入り",
                "ホーム"
                };

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            await VoiceRecognition();
        }

        private async System.Threading.Tasks.Task VoiceRecognition()
        {
            try
            {

                Windows.Media.SpeechRecognition.SpeechRecognitionResult speechRecognitionResult
                    = await speechRecognizer.RecognizeWithUIAsync();

                //テキストボックスに入れる
                String result = speechRecognitionResult.Text;

                //リストから検索して該当するものを選択する
                if (result != "")
                {
                    if(result=="ホーム")
                        webview.Navigate(StartURL);
                    else
                    {
                        textBox.Text = result;
                        foreach (var item in listView.Items)
                            if ((String)item == result)
                                listView.SelectedItem = item;
                    }
                }

            }
            catch { }
        }


        private void image1_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            webview.GoBack();
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listView.SelectedIndex != -1)
            {
                string f = (string)(listView.SelectedItem);
                webview.Navigate(new Uri("https://www.bing.com/images/search?q=" + Uri.EscapeUriString(f) + @"&FORM=HDRSC2"));
            }
        }


    }
}
