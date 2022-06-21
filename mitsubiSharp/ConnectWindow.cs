using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLC;
using AxActUtlTypeLib;

namespace DATD2Sample
{
    public partial class ConnectWindow : UserControl
    {
        private PLCDriver plc;
        public passEvent eventLog;
        public ConnectWindow(PLCDriver driver)
        {
            InitializeComponent();
            plc = driver;

        }
        private void Connect_Click(object sender, EventArgs e)
        {

            try
            {
                if (!plc.connected)
                {
                    bool result = plc.Connect((int)numericUpDown2.Value);
                    if (result)
                    {
                        cnnBTN.Text = "Disconnect";
                        eventLog.Invoke(DateTime.Now, "Connect success");
                    }
                    else
                    {
                        eventLog.Invoke(DateTime.Now, "Connect failed");
                    }
                }
                else
                {
                    try
                    {
                        bool success = plc.Disconnect();
                        if (success)
                        {
                            cnnBTN.Text = "Connect";
                            eventLog.Invoke(DateTime.Now, "Disconnected");
                        }
                    }
                    catch (Exception ex)
                    {
                        eventLog.Invoke(DateTime.Now, ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                eventLog.Invoke(DateTime.Now, ex.Message);
            }


               
        }
        public delegate void passEvent(DateTime time, string message);

        private void ConnectWindow_Load(object sender, EventArgs e)
        {
            eventLog.Invoke(DateTime.Now, "Setup connect window completed");
        }
    }
}
