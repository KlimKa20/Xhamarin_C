using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Net;
using System.IO;

namespace App8
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class onlinetableview : ContentPage
    {
        public onlinetableview()
        {
            string pain;
            InitializeComponent();
            if (Resource1.Yes=="Yes") 
                pain = GET("https://rasp.rw.by/en/tablo/", "st_exp=2100001");
            else
                pain = GET("https://rasp.rw.by/ru/tablo/", "st_exp=2100001");
            Condition.Text = find(ref pain, "<span class=\"page-title_heading page-title_heading_small\">", "</span>");
            int ui = 0, ii = 0,z=0;

            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content=new Label { FontSize=20, Text = Resource1.Train, HorizontalTextAlignment= TextAlignment.Center, VerticalTextAlignment= TextAlignment.Center } }, ui++, ii);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 20, Text = Resource1 .Direction, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 20, Text = Resource1.Arrival, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 20, Text = Resource1.Departure, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 20, Text = Resource1.Car_numbering, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 20, Text = Resource1.Track, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 20, Text = Resource1.Platform , HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
            cran.Children.Add(new Frame { BackgroundColor = Color.LightSteelBlue, Content = new Label { FontSize = 20, Text = Resource1.Delay, HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii++);
            while (true)
            {
                if (z == 0)
                {
                    ui = 0;
                    string m = find(ref pain, "<tr class=\"b-train\">", "<td class=\"train_item train_info\">");
                    if (m == "-1")
                        break;
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightCoral, Content = new Label { Text = find(ref pain, "<small class=\"train_id\">", "</small>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightCoral, Content = new Label { Text = find(ref pain, "<span class=\"train_text\">", "&nbsp;") + " - " + find(ref pain, "&mdash;", "</span>") + "\n" + find(ref pain, "<div class=\"train_description\">", "</div>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightCoral, Content = new Label { Text = find(ref pain, "<b class=\"train_end-time\">", "</b>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightCoral, Content = new Label { Text = find(ref pain, "<b class=\"train_start-time\">", "</b>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightCoral, Content = new Label { Text = find(ref pain, "<span class=\"hidden\">2</span>", "</td>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightCoral, Content = new Label { Text = find(ref pain, "<td class=\"train_item train_halts train_way\">", "</td>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightCoral, Content = new Label { Text = find(ref pain, "<td class=\"train_item train_halts train_platform\">", "</td>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightCoral, Content = new Label { Text = find(ref pain, "<td class=\"train_item train_halts warning\">", "</td>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii++);
                    z = 1;
                }
                else
                {
                    ui = 0;
                    string m = find(ref pain, "<tr class=\"b-train\">", "<td class=\"train_item train_info\">");
                    if (m == "-1")
                        break;
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label { Text = find(ref pain, "<small class=\"train_id\">", "</small>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label { Text = find(ref pain, "<span class=\"train_text\">", "&nbsp;") + " - " + find(ref pain, "&mdash;", "</span>") + "\n" + find(ref pain, "<div class=\"train_description\">", "</div>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label { Text = find(ref pain, "<b class=\"train_end-time\">", "</b>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label { Text = find(ref pain, "<b class=\"train_start-time\">", "</b>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label { Text = find(ref pain, "<span class=\"hidden\">2</span>", "</td>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label { Text = find(ref pain, "<td class=\"train_item train_halts train_way\">", "</td>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label { Text = find(ref pain, "<td class=\"train_item train_halts train_platform\">", "</td>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii);
                    cran.Children.Add(new Frame { BackgroundColor = Color.LightPink, Content = new Label { Text = find(ref pain, "<td class=\"train_item train_halts warning\">", "</td>"), HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center } }, ui++, ii++);
                    z = 0;
                }
            }

        }
        static string find(ref string pain, string strbegin, string strend)
        {
            string temp = "";
            int n = pain.IndexOf(strbegin);
            if (n > pain.IndexOf("</tr><!-- // b-train -->") && strend == "</li>")
            {
                string b = "</tr><!-- // b-train -->";
                pain = pain.Remove(pain.IndexOf(b), b.Length);
                return "-1";
            }
            if (n == -1)
                return "-1";
            if (strbegin == "&car_type=")
                pain = pain.Remove(0, n + strbegin.Length + 3);
            else
                pain = pain.Remove(0, n + strbegin.Length);
            n = 0;
            while (pain[n] != strend[0])
            {
                temp += pain[n];
                n++;
            }
            pain = pain.Remove(n, strend.Length);
            return temp;
        }
        private static string GET(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url + "?" + Data);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();
            Console.WriteLine(Out);
            return Out;
        }
    }
}