using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class Ally : NeutralColony
	{
		int Money { get; set; }
		int AttackSpeed { get; set; }


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
			ColonySprite = Resource1.player1;
			BackColor = Color.Transparent;
			ForeColor = Color.BlueViolet;
			BackgroundImage = Resource1.player1;
			BackgroundImageLayout = ImageLayout.Stretch;
			_format.Alignment = StringAlignment.Center;
			_format.LineAlignment = StringAlignment.Center;
			//Font
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
			if (_isMouseEntered)
			{
				g.DrawEllipse(new Pen(Color.MediumAquamarine, 2f),
					new Rectangle(0, 0, Width, Height));
				g.DrawString(Text, Font, new SolidBrush(ForeColor),
					Width / 2, Height / 2, _format);
			}
			if (IsAttacked)
			{
				var form = this.Parent as GameForm;
				if (form != null)
				{
					Graphics graphics = Graphics.FromHwnd(form.Handle);
					graphics.DrawLine(new Pen(ForeColor, 2f), 
						Location, this.PointToClient(Cursor.Position));
				}
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			IsAttacked = true;
			Invalidate();
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			IsAttacked = false;
			Invalidate();
		}
	}
}
