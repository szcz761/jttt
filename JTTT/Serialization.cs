using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JTTT
{
    class Serialization
    {
        static public void SerializationFunc<T>(T o)
        {
            /*var attributes = new XmlRootAttribute();
            attributes.ElementName = "Tasks";
            attributes.Namespace = "TEST";*/

            var serializer = new XmlSerializer(typeof(ObservableCollection<Task>)/*, attributes*/);
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

        static public ObservableCollection<Task> DeserializationFunc(string xmlName)
        {
            var deserializer = new XmlSerializer(typeof(ObservableCollection<Task>));
            ObservableCollection<Task> returnedValue;

            FileStream xmlStream = new FileStream(xmlName, FileMode.Open);
            XmlReader reader = XmlReader.Create(xmlStream);
            returnedValue = new ObservableCollection<Task>((ObservableCollection<Task>) deserializer.Deserialize(reader));
            xmlStream.Close();
            Log.WriteToLog("Zdeserializowano");
            return returnedValue;
        }
    }
}
