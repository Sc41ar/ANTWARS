using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANTWARS
{
	public partial class GameForm : Form
	{
		public GameForm()
		{
			InitializeComponent();
		}

		public GameForm(MainForm mf)
		{
			InitializeComponent();
		}

		private void GameForm_Deactivate(object sender, EventArgs e)
		{
			
		}
	}
}
