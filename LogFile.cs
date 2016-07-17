using System;
using System.Collections.Generic;

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
			FileTools.WriteTo(log,firstLine);
		}
		bool writeLogTo(string fileName, List<double> text)
		{
			fileName = fileName ?? logPath;
			return FileTools.SaveListTo(fileName,text);
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
