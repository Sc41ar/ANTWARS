using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class OliveEnemy : Enemy
	{
		public OliveEnemy()
		{
			timeToUpgrade = 10;
			Population = 10;
			PopulationLimit = 15;
			Level = Level;
			PopulationGrowthSpeed = 1;
			Fraction = Fractions.oliveEnemy;
		}

		public OliveEnemy(Point location, int population, Levels level)
		{
			Level = level;
			Size = new Size(75, 75);
			Fraction = Fractions.oliveEnemy;
			Text = Population + "/" + PopulationLimit;
			timer.Tick += Timer_Tick;
			timer.Interval = 20;
			timer.Start();
			timeToUpgrade = 10;
			PopulationLimit = 15;
			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
				@"\Resources\" + Fraction.ToString() + ((int)Level).ToString() + ".png";
			BackgroundImage = Bitmap.FromFile(path);
			Population = population % (PopulationLimit + 1);
			Location = location;
			PopulationGrowthSpeed = 1;
			Fraction = Fractions.oliveEnemy;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			if(_isMouseEntered)
			{
				var pen = new Pen(Color.Magenta, 2f);
				switch((int)Level)
				{
					case 1:
						{
							Debug.WriteLine("ellipse");
							g.DrawEllipse(pen,
								new Rectangle(0, 0, Width, Height));
						}break;
					case 2:
						{
							Debug.WriteLine("Triangle");
							g.DrawPolygon(pen, new Point[]{
								new Point(0, Height-1),
								new Point(Width / 2, 0),
								new Point(Width, Height-1)});
						}
						break;
					case 3:
						{
							Debug.WriteLine("Square");
							g.DrawRectangle(pen, new Rectangle(new Point(0, 0), new Size(Width - 1, Height - 1)));
						}
						break;

				}
			}
		}

		public override void PopulationGrowth()
		{
			if (Population < PopulationLimit)
				Population += PopulationGrowthSpeed;
			Text = Population.ToString() + "/" + PopulationLimit.ToString();
			Invalidate();
		}

		protected internal override void Upgrade()
		{
			int nextLevel = (int)Level + 1;
			Level = (Levels)nextLevel;
			PopulationGrowthSpeed += nextLevel / 3;
			PopulationLimit += nextLevel * 7;
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
			if (tickCount % (timeToUpgrade * 50) == 0 && (int)Level < 3 )//1sec / timer.Interval = 50)
			{
				Upgrade();
			}
		}
	}
}
