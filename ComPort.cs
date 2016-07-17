using System;
using System.IO.Ports;
namespace EcgChart
{
	/// <summary>
	/// Description of ComPort.
	/// </summary>
	public class ComPort
	{
		const string defaultPort = "com2";
		static int baud = 57600;
		SerialPort _sPort;
		public ComPort(string PortName)
		{
			_sPort=initPort(PortName);
		}
		static public string[] GetPorts()
		{
			return SerialPort.GetPortNames();
		}
		SerialPort initPort(string portName)
		{
			
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
		void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			if (!_sPort.IsOpen) return;
			int bytes = _sPort.BytesToRead;
			byte[] buffer = new byte[bytes];
			_sPort.Read(buffer, 0, bytes);
		
//			BeginInvoke(new SetTextDeleg(si_DataReceived),
//			                 new object[] {buffer});
		}
		delegate void SetTextDeleg(byte[] text);
	}
}
