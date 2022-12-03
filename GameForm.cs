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
		MainForm mainf;
		public Bitmap t1 = Resource1.player1,
			t2 = Resource1.indigoEnemy;
		public GameForm()
		{
			InitializeComponent();
		}

		public GameForm(MainForm mf)
		{
			InitializeComponent();
			mainf = mf;
		}

		private void GameForm_Deactivate(object sender, EventArgs e)
		{
			mainf.Show();
		}

		private void GameForm_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			
			g.DrawImage(t1, new Rectangle(20, 20, 100, 100));
			g.DrawImage(t2 , new Rectangle(500, 350, 100, 100));
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			Refresh();
		}
	}
}
