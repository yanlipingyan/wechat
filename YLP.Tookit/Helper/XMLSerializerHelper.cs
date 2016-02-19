using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YLP.Tookit.Helper
{
    public class XMLSerializerHelper
    {
        /// <summary>
        /// xml序列化
        /// </summary>
        public static bool Serialize<T>(T o, string filePath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                StreamWriter sw = new StreamWriter(filePath, false);
                formatter.Serialize(sw, o);
                sw.Flush();
                sw.Close();

                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        public static T DeSerialize<T>(string filePath)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                StreamReader sr = new StreamReader(filePath);
                T o = (T)formatter.Deserialize(sr);
                sr.Close();
                return o;
            }
            catch (Exception ex) { throw ex; }

            return System.Activator.CreateInstance<T>();
        }
    }
}
