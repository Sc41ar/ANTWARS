using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANTWARS
{
	public partial class LevelTabel : Form
	{
		MainForm mainf;
		public LevelTabel()
		{
			InitializeComponent();
		}

		public LevelTabel(MainForm mainf)
		{
			InitializeComponent();
			this.mainf = mainf;
		}

		private void button7_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 7);
			gf.Show();
			Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 1);
			gf.Show();
			Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 2);
			gf.Show();
			Close();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 3);
			gf.Show();
			Close();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 4);
			gf.Show();
			Close();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 5);
			gf.Show();
			Close();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 6);
			gf.Show();
			Close();
		}

		private void button8_Click(object sender, EventArgs e)
		{


			var gf = new GameForm(mainf, 8);
			gf.Show();
			Close();

		}

		private void button9_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 9);
			gf.Show();
			Close();
		}

		private void button10_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 10);
			gf.Show();
			Close();
		}

		private void button11_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 11);
			gf.Show();
			Close();
		}

		private void button12_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 12);
			gf.Show();
			Close();
		}

		private void button13_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 13);
			gf.Show();
			Close();

		}

		private void button14_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 14);
			gf.Show();
			Close();

		}

		private void button15_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 15);
			gf.Show();
			Close();
		}

		private void button16_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 16);
			gf.Show();
			Close();
		}

		private void button17_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 17);
			gf.Show();
			Close();
		}

		private void button18_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 18);
			gf.Show();
			Close();

		}

		private void button19_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 19);
			gf.Show();
			Close();
		}

		private void button20_Click(object sender, EventArgs e)
		{
			var gf = new GameForm(mainf, 20);
			gf.Show();
			Close();
		}
	}
}
