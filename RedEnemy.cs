using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class RedEnemy : Enemy
	{

		private int timeToAttack;

		public RedEnemy()
		{
			timeToUpgrade = 7;
			Population = 0;
			PopulationLimit = 18;
			Level = (Levels)1;
			PopulationGrowthSpeed = 1;
			Fraction = Fractions.redEnemy;
		}

		public RedEnemy(Point location, int population, Levels level)
		{

			int seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(seed);
			timeToAttack = rnd.Next(15) + 2;
			Debug.WriteLine("Время до Красного нападения: " + timeToAttack);
			Level = level;
			Size = new Size(75, 75);
			Fraction = Fractions.redEnemy;
			Text = Population + "/" + PopulationLimit;
			timer.Tick += Timer_Tick;
			timer.Interval = 1000;
			timer.Start();
			timeToUpgrade = 25;
			PopulationLimit = 25;
			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
				@"\Resources\" + Fraction.ToString() + ((int)Level).ToString() + ".png";
			BackgroundImage = Bitmap.FromFile(path);
			Population = population % (PopulationLimit + 1);
			Location = location;
			PopulationGrowthSpeed = 2;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			g.DrawString(Text, Font, new SolidBrush(Color.LightGray),
				Width / 2, 2 * Height / 5, _format);
			if (_isMouseEntered)
			{
				var pen = new Pen(Color.Magenta, 2f);
				switch ((int)Level)
				{
					case 1:
						{
							g.DrawEllipse(pen,
								new Rectangle(0, 0, Width, Height));
						}
						break;
					case 2:
						{
							g.DrawPolygon(pen, new Point[]{
								new Point(0, Height-1),
								new Point(Width / 2, 0),
								new Point(Width, Height-1)});
						}
						break;
					case 3:
						{
							g.DrawRectangle(pen, new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1)));
						}
						break;

				}
			}
		}

		protected internal override void Upgrade()
		{
			int nextLevel = (int)Level + 1;
			Level = (Levels)nextLevel;
			PopulationGrowthSpeed += nextLevel / 2;
			PopulationLimit += nextLevel * 10;
			Invalidate();
		}

		public void Attack()
		{
			var gf = this.Parent as GameForm;
			if (gf != null)
			{
				var targets = from item in gf.Colonies
								  where !(item is RedEnemy)
								  select item;
				var runs = 0;
				var seed = (int)DateTime.Now.Ticks;
				var rnd = new Random(seed);
				var target = rnd.Next(targets.Count());
				foreach (var e in targets)
				{
					runs++;
					if (runs == target && e.Population < Population)
					{
						Point centre = new Point(Location.X + Width / 2
							, Location.Y + Height / 2);
						try
						{
							var unit = new Unit(Location, Population, this, e);
							unit.Parent = this.Parent;
							unit.Invalidate();
						}
						catch (Exception ex)
						{
							Debug.WriteLine(ex);
						}
						Population = 0;
						Invalidate();
					}
				}
			}
			else
			{
				Dispose();
			}
		}

		protected override void OnLevelChanged(LevelEventArgs e)
		{
			base.OnLevelChanged(e);
			Debug.WriteLine(Fraction.ToString() + "- Fraction");
			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
				@"\Resources\" + Fraction.ToString() + ((int)Level).ToString() + ".png";
			BackgroundImage = Bitmap.FromFile(path);
			Invalidate();
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_isMouseEntered = true;
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_isMouseEntered = false;
		}

		protected void Timer_Tick(object sender, EventArgs e)
		{
			tickCount++;
			if (tickCount % (timeToUpgrade) == 0 && (int)Level < 4)//1sec / timer.Interval
			{
				Upgrade();
			}
			if (tickCount % timeToAttack == 0)
			{
				Attack();
			}
		}
	}
}
