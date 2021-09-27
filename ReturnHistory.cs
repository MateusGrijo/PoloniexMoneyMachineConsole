using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.Generic;

public class ReturnHistory
{
	
	public static List<ReturnHistory> Create(string json)
	{
		List<ReturnHistory> deserializedUser = new List<ReturnHistory>();
		MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
		DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
		deserializedUser = ser.ReadObject(ms) as List<ReturnHistory>;
		ms.Close();
		return deserializedUser;
	}
		
    public int globalTradeID { get; set; }
    public int tradeID { get; set; }
    public string currencyPair { get; set; }
    public string type { get; set; }
    public string rate { get; set; }
    public string amount { get; set; }
    public string total { get; set; }
    public string fee { get; set; }
    public string date { get; set; }
}