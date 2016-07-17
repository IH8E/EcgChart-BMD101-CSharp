using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

using ZedGraph;

namespace EcgChart
{
	public partial class Form1 : Form
	{
		SerialPort _sPort;
		string defPort;
		string openButtonText;
		string closeButtonText;
		string logPath;
		List<double> allValues = new List<double>();
		ZedGraphControl defaultGraph;
		LogFile log; 
		MyChart myChart;
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
				catch
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
			BeginInvoke(new SetTextDeleg(si_DataReceived),
			                 new object[] {buffer});
		}

		private delegate void SetTextDeleg(byte[] text);
		public  IEnumerable<double> parseBytes(byte[] data)
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
		double parseByte(List<string> cByte)
		{
//			if(!checkSum(cByte)) return;
			double v1 = double.Parse(cByte[3]);
			double v2 = double.Parse(cByte[4]);
			double v = v1*16*16+v2;
			v = hexToSigned(v);
			return v;
		}
		public void si_DataReceived(byte[] data)
		{
			List<double> values = new List<double>();
			foreach(double v in parseBytes(data))
			{
				values.Add(v);
				myChart.AddPoint(v);
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
			string[] ports = ComPort.GetPorts();//SerialPort.GetPortNames();
			comboBox1.Items.Clear();
			comboBox1.Items.AddRange(ports);
			comboBox1.Text = defPort;
		}
		SerialPort initPort(string portName)
		{
			int baud = 57600;
			try {
				SerialPort port = new SerialPort(portName,baud);
				port.DataReceived += sp_DataReceived;
				return port;
			}
			catch
			{
				return null;
			}
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
		void initAll()
		{
			button1.Text=openButtonText;
			_sPort = initPort(defPort);
			initComboPorts();
			listBox1.DataSource=null;	
			listBox1.Items.Clear();
			log = new LogFile(logPath);
			myChart = new MyChart(defaultGraph);
		}
				
		PointPairList listToPointPairList(List<double> val)
		{
			PointPairList result = new PointPairList ();
			int l=val.Count;
			for(int i=0; i<l;i++) {
				result.Add(i,val[i]);
			}
			return result;
		}
		
		void fileOpenClick(object sender, EventArgs e)
		{
			string fileName=FileTools.GetOpenFileName();
			if(fileName==null) return; 
			List<double>v = FileTools.LoadListFromFile(fileName);
			listBox1.DataSource = v;
			myChart.DrawGraph(v,null,false);
		}
        
        void AddFromFileClick(object sender, EventArgs e)
        {
			string fileName=FileTools.GetOpenFileName();
			if(fileName==null) return; 
			List<double>v = FileTools.LoadListFromFile(fileName);
			listBox1.DataSource = v;
			myChart.DrawGraph(v,"loaded",true);
        }
        
        void SaveFileClick(object sender, EventArgs e)
        {
        	string fileName=FileTools.GetSaveFileName();
		    log.saveAsData(fileName,allValues);
        }
	}
}
