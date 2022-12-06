using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class Ally : NeutralColony
	{
		int Money { get; set; }
		int AttackSpeed { get; set; }
		internal bool _isMouseDown = false;

		public Ally()
		{
			Population = 20;
			PopulationGrowthSpeed = 1;
			AttackSpeed = 20;
			Location = new Point(10, 10);
			PopulationLimit = 30;
			Money = 0;
			Fraction = Fractions.player;
			Levels = Levels.first;
			Size = new Size(100, 100);
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			DoubleBuffered = true;
			BackColor = Color.Transparent;
			ForeColor = Color.BlueViolet;
			BackgroundImage = Resource1.player1;
			BackgroundImageLayout = ImageLayout.Stretch;
			_format.Alignment = StringAlignment.Center;
			_format.LineAlignment = StringAlignment.Center;
			//Font
		}

		public Ally(Point location, int population, Levels level)
		{
			Population = population;
			PopulationGrowthSpeed = 1;
			AttackSpeed = 20;
			Location = location;
			PopulationLimit = 30;
			Money = 0;
			Fraction = Fractions.player;
			Levels = level;
			Size = new Size(100, 100);
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			DoubleBuffered = true;
			BackColor = Color.Transparent;
			ForeColor = Color.BlueViolet;
			BackgroundImage = Resource1.player1;
			BackgroundImageLayout = ImageLayout.Stretch;
			_format.Alignment = StringAlignment.Center;
			_format.LineAlignment = StringAlignment.Center;
		}

		void Upgrade()
		{
			Levels = (Levels)((int)Levels + 1);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			Text = Population.ToString() + "/" + PopulationLimit.ToString();
			var cursorPos = this.PointToClient(Cursor.Position);
			g.SmoothingMode = SmoothingMode.HighQuality;

			g.DrawString(Text, Font, new SolidBrush(ForeColor),
				Width / 2, Height / 2, _format);
			if (_isMouseEntered)
			{
				g.DrawEllipse(new Pen(Color.MediumAquamarine, 2f),
					new Rectangle(0, 0, Width, Height));
			}
			//if (_isMouseDown)
			//{
			//	var form = this.Parent as GameForm;
			//	if (form != null)
			//	{
			//		Graphics graphics = Graphics.FromHwnd(form.Handle);
			//		graphics.DrawLines(new Pen(ForeColor, 2f), 
			//			Location, this.PointToClient(Cursor.Position));
			//	}
			//}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			_isMouseDown = true;

			Invalidate();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_isMouseDown = false;
			var form = this.Parent as GameForm;
			var currentMousePos = form.PointToClient(Cursor.Position);
			foreach (Control control in form.Controls)
			{
				if (control is NeutralColony && !(control is Ally))
				{
					var target = control as NeutralColony;
					var targetLoc = target.Location;
					if (currentMousePos.X <= targetLoc.X + target.Width &&
						currentMousePos.X >= targetLoc.X &&
						currentMousePos.Y >= targetLoc.Y &&
						currentMousePos.Y <= targetLoc.Y + target.Height)
					{
						if (Population >= target.Population)
						{

							Ally ally = new Ally(target.Location,
								(Population - target.Population), Levels.first);
							ally.Location = target.Location;
							ally.Parent = this.Parent;
							ally.Invalidate();
							Population = 0;
							form.Colonies.Add(ally);
							form.Controls.Add(ally);
							form.Colonies.Remove(target);
							form.Controls.Remove(target);
							target.Fraction = Fractions.player;
							target.BackgroundImage = Resource1.player1;
							target.Invalidate();
						}
						else
						{
							target.Population -= Population;
							Population = 0;
							Invalidate();
						}
					}
				}
				if (control is Ally)
				{
					var target = control as NeutralColony;
					var targetLoc = target.Location;
					if (currentMousePos.X <= targetLoc.X + target.Width &&
						currentMousePos.X >= targetLoc.X &&
						currentMousePos.Y >= targetLoc.Y &&
						currentMousePos.Y <= targetLoc.Y + target.Height)
					{
						target.Population = (target.Population + Population) > target.PopulationLimit ?
							target.PopulationLimit :
							(target.Population + Population);
						Population = 0;
						Invalidate();
						target.Invalidate();
					}


				}
			}
			Invalidate();
		}
	}
}
