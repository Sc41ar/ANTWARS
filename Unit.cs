using System;
using System.Drawing;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class Unit : Panel
	{
		public int Size { get; set; }
		public Point Destination { get; set; }
		private Timer tiger = new Timer();
		private int deltax;
		private int deltay;
		private int xStep;
		private int yStep;
		private int tickCount;
		Unit()
		{
			BackColor = Color.White;
		}

		public Unit(Point startLocation, Point destination, int size)
		{
			Location = startLocation;
			Destination = destination;
			BackColor = Color.Transparent;
			BackgroundImage = Resource1.Unit;
			BackgroundImageLayout = ImageLayout.Stretch;
			deltax = Destination.X - Location.X;
			deltay = Destination.Y - Location.Y;
			xStep = deltax / 75;
			yStep = deltay / 75;
			tiger.Interval = 20;
			tiger.Tick += tiger_Tick;
			tiger.Enabled = true;
			tiger.Start();
			Size = size;
		}

		void Step()
		{
			Location = new Point(Location.X + xStep, Location.Y + yStep);
			Invalidate();
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

		}

		private void tiger_Tick(object sender, EventArgs e)
		{
			Step();
			tickCount++;
			if (tickCount == 75)
			{
				tiger.Stop();
				this.Parent = null;
				Invalidate();
			}
		}
	}
}
