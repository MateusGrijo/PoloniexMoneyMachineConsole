/*
 * Created by SharpDevelop.
 * User: mifus_000
 * Date: 20/05/2017
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace MoneyMachineConsole
{
	/// <summary>
	/// Description of Logger.
	/// </summary>
	public class Logger
	{
		public Logger()
		{
		}
		
		public static void log(string value)
		{
			Console.WriteLine("[" + DateTime.Now.ToString() + "] - " + value);
		}
	}
}
