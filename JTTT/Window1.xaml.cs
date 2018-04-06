using System;
using System.Net;
using System.Windows;
using System.Windows.Media.Imaging;
using QuickType;



namespace Jeson_Test_JTTT4._1_
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string jsonString;
                using (var wc = new WebClient())
                    jsonString = wc.DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + TBMiasto.Text.ToString() + ",pl&appid=e3bb48f5e5555457cb51cee4bee3ceca");
                var welcome = Welcome.FromJson(jsonString);
                (welcome.Main.Temp - 273).ToString();
                TBPogoda.Text = $"Dzisiaj w {TBMiasto.Text.ToString()} jest { (welcome.Main.Temp - 273.15).ToString()} stopni. \n" +
                    $"Ciśnienie: {welcome.Main.Pressure.ToString()}\n" +
                    $"Wiatr: {welcome.Wind.Speed*3.6} km/h \n";
                var uri = new Uri("http://openweathermap.org/img/w/" + welcome.Weather[0].Icon + ".png");
                var bitmap = new BitmapImage(uri);
                Icon.Source = bitmap;
            }
            catch(Exception ex)
            {
                TBPogoda.Text = "Nie udało się pobrac pogody!";
            }
        }
    }
}
