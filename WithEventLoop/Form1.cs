using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WithEventLoop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ReportThread()
        {
            lbxList.Items.Add($"Thread Id: {Thread.CurrentThread.ManagedThreadId}");
        }

        private async void btnGo_Click(object sender, EventArgs e)
        {
            WebClient client = new WebClient();

            lbxList.Items.Add("Starting call...");

            ReportThread();

            var stream = await client.OpenReadTaskAsync(new Uri("https://bbc.co.uk"));

            ReportThread();

            lbxList.Items.Add("Results in...");

            lbxList.Items.Add(new StreamReader(stream).ReadLine());
        }
    }
}
