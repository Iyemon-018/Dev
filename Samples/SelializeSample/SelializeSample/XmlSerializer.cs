using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SelializeSample
{
    public static class XmlSerializer
    {
        public static string Serialize<T>(this T self)
        {
            string result;

            using (var sw = new StringWriter())
            {
                var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                serializer.Serialize(sw, self);
                result = sw.ToString();
            }

            return result;
        }
    }
}
