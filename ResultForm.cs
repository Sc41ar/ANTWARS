using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANTWARS
{
	public partial class ResultForm : Form
	{
		internal GameForm gf = null;
		private int ticks;
		public ResultForm()
		{
			InitializeComponent();
		}
		internal ResultForm(string text, GameForm gf)
		{
			InitializeComponent();
			Text = text;
			this.gf = gf;
			gf.Close();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		//e.Graphics.DrawString(Text, Font, new SolidBrush(Color.Crimson), 200, 50);
		}
		private void timer1_Tick(object sender, EventArgs e)
		{
			if(ticks % 5 == 0)
				Close();
			Dispose();
		}
	}
}
