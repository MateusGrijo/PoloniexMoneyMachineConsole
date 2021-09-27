using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.Generic;

public class ReturnOpenOrders
{
	
	
	public static List<ReturnOpenOrders> Create(string json)
	{
		List<ReturnOpenOrders> deserializedUser = new List<ReturnOpenOrders>();
		MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
		DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
		deserializedUser = ser.ReadObject(ms) as List<ReturnOpenOrders>;
		ms.Close();
		return deserializedUser;
	}
	
    public string orderNumber { get; set; }
    public string type { get; set; }
    public string rate { get; set; }
    public string startingAmount { get; set; }
    public string amount { get; set; }
    public string total { get; set; }
    public string date { get; set; }
    public int margin { get; set; }

}