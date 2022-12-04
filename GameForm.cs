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
			this.Paint += GameForm_Paint;
		}

		private void GameForm_Paint1(object sender, PaintEventArgs e)
		{
			throw new NotImplementedException();
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

		internal void GameForm_Paint(object sender, PaintEventArgs e)
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			Graphics g = e.Graphics;
			var form = sender as GameForm;
		}

		private void ally1_DragLeave(object sender, EventArgs e)
		{
			
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			//Refresh();
			ally1.Invalidate();
		}
	}
}
