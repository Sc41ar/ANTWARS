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
	internal partial class GameForm : Form
	{
		MainForm mainf;
		public List<NeutralColony> Colonies;
		public List<PictureBox> Images;
		private int _ticks;
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
			Colonies = new List<NeutralColony>();
			Images = new List<PictureBox>();
			Colonies.Add(new Ally(new Point(75, 100), 20, Levels.first));
			Colonies.Add(new NeutralColony(new Point(225, 300), 10, Levels.neutral));
			Colonies.Add(new NeutralColony(new Point(75, 540), 10, Levels.neutral));
			Colonies.Add(new NeutralColony(new Point(125, 200), 10, Levels.neutral));
			Colonies.Add(new NeutralColony(new Point(225, 100), 10, Levels.neutral));
			Colonies.Add(new OliveEnemy(new Point(331, 100), 14, Levels.first));
			Colonies.Add(new BlueEnemy(new Point(456, 100), 14, Levels.first));
			Colonies.Add(new RedEnemy(new Point(581, 100), 20, Levels.first));
			Colonies.Add(new IndigoEnemy(new Point(706, 100), 25, Levels.first));
			for (int i = 0; i < 25; i++)
			{
				Images.Add(new PictureBox());
				Images.ElementAt(i).BackColor = Color.Transparent;
				Images.ElementAt(i).Image = Resource1.Unit;
			}
		}

		public void AddAllyColony(Point location, int population, Levels level)
		{

			Colonies.Add(new Ally(location, population, Levels.first));
			Update();
		}

		private void GameForm_Deactivate(object sender, EventArgs e)
		{
			//mainf.Show();
			//mainf.Close();
		}

		internal void GameForm_Paint(object sender, PaintEventArgs e)
		{
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			foreach (var item in Colonies)
			{
				Controls.Add(item);
				item.Parent = this;
				item.Show();
				item.Update();
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			g.DrawString(_ticks.ToString(), Font, new SolidBrush(Color.DarkOrange), new Point(Width - 100, 30));
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			_ticks++;
			var Allies =
				from t in Colonies
				where t is Ally
				select t;
			var enemies = from t in Colonies
							  where t is Enemy
							  select t;
			foreach (var enemy in enemies)
			{
				enemy.PopulationGrowth();
			}
			foreach (var item in Allies)
			{
				item.PopulationGrowth();
			}
			Invalidate();
		}
	}
}
