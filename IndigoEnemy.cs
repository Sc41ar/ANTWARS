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
	internal class IndigoEnemy : Enemy
	{
		public IndigoEnemy()
		{
			timeToUpgrade = 7;
			Population = 0;
			PopulationLimit = 18;
			Level = (Levels)1;
			PopulationGrowthSpeed = 3;
			Fraction = Fractions.indigoEnemy;
		}

		public IndigoEnemy(Point location, int population, Levels level)
		{
			Level = level;
			Size = new Size(75, 75);
			Fraction = Fractions.indigoEnemy;
			Text = Population + "/" + PopulationLimit;
			timer.Tick += Timer_Tick;
			timer.Interval = 1000;
			timer.Start();
			timeToUpgrade = 20;
			PopulationLimit = 30;
			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
				@"\Resources\" + Fraction.ToString() + ((int)Level).ToString() + ".png";
			BackgroundImage = Bitmap.FromFile(path);
			Population = population % (PopulationLimit + 1);
			Location = location;
			PopulationGrowthSpeed = 3;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			g.DrawString(Text, Font, new SolidBrush(Color.Gray),
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
		}
	}
}
