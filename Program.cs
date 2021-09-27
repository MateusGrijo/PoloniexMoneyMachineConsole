/*
 * Created by SharpDevelop.
 * User: mifus_000
 * Date: 20/05/2017
 * Time: 09:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace MoneyMachineConsole
{
	class Program
	{
		


		
		public static void Main(string[] args)
		{
			
		
			Logger.log("# Money Machine version 0.2 #");
			
			while(true)
			{
				String jsonAsString = Http.get("https://poloniex.com/public?command=returnTicker");
				Newtonsoft.Json.Linq.JContainer jcontainer = (Newtonsoft.Json.Linq.JContainer)JsonConvert.DeserializeObject(jsonAsString);
				
				
				foreach (JContainer element in jcontainer.Children()) {
					
					String coin = element.ToString().Split(':')[0].ToString();
					if(coin.IndexOf("BTC") >= 0)
					{
						Newtonsoft.Json.Linq.JContainer detailsContainer = (Newtonsoft.Json.Linq.JContainer)JsonConvert.DeserializeObject("{" + element.ToString().Split('{')[1]);
						decimal last = decimal.Parse( detailsContainer["last"].ToString().Replace(".",","));
						
						decimal high24hr = decimal.Parse( detailsContainer["high24hr"].ToString().Replace(".",","));
						
						if(last >= high24hr)
						{
							Logger.log(element.ToString());
						
						}
					}
				}
				
				
				
				
			
				Logger.log("Sleep 1s");
				System.Threading.Thread.Sleep(200);
			
			}
			
			while(true)
			{
				ReturnTicker returnTicker = ReturnTicker.Create(Http.get("https://poloniex.com/public?command=returnTicker"));
				Logger.log("[SELL] Highest Bid: " + returnTicker.BTC_ETH.highestBid);
				Logger.log("[BUY]  Lowest Ask : " + returnTicker.BTC_ETH.lowestAsk);
				Logger.log("Base volume: " + returnTicker.BTC_ETH.baseVolume);
				Logger.log("High 24h: " + returnTicker.BTC_ETH.high24hr);
				Logger.log("ID: " + returnTicker.BTC_ETH.id);
				Logger.log("Frozen: " + returnTicker.BTC_ETH.isFrozen);
				Logger.log("Last: " + returnTicker.BTC_ETH.last);
				Logger.log("Low 24h: " + returnTicker.BTC_ETH.low24hr);
				Logger.log("Percent Change: " + returnTicker.BTC_ETH.percentChange);
				Logger.log("Quote volume: " + returnTicker.BTC_ETH.quoteVolume);
				Logger.log("");
				
				Logger.log("Sleep 1s");
				System.Threading.Thread.Sleep(1000);
			}
			
			
			
			
			
			
			
//			while(true)
//			{
//				
//				ReturnBalances returnBalances = ReturnBalances.Create(Http.post("https://poloniex.com/tradingApi","command=returnBalances"));
//				
//				Logger.log("## Balance ##");
//				Logger.log("My balance BTC: " + returnBalances.BTC);
//				Logger.log("My balance LTC: " + returnBalances.LTC);
//				Logger.log("");
//				
//				Logger.log("## Ticker BTC/LTC ##");
//				ReturnTicker returnTicker = ReturnTicker.Create(Http.get("https://poloniex.com/public?command=returnTicker"));
//				Logger.log("[SELL] Highest Bid: " + returnTicker.BTC_LTC.highestBid);
//				Logger.log("[BUY]  Lowest Ask : " + returnTicker.BTC_LTC.lowestAsk);
//				Logger.log("Base volume: " + returnTicker.BTC_LTC.baseVolume);
//				Logger.log("High 24h: " + returnTicker.BTC_LTC.high24hr);
//				Logger.log("ID: " + returnTicker.BTC_LTC.id);
//				Logger.log("Frozen: " + returnTicker.BTC_LTC.isFrozen);
//				Logger.log("Last: " + returnTicker.BTC_LTC.last);
//				Logger.log("Low 24h: " + returnTicker.BTC_LTC.low24hr);
//				Logger.log("Percent Change: " + returnTicker.BTC_LTC.percentChange);
//				Logger.log("Quote volume: " + returnTicker.BTC_LTC.quoteVolume);
//				Logger.log("");
//				
//				if(double.Parse(returnBalances.LTC.Replace(".",",")) <= 0.01)
//				{
//					Logger.log("Lets buy LTC...");
//					Logger.log("Lower value in 24h " + returnTicker.BTC_LTC.low24hr);
//					Logger.log("Try buy fast...");
//					
//					MyOperation myOperation = new MyOperation();
//					myOperation.typeOrder = "buy";
//					myOperation.amount = "0.01";
//					myOperation.currencyPair = "BTC_LTC";
//					myOperation.rate = returnTicker.BTC_LTC.low24hr;
//					myOperation.date = DateTime.Now;
//					
//					ReturnOperation returnOperation = ReturnOperation.Create(Http.post("https://poloniex.com/tradingApi","command="+myOperation.typeOrder+"&currencyPair="+myOperation.currencyPair+"&amount="+myOperation.amount+"&rate=" + myOperation.rate));
//					
//					myOperation.orderNumber = returnOperation.orderNumber.ToString();
//					Database.SerializeObject(myOperation);
//					
//				}
//				else
//				{
//					
//					List<ReturnOpenOrders> orders = ReturnOpenOrders.Create(Http.post("https://poloniex.com/tradingApi","command=returnOpenOrders&currencyPair=BTC_LTC"));
//					
//					if(orders.Count <= 0 )
//					{
//						MyOperation myOperation = Database.DeSerializeObject<MyOperation>();
//						
//						Logger.log("Lets sell LTC...");
//						Logger.log("Lower value in 24h " + returnTicker.BTC_LTC.high24hr);
//						Logger.log("Try sell fast...");
//						
//						Logger.log("[buy ] " + myOperation.rate);
//						Logger.log("[sell] " + myOperation.rateSell(returnTicker).ToString());
//						Logger.log("[prof] " + myOperation.profit(returnTicker).ToString());
//						
//						ReturnOperation returnOperation = ReturnOperation.Create(Http.post("https://poloniex.com/tradingApi","command=sell&currencyPair=BTC_LTC&amount=0.01&rate=" +  myOperation.rateSell(returnTicker).ToString()));
//					}
//					else
//					{
//						Logger.log("Wait order sell...");
//						Logger.log("rate order: " + orders[0].rate.ToString());
//					}
//					
//					//List<ReturnHistory> history = ReturnHistory.Create(Http.post("https://poloniex.com/tradingApi","command=returnOrderTrades&orderNumber=" + returnOperation.orderNumber));
//
//				}
//				
//
//				
//				
//				Logger.log("Sleep 1s");
//				System.Threading.Thread.Sleep(100000000);
//			}
		}
		
	}
}