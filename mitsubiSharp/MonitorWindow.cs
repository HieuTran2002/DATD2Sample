using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using PLC;
using SymbolFactoryDotNet;

namespace DATD2Sample
{
    public partial class MonitorWindow : UserControl
    {
        private PLCDriver plc;
        public passEvent eventLog;
        MouseButtons lastMouseDown;
        List<Control> readControl = new List<Control> { };
        List<Control> writeControl = new List<Control> { };

        private System.Windows.Forms.Timer timer1;
        
        public MonitorWindow(PLCDriver driver)
        {
            InitializeComponent();
            plc = driver;

            timer1 = new System.Windows.Forms.Timer();
            timer1.Interval = 100;

            timer1.Tick += timerTick;


        }

        private async void timerTick(object sender, EventArgs e)
        {
            if (plc.connected)
            {
                foreach (Control control in readControl)
                {
                    control.Visible = (await Task.Run(() => updateControl(control)));
                }

                //Thread t = new Thread(() =>
                //{
                //    this.Invoke((Action)async delegate ()
                //    {
                //        //cylinder2A.Visible = !(await plc.read(cylinder2A.Tag.ToString()) == 1);
                //        foreach (Control control in Controls)
                //        {
                //            control.Visible = (await Task.Run(() => updateControl(control)));
                //        }

                //    });
                //});
                //t.IsBackground = true;
                //t.Start();
                
            }
            else
            {
                foreach (Control control in Controls)
                {
                    control.Visible = true;
                }
            }
        }
        
        private async Task<bool> updateControl(Control control)
        {
            return !(await plc.read(control.Tag.ToString()) == 1);
        }

        private void writeBTN_Click(object sender, EventArgs e)
        {

        }

        public delegate void passEvent(DateTime time, string message);

        private void MonitorWindow_Load(object sender, EventArgs e)
        {
            eventLog.Invoke(DateTime.Now, "Setup monitor window completed");

            Thread t = new Thread(() =>
            {
                this.Invoke((Action)delegate ()
                {
                    
                    timer1.Start();
                });
            });
            t.IsBackground = true;
            t.Start();

        }


        private void btn_Click(object sender, EventArgs e)
        {
        }
        private void BTN_MouseDown(object sender, MouseEventArgs e)
        {
            lastMouseDown = MouseButtons;
            if (sender != null)
            {
                try
                {
                    if ( sender is SymbolFactoryDotNet.StandardControl)
                    {
                        Control control = sender as SymbolFactoryDotNet.StandardControl;

                        if(MouseButtons == MouseButtons.Left && plc.connected && writeControl.Contains(control)) 
                        {
                            plc.write(control.Tag.ToString(), 1);
                        }
                        else if (MouseButtons == MouseButtons.Right && !plc.connected)
                        {
                            contextMenuStrip1.Show(MousePosition);

                            changeAddressDialog changeAddressDialog = new changeAddressDialog(MousePosition);
                            changeAddressDialog.Show();
                            changeAddressDialog.FormClosed += (object sender2, FormClosedEventArgs e2) =>
                            {
                                changeAddressDialog dialog = sender2 as changeAddressDialog;
                                if (dialog.DialogResult == DialogResult.OK)
                                {
                                    if (dialog.func == "Read")
                                    {
                                        if (readControl.Contains(control))
                                        {
                                            readControl[readControl.IndexOf(control)] = control;
                                            control.Tag = dialog.output;
                                        }                                    
                                        else
                                        {
                                            readControl.Add(control);
                                            control.Tag = dialog.output;

                                        }
                                    }
                                    if (dialog.func == "Write")
                                    {
                                        if (writeControl.Contains(control))
                                        {
                                            writeControl[writeControl.IndexOf(control)] = control;
                                            control.Tag = dialog.output;
                                        }                                    
                                        else
                                        {
                                            writeControl.Add(control);
                                            control.Tag = dialog.output;
                                        }
                                    }
 
                                    eventLog.Invoke(DateTime.Now, "Update address for " + control.Name + " to " + dialog.output + " " + readControl.Count.ToString());

                                }
                            };
                        }

                    }
                }
                catch (Exception ex)
                {
                    eventLog.Invoke(DateTime.Now, ex.Message);
                }
            }
        }


        private void BTN_MoustUp(object sender, MouseEventArgs e)
        {
            if (lastMouseDown == MouseButtons.Left && sender is StandardControl && plc.connected)
            {
                plc.write((sender as SymbolFactoryDotNet.StandardControl).Tag.ToString(), 0);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
