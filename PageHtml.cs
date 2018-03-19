using System;
//using HtmlAgilityPack;

public class PageHtml
{
    private string sURL;

	public PageHtml(string url)
	{
        if (url.Substring(0, 7) != "http://" ||  url.Substring(0, 8) != "https://")
            url = "http://" + url;
        sURL = url;
    }

    public string GetStringPage()
    {
        var wc = new WebClient();
 
            // Ustawiamy prawidłowe kodowanie dla tej strony
            wc.Encoding = Encoding.UTF8;
            // Dekodujemy HTML do czytelnych dla wszystkich znaków 
            var html = System.Net.WebUtility.HtmlDecode(wc.DownloadString(_url));

            return html;
        }
    }
}
