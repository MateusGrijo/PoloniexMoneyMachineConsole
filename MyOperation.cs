
using System;
public class MyOperation
{
	public string orderNumber { get; set; }
	public string typeOrder { get; set; }
	public string currencyPair { get; set; }
	public string amount { get; set; }
	public string rate { get; set; }
	public DateTime date { get; set; }
	
	public string rateSell(ReturnTicker returnTicker)
	{
		
		if(returnTicker != null)
		{
			decimal last =  decimal.Parse(returnTicker.BTC_LTC.last.Replace(".",","));
			decimal fee = (decimal.Parse(rate.Replace(".",",")) * decimal.Parse(Key.fee.ToString().Replace(".",","))) / 100;
			last = last + fee;
			decimal _drate = decimal.Parse(rate.Replace(".",","));
			if( last > _drate )
				return returnTicker.BTC_LTC.last;
		}
		
		 String _rate = rate.Replace(".",",");
		 _rate = _rate;
		_rate = Convert.ToString( ((double.Parse(_rate) * Key.fee) / 100) + double.Parse(_rate) + ((double.Parse(_rate) * Key.profit) / 100)).Replace(",",".");
		if(_rate.Length > 10)
			_rate = _rate.Substring(0,10);
		return _rate;
	}
	
	public string profit(ReturnTicker returnTicker)
	{
		decimal _rateSell = decimal.Parse(rateSell(returnTicker).Replace(".",","));
		decimal _rate = decimal.Parse(rate.Replace(".",","));
		decimal total= _rateSell - _rate;
		return total.ToString().Replace(",",".");
		
	}
	
}

