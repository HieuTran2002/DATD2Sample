using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace ArduinoCSharp
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort = new SerialPort();
        private bool connected = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                portBox.Items.AddRange(SerialPort.GetPortNames());
                portBox.SelectedIndex = 0;
                serialPort.BaudRate = 9600;
                serialPort.ReadTimeout = 2000;
                serialPort.WriteTimeout = 2000;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void portBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort.PortName = portBox.Text;
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
                serialPort.Open();
                serialPort.WriteLine("on");
                serialPort.Close();
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
                serialPort.Open();
                serialPort.WriteLine("off");
                serialPort.Close();
        }
    }
}
