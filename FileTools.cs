using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
namespace EcgChart
{
	/// <summary>
	/// Static class FileTools including WinForms OpenFileDialog and SaveFileDialog
	/// </summary>
	public class FileTools
	{
		static string WorkingDir = Directory.GetCurrentDirectory();
		const string defaultFileFilter = "Текстовые файлы (*.txt)|*.txt";
		
		enum Dialogs {Open, Save};
		static string GetFileNameDialog(Dialogs d) 
		{
			FileDialog fd;
			switch(d) 
			{
				case Dialogs.Open: 
					fd = new OpenFileDialog();
					break;
				case Dialogs.Save: 
					fd = new SaveFileDialog();
					break;
				default:
					return null; 
			}
			fd.InitialDirectory = WorkingDir;
			fd.Filter = defaultFileFilter;
			fd.RestoreDirectory = true ;
			if(fd.ShowDialog() != DialogResult.OK) return null;
			return fd.FileName; 
		}
		static public string GetOpenFileName()
		{
			return GetFileNameDialog(Dialogs.Open);
		}
		
		static public string GetSaveFileName()
		{
			return GetFileNameDialog(Dialogs.Save);
		}
		static List<string> LoadList(string FileName)
		{
			List<string> result = new List<string>();
			StreamReader sr = new StreamReader(FileName);
			string text;
			while ((text=sr.ReadLine())!=null) result.Add(text);
			sr.Close();
			return result; 
		}
		public static List<double> LoadListFromFile(string FileName)
		{
			List<double> v = new List<double>();
			StreamReader sr = new StreamReader(FileName);
			string text;
				while ((text=sr.ReadLine())!=null)
				{
					try 
					{
						double tmp=double.Parse(text);
						v.Add(tmp);
					}
					catch
					{
						continue;
					}
				}
				sr.Close();
			return v; 
		}
	}
}
