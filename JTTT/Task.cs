using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace JTTT
{
    [Serializable()]
    public class Task : INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _taskProperties;
        [XmlElement("SearchPhrase")]
        public string SearchPhrase { get; set; }
        [XmlElement("SourceUrl")]
        public string SourceUrl { get; set; }
        [XmlElement("MailAdress")]
        public string MailAdress { get; set; }
        [XmlElement("TaskName")]
        public string City { get; set; }
        [XmlElement("City")]
        public int Temp { get; set; }
        [XmlElement("Temp")]
        public string TaskName { get; set; }
        [XmlElement("TaskProperties")]
        public string TaskProperties
        {
            get { return _taskProperties; }
            set { _taskProperties = value; NotifyPropertyChanged("TaskProperties"); }
        }

        public Task()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        virtual public string Process()
        {
            return "false";
        }

        private void NotifyPropertyChanged(string value)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(value));
            }
        }
    }
}
