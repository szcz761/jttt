using QuickType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JTTT
{
    class TaskTempDisplay : Task
    {

        public TaskTempDisplay(string city, int? temp, string textBoxTaskName)
        {
            TaskName = textBoxTaskName;
            City = city;
            Temp = (int)temp;
            TaskProperties = TaskName + ": jeśli temp jest większa niż "+Temp+" Wyświetlenie pogody  w "  + City;
        }
        public TaskTempDisplay()
        {
        }


        public override void Process()
        {
            try
            {
                Log.WriteToLog("Nacisniecie przyciksu wykonaj. wyświetl na ekranie pogodę Z parametrami: miasto: " + City);
               
                string jsonString;
                using (var wc = new WebClient())
                    jsonString = wc.DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + City + ",pl&appid=e3bb48f5e5555457cb51cee4bee3ceca");
                var welcome = Welcome.FromJson(jsonString);
                if (welcome.Main.Temp - 273 < Temp)
                {
                    Log.WriteToLog("Temperatura jest niższa niz podano nie robie nic");
                    return;
                }
                string Message = $"Dzisiaj w {City} jest { (welcome.Main.Temp - 273.15).ToString()} stopni. \n" +
                    $"Ciśnienie: {welcome.Main.Pressure.ToString()}\n" +
                    $"Wiatr: {welcome.Wind.Speed * 3.6} km/h \n";

                var uri = "http://openweathermap.org/img/w/" + welcome.Weather[0].Icon + ".png";
                using (var wc = new WebClient())
                    wc.DownloadFile(uri, "tmp.png");
                var win = new WindowDisplay(Message, uri);
                win.Show();
                Log.WriteToLog("Barwo! Wyświetliłeś Obrazek o URL: " + uri.ToString() + "\n");
            }
            catch (Exception x)
            {
                Log.WriteToLog("Błąd: " + x);
            }
        }
    }
}

