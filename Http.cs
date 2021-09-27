using System;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Cryptography;

public static class Http
{
	static string ByteToString(byte[] buff)
{
    string sbinary = "";
    for (int i = 0; i < buff.Length; i++)
    {
        sbinary += buff[i].ToString("X2"); /* hex format */
    }
    return (sbinary);
} 
	
	public static string post(String url,String parameters)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			
			parameters = "nonce=" + DateTime.Now.ToString("yyyyMMddHHmmss") + "&" + parameters;
			
			
			
			var data = Encoding.ASCII.GetBytes(parameters);
			
			HMACSHA512 encryptor = new HMACSHA512();
			encryptor.Key = Encoding.ASCII.GetBytes(Key.secret);
			String sign = ByteToString(encryptor.ComputeHash(data));
				
			request.Headers["Key"] = Key.key;
			request.Headers["Sign"] = sign;
			request.Method = "POST";
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;

			using (var stream = request.GetRequestStream())
			{
				stream.Write(data, 0, data.Length);
			}

			var response = (HttpWebResponse)request.GetResponse();

			return new StreamReader(response.GetResponseStream()).ReadToEnd();
		}
		
		public static String get(String url)
		{
			var request = (HttpWebRequest)WebRequest.Create(url);
			var response = (HttpWebResponse)request.GetResponse();
			return new StreamReader(response.GetResponseStream()).ReadToEnd();
		}
}