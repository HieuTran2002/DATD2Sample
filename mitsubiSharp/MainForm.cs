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
using ActUtlTypeLib;

namespace DATD2Sample
{
    public partial class MainForm : Form
    {
        private static PLCDriver plc = new PLCDriver();
        ConnectWindow connectWindow = new ConnectWindow(plc);
        MonitorWindow monitorWindow = new MonitorWindow(plc);


        public MainForm ()
        {
            InitializeComponent();
            connectWindow.eventLog += new ConnectWindow.passEvent(updateStatus);
            monitorWindow.eventLog += new MonitorWindow.passEvent(updateStatus);

        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectWindow.Visible = true;
            monitorWindow.Visible = false;
        }
        private void monitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            connectWindow.Visible = false;
            monitorWindow.Visible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connectWindow.Visible = false;
            connectWindow.Show();
            connectWindow.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(connectWindow);

            monitorWindow.Visible = false;
            monitorWindow.Show();
            monitorWindow.Dock = DockStyle.Fill;
            splitContainer1.Panel1.Controls.Add(monitorWindow);

            ListViewItem item = new ListViewItem(DateTime.Now.ToString());
            item.SubItems.Add("Setup completed");
            listView1.Items.Add(item);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            listView1.Columns[1].Width = listView1.Width - listView1.Columns[0].Width - 10;
        }

        public void updateStatus(DateTime time, string message)
        {
            ListViewItem item = new ListViewItem(time.ToString());
            item.SubItems.Add(message);
            listView1.Items.Add(item);
        }

    }
}
