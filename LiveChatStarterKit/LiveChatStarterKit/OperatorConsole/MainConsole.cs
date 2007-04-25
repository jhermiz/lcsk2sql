using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LiveChatStarterKit.OperatorConsole.LiveChatWS;

namespace LiveChatStarterKit.OperatorConsole
{
	public partial class MainConsole : Form
	{
		Operator ws = new Operator();
		private DateTime lastRequestTime = DateTime.Now.AddMinutes(-30);
		private Hashtable currentVisitors = new Hashtable();

		public MainConsole()
		{
			InitializeComponent();
		}

		private void tmrGetWebSiteRequests_Tick(object sender, EventArgs e)
		{
			// we get the latest website requests
			RequestInfo[] requests = ws.GetWebSiteRequests(lastRequestTime);
			if (requests != null && requests.Length > 0)
			{
				// set the last request time
				lastRequestTime = requests[0].RequestTime;

				ListViewItem item = new ListViewItem();
				for (int i = requests.Length - 1; i >= 0; i--)
				{
					item.Text = requests[i].RequestTime.ToShortTimeString();
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].VisitorIP));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].PageRequested));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].Referrer));
					item.SubItems.Add(new ListViewItem.ListViewSubItem(item, requests[i].VisitorUserAgent));

					lstActiveRequests.Items.Insert(0, item);

					// Add the visitor to the visitor hashtable
					if (!currentVisitors.ContainsKey(requests[i].VisitorIP))
						currentVisitors.Add(requests[i].VisitorIP, requests[i].Referrer);
				}

				DisplayStatus();
			}
		}

		private void DisplayStatus()
		{
			toolStripStatuslblVisitors.Text = string.Format("{0} visitor(s) currently on your website", currentVisitors.Count);
		}

		private void onlineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!Program.CurrentOperator.IsOnline)
			{
				ws.SetOperatorStatus(Program.CurrentOperator.OperatorId, true);
				onlineToolStripMenuItem.Checked = true;
				offlineToolStripMenuItem.Checked = false;

				Program.CurrentOperator.IsOnline = true;
			}
		}

		private void offlineToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Program.CurrentOperator.IsOnline)
			{
				ws.SetOperatorStatus(Program.CurrentOperator.OperatorId, false);
				onlineToolStripMenuItem.Checked = false;
				offlineToolStripMenuItem.Checked = true;

				Program.CurrentOperator.IsOnline = false;
			}
		}

		private void MainConsole_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}
	}
}