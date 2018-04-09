using QuickType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JTTT
{
    /// <summary>
    /// Logika interakcji dla klasy WindowDisplay.xaml
    /// </summary>
    public partial class WindowDisplay : Window
    {
        public WindowDisplay(String Message, String URI)
        {
            InitializeComponent();
            try
            {
                TextBoxMessage.Text = Message;
                var uri = new Uri(URI);
                var bitmap = new BitmapImage(uri);
                Icon.Source = bitmap;
            }
            catch (Exception ex)
            {
                TextBoxMessage.Text = "Nie udało się pobrac danych!";
            }
        }
    }
}
