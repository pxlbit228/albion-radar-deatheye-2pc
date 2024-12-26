using System.IO;
using System.Xml.Serialization;

namespace X975.Radar.Dependencies
{
    public class XmlTools
    {
        public static T Deserialize<T>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(fileStream);
            }
        }
    }
}