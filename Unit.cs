using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class Unit : Panel
	{
		public int Population { get; set; }
		public Point Destination { get; set; }
		private readonly Timer tiger = new Timer();
		private int deltax;
		private int deltay;
		private int xStep;
		private int yStep;
		private int tickCount;
		private NeutralColony attacker;
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
			tiger.Interval = 20;
			tiger.Tick += Tiger_Tick;
			tiger.Enabled = true;
			tiger.Start();
			Size = new Size(30, 30);
			this.Population = Population;
			this.attacker = attacker;
			this.target = target;
			if (target._isAttacked)
			{
				Debug.WriteLine("У ВАС ЧИЧА");
				Dispose();
			}
			target._isAttacked = true;
		}

		public Unit(Point startLocation, int Population, Enemy attacker, NeutralColony target)
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
			tiger.Interval = 20;
			tiger.Tick += Tiger_Tick;
			tiger.Enabled = true;
			tiger.Start();
			Size = new Size(30, 30);
			this.Population = Population;
			this.attacker = attacker;
			this.target = target;
			if (target._isAttacked)
			{
				Dispose();
			}
			target._isAttacked = true;
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
				//attacker._isArrived = true;
				return true;
			}
			return false;
		}

		void TargetAction()
		{
			var form = this.Parent as GameForm;
			if (form == null)
			{

				Dispose();
				return;
			}
			Point currentMousePos = new Point(target.Location.X + target.Width / 2,
				target.Location.Y + target.Height / 2);
			if (!(target is Ally))
			{
				Debug.WriteLine("target type: " + target.GetType().Name);
				if (Population >= target.Population)
				{

					if (attacker is Ally)
					{
						form.AddAllyColony(target.Location, Population - target.Population, Levels.first);
						Ally ally = (Ally)attacker;
						ally.Money += ((int)target.Level + 1) * 40;
					}
					else
					{
						form.AddOliveEnemy(target.Location, Population - target.Population, Levels.first);
					}
					form.Colonies.Remove(target);
					Debug.WriteLine(form.Colonies.Contains(target));
					form.Controls.Remove(target);
					Debug.WriteLine(form.Controls.Contains(target));
					Debug.WriteLine(target == null);
					target.Dispose();
					target = null;
					GC.Collect();
					GC.WaitForPendingFinalizers();
					Dispose();
				}
				else
				{
					target.Population -= Population;
					target.Invalidate();
					target._isAttacked = false;
					Dispose();
				}
			}
			else if (target is Ally)
			{
				if (!(currentMousePos.X <= Location.X + Width &&
						currentMousePos.X >= Location.X &&
						currentMousePos.Y >= Location.Y &&
						currentMousePos.Y <= Location.Y + Height) && attacker is Ally)
				{
					target.Population = (target.Population + Population) > target.PopulationLimit ?
								target.PopulationLimit :
								(target.Population + Population);

					target.Invalidate();
					this.Dispose();
				}
				else
				{
					if (Population >= target.Population)
					{
						form.AddOliveEnemy(target.Location, Population - target.Population, Levels.first);
						form.Colonies.Remove(target);
						Debug.WriteLine(form.Colonies.Contains(target));
						form.Controls.Remove(target);
						Debug.WriteLine(form.Controls.Contains(target));
						Debug.WriteLine(target == null);
						target.Dispose();
						target = null;
						GC.Collect();
						GC.WaitForPendingFinalizers();
						Dispose();
					}
					else
					{
						target.Population -= Population;
						target.Invalidate();
						target._isAttacked = false;
						Dispose();
					}
				}
			}
		}

		private void Tiger_Tick(object sender, EventArgs e)
		{
			if (target.Parent == null || target == null)
			{
				Dispose();
			}
			Step();
			tickCount++;
			if (IsArrived(Location))
			{
				tiger.Stop();
				TargetAction();

			}
			if (tickCount % 5 == 1)
			{
				deltax = Destination.X - Location.X;
				deltay = Destination.Y - Location.Y;
				xStep = deltax / (120 - tickCount);
				yStep = deltay / (120 - tickCount);

			}
		}
	}
}
