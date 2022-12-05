using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ANTWARS
{
	public class NeutralColony : Control
	{
		protected StringFormat _format = new StringFormat();
		protected bool _isMouseEntered = false;
		public int Population { get; set; }
		public int PopulationGrowthSpeed { get; set; }

		private Fractions _fraction;
		public Fractions Fraction
		{
			get
			{
				return _fraction;
			}
			set
			{
				_fraction = value;
			}
		}

		public bool IsAttacked { get; set; }


		public int PopulationLimit { get; set; }
		public Levels Levels { get; set; }

		public NeutralColony()
		{
			Population = 20;
			PopulationGrowthSpeed = 1;
			PopulationLimit = 20;
			IsAttacked = false;
			_fraction = Fractions.neutral;
			Levels = Levels.first;
			Size = new Size(100, 100);
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			BackColor = Color.Transparent;
			BackgroundImage = Resource1.Neutral;
			BackgroundImageLayout = ImageLayout.Stretch;
			_format.Alignment = StringAlignment.Center;
			_format.LineAlignment = StringAlignment.Center;

			Text = Population.ToString() + "/" + PopulationLimit.ToString();
		}

		public NeutralColony(Point location, int population, Levels level)
		{
			Population = population;
			Location = location;
			PopulationGrowthSpeed = 1;
			PopulationLimit = 20;
			IsAttacked = false;
			_fraction = Fractions.neutral;
			Levels = level;
			Size = new Size(100, 100);
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			BackColor = Color.Transparent;
			BackgroundImage = Resource1.blueEnemy;
			BackgroundImageLayout = ImageLayout.Stretch;
			_format.Alignment = StringAlignment.Center;
			_format.LineAlignment = StringAlignment.Center;

			Text = Population.ToString() + "/" + PopulationLimit.ToString();
		}
		public void PopulationGrowth()
		{
			if (Population < PopulationLimit)
				Population += PopulationGrowthSpeed;
			Text = Population.ToString() + "/" + PopulationLimit.ToString();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			Text = Population.ToString() + "/" + PopulationLimit.ToString();
			g.SmoothingMode = SmoothingMode.HighQuality;

			g.DrawString(Text, Font, new SolidBrush(Color.Crimson),
				Width / 2, Height / 2, _format);
			if (_isMouseEntered)
			{
				g.DrawEllipse(new Pen(Color.MediumAquamarine, 2f),
					new Rectangle(0, 0, Width, Height));
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			_isMouseEntered = true;
			Invalidate();
			
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			_isMouseEntered = false;
			Invalidate();
		}

		protected override void OnMouseHover(EventArgs e)
		{
			base.OnMouseHover(e);
			
		}
	}
}
