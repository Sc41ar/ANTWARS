using System;
using System.Diagnostics;
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
		private int deltax;
		private int deltay;
		private int xStep;
		private int yStep;
		private int tickCount;
		private Ally attacker;
		private NeutralColony target;
		Unit()
		{
			BackColor = Color.White;
		}

		public Unit(Point startLocation, int Population, Ally attacker, NeutralColony target)
		{
			Location = startLocation;
			Destination = /*target.Location;*/ new Point(target.Location.X + target.Width / 2,
				target.Location.Y + target.Height / 2);
			BackColor = Color.Transparent;
			BackgroundImage = Resource1.Unit;
			BackgroundImageLayout = ImageLayout.Stretch;
			deltax = Destination.X - Location.X;
			deltay = Destination.Y - Location.Y;
			xStep = deltax / 60;
			yStep = deltay / 60;
			Debug.WriteLine("x {0}, y {1}, xs {2}, xy {3}", deltax, deltay, xStep, yStep);
			tiger.Interval = 20;
			tiger.Tick += Tiger_Tick;
			tiger.Enabled = true;
			tiger.Start();
			Size = new Size(30, 30);
			this.Population = Population;
			this.attacker = attacker;
			this.target = target;
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

		public bool IsArrived(Point current)
		{

			//Debug.WriteLine(current.ToString());
			if ((current.X >= Destination.X - 5 &&
				current.X <= Destination.X + 5) &&
				(current.Y >= Destination.Y - 5 &&
				current.Y <= Destination.Y + 5))
			{
				attacker._isArrived = true;
				return true;
			}
			return false;
		}

		void TargetAction()
		{
			var form = this.Parent as GameForm;
			var currentMousePos = form.PointToClient(Cursor.Position);
			if (!(target is Ally))
			{
				target.IsAttacked = true;
				if (Population >= target.Population)
				{
					form.AddAllyColony(target.Location, Population - target.Population, Levels.first);
					form.Colonies.Remove(target);
					form.Controls.Remove(target);
					attacker.Money += ((int)target.Level+1) * 10;
					target.Update();
				}
				else
				{
					target.Population -= Population;
				}
			}
			else if (target is Ally || target.IsAttacked)
			{
				if (!(currentMousePos.X <= Location.X + Width &&
						currentMousePos.X >= Location.X &&
						currentMousePos.Y >= Location.Y &&
						currentMousePos.Y <= Location.Y + Height))
				{
					target.Population = (target.Population + Population) > target.PopulationLimit ?
								target.PopulationLimit :
								(target.Population + Population);
					this.Dispose();
					target.IsAttacked = true;
					target.Invalidate();
				}
			}
		}

		private void Tiger_Tick(object sender, EventArgs e)
		{
			Step();
			tickCount++;
			if (target.IsAttacked)
				this.Dispose(false);
			if (IsArrived(Location))
			{
				TargetAction();
				tiger.Stop();
				this.Parent = null;
				Invalidate();
			}
			if (tickCount % 5 == 1)
			{
				deltax = Destination.X - Location.X;
				deltay = Destination.Y - Location.Y;
				xStep = deltax / (120 - tickCount);
				yStep = deltay / (120 - tickCount);
				Debug.WriteLine("new x {0}, y {1}, xs {2}, xy {3} \n{4}\n{5}",
					deltax, deltay, xStep, yStep, Destination.ToString(), Location.ToString());

			}
		}
	}
}
