using System;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.IO;

public class PageHtml
{
    private readonly string URL;

	public PageHtml(string url)
	{ 
        if (url.Length < 9 || (url.Substring(0, 7) != "http://"  &&  url.Substring(0, 8) != "https://"))
            URL = "http://" + url;
        else
            URL = url;
        using (StreamWriter log = File.AppendText("JTTT.log"))
        {
            log.WriteLine(DateTime.Now.ToString() + "Stworzenie PAgeHtml z  url: " + URL);
            log.Close();
        }
    }

    public string GetStringPage()
    {
        try
        {
            var client = new WebClient();
            client.Encoding = Encoding.UTF8;
            //return client.DownloadString(URL)
            return WebUtility.HtmlDecode(client.DownloadString(URL));
        }
        catch (Exception ex)
        {
            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + " GetStringPage(): Expection:  " + ex);
                log.Close();
            }
            throw new Exception("Nie udało się pobrac zawartosci strony!");
        }
    }

    public string SearchSentence(string KeyWord) //return URL_source of image
    {
        try
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(GetStringPage());

            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + "SearchSentence(): Stworzenie HtmlDocument z strony sciagnietej prez WebClient.DownloadString ");
                log.Close();
            }

            var nodes = doc.DocumentNode.Descendants("img");
            foreach (var node in nodes)
            {
                if (node.GetAttributeValue("alt", "").ToLower().Contains(KeyWord.ToLower()))
                    return node.GetAttributeValue("src", "");
            }

            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + "SearchSentence(): Nie znaleziono pasujacych nodow - zwracam pousty String");
                log.Close();
            }

            return "";
        }
        catch (Exception ex)
        {
            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + " SearchSentence(): Expection:  " + ex);
                log.Close();
            }
            throw new Exception("Nie udało się wyszukać obrazka! Prawdopodobnie zły URL.");
        }
    }

    public void SaveImage(string ImageURL,string name)
    {
        try
        {
            var client = new WebClient();
            client.DownloadFile(ImageURL, name);

            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + "SaveImage(): zapisalem obraz o URL: " + ImageURL + " Pod nazwą: " + name);
                log.Close();
            }
        }
        catch (Exception ex)
        {
            using (StreamWriter log = File.AppendText("JTTT.log"))
            {
                log.WriteLine(DateTime.Now.ToString() + " SaveImage(): Expection:  " + ex);
                log.Close();
            }
            throw new Exception("Nie udało się zapisać obrazka!");
        }
    }

}
