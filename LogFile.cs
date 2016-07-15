/*
 * Сделано в SharpDevelop.
 * Пользователь: IH8E
 * Дата: 14.07.2016
 * Время: 9:46
 */
using System;
using System.Collections.Generic;
using System.IO;
namespace EcgChart
{
	/// <summary>
	/// Class for logging input data
	/// </summary>
	public class LogFile
	{
		string logPath;
		public LogFile(string fileName)
		{
			logPath = fileName; 
			createLog(logPath);
		}
		void createLog(string log) {
			string firstLine = String.Format("Log started: {0:dd.MM.yyy HH:mm:ss}",DateTime.Now)+Environment.NewLine;
			System.IO.File.WriteAllText(log,firstLine);
		}
		bool writeLogTo(string fileName, List<double> text)
		{
			fileName = (fileName!=null)?fileName:logPath;
			try 
			{
				using (StreamWriter sw = File.AppendText(fileName)) 
				{
					foreach (var v in text) 
					{
						sw.WriteLine(v);
					}
				}
				return true;
			}
			catch
			{
				return false;
			}
			
		}
		public bool saveData(List<double> text)
		{
			return writeLogTo(null,text);
		}
		public bool saveAsData(string fileName,List<double> text)
		{
			return writeLogTo(fileName,text);
		}
	}
}
