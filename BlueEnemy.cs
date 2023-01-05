using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class BlueEnemy : Enemy
	{
		private int timeToAttack;
		public BlueEnemy()
		{
			timeToUpgrade = 7;
			Population = 0;
			PopulationLimit = 18;
			Level = (Levels)1;
			PopulationGrowthSpeed = 1;
			Fraction = Fractions.blueEnemy;
		}

		public BlueEnemy(Point location, int population, Levels level)
		{
			int seed = (int)DateTime.Now.Ticks;
			Random rnd = new Random(seed);
			timeToAttack = rnd.Next(20) + 7;
			Debug.WriteLine("Время до синего нападения: " + timeToAttack);
			Level = level;
			Size = new Size(75, 75);
			Fraction = Fractions.blueEnemy;
			Text = Population + "/" + PopulationLimit;
			timer.Tick += Timer_Tick;
			timer.Interval = 1000;
			timer.Start();
			timeToUpgrade = 7;
			PopulationLimit = 18;
			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
				@"\Resources\" + Fraction.ToString() + ((int)Level).ToString() + ".png";
			BackgroundImage = Bitmap.FromFile(path);
			Population = population % (PopulationLimit + 1);
			Location = location;
			PopulationGrowthSpeed = 1;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			g.DrawString(Text, Font, new SolidBrush(Color.Crimson),
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
						var gf = this.Parent as GameForm;
						if (gf.Colonies.Contains(this))
						{

							Debug.WriteLine("У ВАС 2 ЧИЧА");
						}
						else
						{
							Dispose();
						}
				}
			}
		}

		internal void Attack()
		{
			var gf = this.Parent as GameForm;
			if (gf != null)
			{
				var targets = from item in gf.Colonies
								  where !(item is Enemy)
								  select item;
				var runs = 0;
				int seed = (int)DateTime.Now.Ticks;
				var rnd = new Random(seed);
				var target = rnd.Next(targets.Count()) + 1;
				foreach (var e in targets)
				{
					runs++;
					if (runs == target)
					{
						Point centre = new Point(Location.X + Width / 2
								, Location.Y + Height / 2);
						_ = new Unit(centre, Population, this, e)
						{
							Parent = this.Parent
						};
						Population = 0;
					}
				}
			}
			else
			{
				Dispose();
			}
		}

		protected internal override void Upgrade()
		{
			int nextLevel = (int)Level + 1;
			Level = (Levels)nextLevel;
			PopulationGrowthSpeed += nextLevel / 3;
			PopulationLimit += nextLevel * 8;
			Invalidate();
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
			if (tickCount % (timeToUpgrade) == 0 && (int)Level < 3)//1sec / timer.Interval
			{
				Upgrade();
			}
		}
	}
}
