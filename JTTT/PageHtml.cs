using System;
using System.Text;
using System.Net;
using HtmlAgilityPack;
using System.IO;


namespace JTTT
{


public class PageHtml
{
    private readonly string URL;

	public PageHtml(string url)
	{ 
        if (url.Length < 9 || (url.Substring(0, 7) != "http://"  &&  url.Substring(0, 8) != "https://"))
            URL = "http://" + url;
        else
            URL = url;
     
        Log.WriteToLog("Stworzenie PAgeHtml z  url: " + URL);

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
            Log.WriteToLog("GetStringPage(): Expection:  " + ex);
            throw new Exception("Nie udało się pobrac zawartosci strony!");
        }
    }

    public string SearchSentence(string KeyWord) //return URL_source of image
    {
        try
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(GetStringPage());
                
            Log.WriteToLog("SearchSentence(): Stworzenie HtmlDocument z strony sciagnietej prez WebClient.DownloadString ");

            var nodes = doc.DocumentNode.Descendants("img");
            foreach (var node in nodes)
            {
                if (node.GetAttributeValue("alt", "").ToLower().Contains(KeyWord.ToLower()))
                    return node.GetAttributeValue("src", "");
            }

            Log.WriteToLog(DateTime.Now.ToString() + "SearchSentence(): Nie znaleziono pasujacych nodow - zwracam pousty String");

            return "";
        }
        catch (Exception ex)
        {
            Log.WriteToLog("SearchSentence(): Expection:  " + ex);
            throw new Exception("Nie udało się wyszukać obrazka! Prawdopodobnie zły URL.");
        }
    }

    public void SaveImage(string ImageURL,string name)
    {
        try
        {
            var client = new WebClient();
            client.DownloadFile(ImageURL, name);

            Log.WriteToLog("SaveImage(): zapisalem obraz o URL: " + ImageURL + " Pod nazwą: " + name);
        }
        catch (Exception ex)
        {
            Log.WriteToLog("SaveImage(): Expection:  " + ex);
            throw new Exception("Nie udało się zapisać obrazka!");
        }
    }

}

}