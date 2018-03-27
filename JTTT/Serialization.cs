using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace JTTT
{
    class Serialization
    {
        static public void SerializationFunc<T>(T o)
        {
            var attributes = new XmlRootAttribute();
            attributes.ElementName = "Tasks";
            attributes.Namespace = "TEST";

            var serializer = new XmlSerializer(typeof(ObservableCollection<Task>), attributes);
            try
            {
                var xmlStream = new StreamWriter("task.xml");
                serializer.Serialize(xmlStream, o);
                xmlStream.Dispose();
            }
            catch (Exception e)
            {
                Log.WriteToLog("Aplikacja wygenerowała wyjątek: " + e.Message);
            }
        }
    }
}
