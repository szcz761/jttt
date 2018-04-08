using QuickType;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Media.Imaging;

namespace JTTT
{
    class TaskTempSender : Task
    {
      
        public TaskTempSender(string city, int? temp, string textboxMail, string textBoxTaskName)
        {
            base.TaskName = textBoxTaskName;
            MailAdress = textboxMail;
            City = city;
            Temp = (int)temp;
            TaskProperties = TaskName + ": Pogoda powyzej? \"" + Temp + "\" w " + City + " dla " + MailAdress;
        }


        public override string Process()
        {
            try
            {
                Log.WriteToLog("Nacisniecie przyciksu wykonaj. Z parametrami: temp: " + Temp + " mail: " + MailAdress + " miasto: " + City);
               
                var mailsender = new MailSender(MailAdress);

                string jsonString;
                using (var wc = new WebClient())
                    jsonString = wc.DownloadString("http://api.openweathermap.org/data/2.5/weather?q=" + City + ",pl&appid=e3bb48f5e5555457cb51cee4bee3ceca");
                var welcome = Welcome.FromJson(jsonString);
                if(welcome.Main.Temp - 273 < Temp)
                    return"Temperatura jest niższa niz podano nie robie nic";

                string Message = $"Dzisiaj w {City} jest { (welcome.Main.Temp - 273.15).ToString()} stopni. \n" +
                    $"Ciśnienie: {welcome.Main.Pressure.ToString()}\n" +
                    $"Wiatr: {welcome.Wind.Speed * 3.6} km/h \n";
         
                var uri = new Uri("http://openweathermap.org/img/w/" + welcome.Weather[0].Icon + ".png");
                var bitmap = new BitmapImage(uri);
              
                
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(uri));
                using (var fileStream = new System.IO.FileStream("tmp.png", System.IO.FileMode.Create))
                {
                    encoder.Save(fileStream);
                }
                mailsender.SendEmail(Message, uri.ToString(), "tmp.png");

                return "Barwo! Wysłałeś Obrazek o URL: " + uri.ToString() + "\n";
            }
            catch (Exception x)
            {
                Log.WriteToLog("Błąd: " + x);
                return "Błąd: " + x.Message.ToString() + "\n";
            }
        }

    }
}