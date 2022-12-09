using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class Unit : Panel
	{
		public int Population { get; set; }
		public Point Destination { get; set; }
		private readonly System.Windows.Forms.Timer tiger = new System.Windows.Forms.Timer();
		private readonly int deltax;
		private readonly int deltay;
		private int xStep;
		private int yStep;
		private int tickCount;
		Unit()
		{
			BackColor = Color.White;
		}

		public Unit(Point startLocation, Point destination, int Population)
		{
			Location = startLocation;
			Destination = destination;
			BackColor = Color.Transparent;
			BackgroundImage = Resource1.Unit;
			BackgroundImageLayout = ImageLayout.Stretch;
			deltax = Destination.X - Location.X;
			deltay = Destination.Y - Location.Y;
			xStep = deltax / 100;
			yStep = deltay / 100;
			tiger.Interval = 20;
			tiger.Tick += Tiger_Tick;
			tiger.Enabled = true;
			tiger.Start();
			Size = new Size(30, 30);
			this.Population = Population;
		}

		void Step()
		{
			Location = new Point(Location.X + xStep, Location.Y + yStep);
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			e.Graphics.DrawString(Population.ToString(),
				Font,
				new SolidBrush(Color.Crimson),
				new Point(Location.X + Width / 2,
					Location.Y + Height / 2));
		}

		private bool IsArrived()
		{
			if ((Location.X + Width / 2 >= Destination.X - deltax ||
				Location.X + Width / 2 <= Destination.X + deltax) &&
				(Location.Y + Height / 2 >= Destination.Y - deltay ||
				Location.Y + Height / 2 <= Destination.Y + deltay))
				return true;
			return false;
		}

		private void Tiger_Tick(object sender, EventArgs e)
		{
			Step();
			tickCount++;
			if (IsArrived())
			{
				tiger.Stop();
				this.Parent = null;
				Thread.Sleep(100);
				Invalidate();
			}
		}
	}
}
