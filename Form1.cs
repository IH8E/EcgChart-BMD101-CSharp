using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

using ZedGraph;

namespace EcgChart
{
	public partial class Form1 : Form
	{
		SerialPort _sPort;
		private string defPort;
		private int pointCount;
		private string openButtonText;
		private string closeButtonText;
		string logPath;
		List<double> values0 = new List<double>();
		List<double> allValues = new List<double>();
		ZedGraphControl defaultGraph;
		LogFile log; 
		MyChart myChart;
		string defaultFilterForFiles = "Текстовые файлы (*.txt)|*.txt";
		public Form1()
		{
			InitializeComponent();
			defPort = "COM2";
			openButtonText = "Открыть";
			closeButtonText = "Закрыть";
			logPath = @".\log.txt";
			defaultGraph = zedGraphControl1;
			initAll();
		
		}
		
		void button1_Click(object sender, EventArgs e)
		{
			listBox1.DataSource=null;
			if(_sPort.IsOpen)
			{
				
				_sPort.Close();
				listBox1.Items.Add("Port closed: " + _sPort.PortName);
				log.saveData(allValues);
				button1.Text=openButtonText;
			}
			else
			{
				listBox1.Items.Clear();
				try
				{
					_sPort.Open();
					
					listBox1.Items.Add("Port opened: " + _sPort.PortName);
					button1.Text=closeButtonText;
				}
				catch(Exception ex)
				{
					listBox1.Items.Add("Port unavaliable: " + _sPort.PortName);
				}
				
			}
			//            _sPort.Open();
			
		}

		void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			if (!_sPort.IsOpen) return;
			int bytes = _sPort.BytesToRead;
			byte[] buffer = new byte[bytes];
			_sPort.Read(buffer, 0, bytes);
			this.BeginInvoke(new SetTextDeleg(si_DataReceived),
			                 new object[] {buffer});
		}

		private delegate void SetTextDeleg(byte[] text);
		
		void si_DataReceived(byte[] data)
		{
			GraphPane pane = zedGraphControl1.GraphPane;
			
			
			List<double> values = new List<double>();
			try {
				
				List<string> cByte = new List<string>();
				for (int i=0, length=data.Length; i<length; i++) {
					if(data[i]==170) {
						if(data[i+1]==170) {							
							if (cByte.Count==6 ) {
								double v1 = double.Parse(cByte[3]);
								double v2 = double.Parse(cByte[4]);
								double v = v1*16*16+v2;
								v = hexToSigned(v);
								values.Add(v);
								pane.CurveList[0].AddPoint(pointCount,v);
								zedGraphControl1.AxisChange ();
								zedGraphControl1.Invalidate ();
								pointCount++;
							}
							cByte.Clear();
						}
					}
					else {
						cByte.Add(data[i].ToString());
					}
				}
			}
			catch
			{
			}
			allValues.AddRange(values);
			
		}

		void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			_sPort.Close();

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
			MessageBox.Show(msg);
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
		void initComboPorts()
		{
			string[] ports = SerialPort.GetPortNames();
//			Array.Reverse(ports);
			comboBox1.Items.Clear();
			comboBox1.Items.AddRange(ports);
			comboBox1.Text = defPort;
		}
		SerialPort initPort(string portName)
		{
			try {
				SerialPort port = new SerialPort(portName);
				port.BaudRate = 57600;
				port.Parity=Parity.None;
				port.StopBits=StopBits.One;
				port.DataBits = 8;
				port.Handshake = Handshake.RequestToSend;
				port.DtrEnable = true;
				port.RtsEnable = true;
				port.WriteTimeout = 20;
				port.ReadTimeout = 2;
				port.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
				return port;
			}
			catch(Exception e) {
				return null;
			}
		}
		public static byte[] FromHex(string hex)
		{
			hex = hex.Replace("-", "");
			byte[] raw = new byte[hex.Length / 2];
			for (int i = 0; i < raw.Length; i++)
			{
				raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
			}
			return raw;
		}
		double hexToSigned(double vd) {
			int v = (int)vd;
			if ((v & 0x8000) > 0) {
				v = v - 0x10000;
			}
			return v;
		}
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			if(_sPort.IsOpen) {
				_sPort.Close(); button1.Text=openButtonText;
			}
			_sPort = initPort(comboBox1.Text);
			
		}
		
		void Button2Click(object sender, EventArgs e)
		{
			initAll();
		}
		private void initAll()
		{
			_sPort = initPort(defPort);
			initComboPorts();
			listBox1.DataSource=null;	
			listBox1.Items.Clear();
			log = new LogFile(logPath);
			myChart = new MyChart(defaultGraph);
		}
				
		private PointPairList listToPointPairList(List<double> val)
		{
			PointPairList result = new PointPairList ();
			int l=val.Count;
			for(int i=0; i<l;i++) {
				result.Add(i,val[i]);
			}
			return result;
		}
		List<double> LoadListFromFile(string FileName)
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
		void fileOpenClick(object sender, EventArgs e)
		{
			List<double> v;
			OpenFileDialog of = new OpenFileDialog();
			string path = Directory.GetCurrentDirectory();
			of.InitialDirectory = path ;
			of.Filter = defaultFilterForFiles;
			of.RestoreDirectory = true ;
			
			if(of.ShowDialog() != DialogResult.OK) return;
			v = LoadListFromFile(of.FileName);
				listBox1.DataSource = v;
				
				myChart.DrawGraph(v,null,false);
			
		}
        
        void AddFromFileClick(object sender, EventArgs e)
        {
			List<double> v;
			OpenFileDialog of = new OpenFileDialog();
			string path = Directory.GetCurrentDirectory();
			of.InitialDirectory = path ;
			of.Filter = defaultFilterForFiles;
			of.RestoreDirectory = true ;
			
			if(of.ShowDialog() != DialogResult.OK) return;
			v = LoadListFromFile(of.FileName);
				listBox1.DataSource = v;
				myChart.DrawGraph(v,"loaded",true);
        }
        
        void SaveFileClick(object sender, EventArgs e)
        {
		     SaveFileDialog sf = new SaveFileDialog();
		     sf.Filter = defaultFilterForFiles;
		     sf.RestoreDirectory = true ;
		     if(sf.ShowDialog() != DialogResult.OK) return;
		     log.saveAsData(sf.FileName,allValues);
        }
	}
}
