using System;
using System.Threading;
using System.Text;
using System.IO.Ports;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

using GHIElectronics.NETMF.USBClient;
using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;

namespace FlightTestApp
{
    public class Program
    {
        public static USBC_CDC cdc;
        public static int read_count = 0;
        public static byte[] rx_data = new byte[465];
        public static byte[] tx_data;
        public static SerialPort UART;
        public static void Main()
        {
            cdc = USBClientController.StandardDevices.StartCDC_WithDebugging();

            UART = new SerialPort("COM1", 9600, Parity.None, 8, StopBits.One);
            UART.Open();
            UART.Flush();
            UART.DataReceived += new SerialDataReceivedEventHandler(UART_DataReceived);
            if (USBClientController.GetState() != USBClientController.State.Running)
            {
                Debug.Print("Waiting to connect to PC...");
            }
            else
            {
                while (true)
                {
                    
                    UART.Flush();
                    Thread.Sleep(1000);
                }
            }
        }
        private static void UART_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // read the data
            read_count = UART.Read(rx_data, 0, UART.BytesToRead);

            cdc.Write(rx_data, 0, rx_data.Length);
        }
    }
}



