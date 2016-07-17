using System;
using System.Collections.Generic;
using System.Linq;
namespace EcgChart
{
	/// <summary>
	/// Description of IncomingData.
	/// </summary>
	public class IncomingData
	{
		public IncomingData()
		{
		}
		public static IEnumerable<double> parseBytes(byte[] data)
		{
			int ByteLength = 6;
			List<string> cByte = new List<string>();
			for (int i=0, length=data.Length; i<length; i++) 
			{
				if(data[i]==170) {
					if(i<=length-ByteLength && data[i+1]==170) {							
						if (cByte.Count==ByteLength) {
							yield return parseByte(cByte);
						}
						cByte.Clear();
					}
				}
				else 
				{
					cByte.Add(data[i].ToString());
				}
			}
		}
		static double parseByte(List<string> cByte)
		{
//			if(!checkSum(cByte)) return;
			double v1 = double.Parse(cByte[3]);
			double v2 = double.Parse(cByte[4]);
			double v = v1*16*16+v2;
			v = hexToSigned(v);
			return v;
		}
		void testCheckSum()
		{
			List<int> foo = new List<int>();
			foo.Add(128);
			foo.Add(2);
			foo.Add(9);
			foo.Add(82);
			foo.Add(34);
			string msg = checkSum(foo)?"ok":"nope";
//			MessageBox.Show(msg);
		}
		bool checkSum(List<int> arr)
		{
			int length, checkValue, checksum;
			length = arr.Count;
			if(length<2) return false;
			checkValue = arr[length-1]; arr.RemoveAt(length-1); //Array.pop();
			checksum = arr.Sum(); 
			checksum &= 0xFF;
			checksum = ~checksum & 0xFF;
			return checksum==checkValue;
		}
		static double hexToSigned(double vd) {
			int v = (int)vd;
			if ((v & 0x8000) > 0) {
				v = v - 0x10000;
			}
			return v;
		}
	}
}
