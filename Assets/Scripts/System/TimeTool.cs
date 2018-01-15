using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTool {

	public static string ConvertFloatToTimeString(float src){
		TimeSpan t = TimeSpan.FromSeconds( src );

		string answer = string.Format("{0:D2}:{1:D2}:{2:D2}", 
			Convert.ToInt32(Math.Floor(t.TotalHours)), 
			t.Minutes, 
			t.Seconds );

		return answer;
	}

	public static string ConvertDateToChtDay(DateTime t){
		return t.ToString ("yyyy") + "年" + t.ToString ("MM") + "月" + t.ToString ("dd") + "日";
	}

	public static string ConvertDateToHHMMSS(DateTime t){
		return t.ToString ("HH:mm:ss");
	}
}
