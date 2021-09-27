
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class Database
{

	 /// <summary>
    /// Serializes an object.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="serializableObject"></param>
    /// <param name="fileName"></param>
    public static void SerializeObject<T>(T serializableObject)
    {
    	
    	String fileName =  System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\db.io";


        if (serializableObject == null) { return; }

        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, serializableObject);
                stream.Position = 0;
                xmlDocument.Load(stream);
                xmlDocument.Save(fileName);
                stream.Close();
            }
        }
        catch 
        {
            //Log exception here
        }
    }


    /// <summary>
    /// Deserializes an xml file into an object list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static T DeSerializeObject<T>()
    {
    	
    	String fileName =  System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "\\db.io";

    	  	
        if (string.IsNullOrEmpty(fileName)) { return default(T); }

        T objectOut = default(T);

        try
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(fileName);
            string xmlString = xmlDocument.OuterXml;

            using (StringReader read = new StringReader(xmlString))
            {
                Type outType = typeof(T);

                XmlSerializer serializer = new XmlSerializer(outType);
                using (XmlReader reader = new XmlTextReader(read))
                {
                    objectOut = (T)serializer.Deserialize(reader);
                    reader.Close();
                }

                read.Close();
            }
        }
        catch 
        {
            //Log exception here
        }

        return objectOut;
    }
	
	

}