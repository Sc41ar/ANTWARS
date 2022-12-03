using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANTWARS
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}

		private void GameStart_Click(object sender, EventArgs e)
		{
			GameForm gameForm = new GameForm();
			gameForm.Show();
			this.Hide();
		}
	}
}
