using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.Generic;

public class ResultingTrade
{
    public string amount { get; set; }
    public string date { get; set; }
    public string rate { get; set; }
    public string total { get; set; }
    public string tradeID { get; set; }
    public string type { get; set; }
}

public class ReturnOperation
{
		
	public static ReturnOperation Create(string json)
	{
		ReturnOperation deserializedUser = new ReturnOperation();
		MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
		DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
		deserializedUser = ser.ReadObject(ms) as ReturnOperation;
		ms.Close();
		return deserializedUser;
	}
	
    public Int64 orderNumber { get; set; }
    public List<ResultingTrade> resultingTrades { get; set; }
}