using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class Ally : NeutralColony
	{
		public int Money { get; set; }
		internal bool _isMouseDown = false;
		internal bool _isArrived = false;
		internal bool _isRightButton = false;
		public Ally()
		{
			Population = 20;
			PopulationGrowthSpeed = 1;
			Location = new Point(10, 10);
			PopulationLimit = 30;
			Money = 0;
			Fraction = Fractions.player;
			Level = Levels.first;
			Size = new Size(150, 100);
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

		public Ally(Point location, int population, Levels level)
		{
			Population = population;
			PopulationGrowthSpeed = 1;
			Location = location;
			PopulationLimit = 30;
			Money = 0;
			Fraction = Fractions.player;
			Level = level;
			Size = new Size(75, 75);
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

		protected override void OnLevelChanged(LevelEventArgs e)
		{
			base.OnLevelChanged(e);

			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
				@"\Resources\" + Fraction.ToString() + ((int)Level).ToString() + ".png";
			BackgroundImage = Bitmap.FromFile(path);
		}

		internal void Upgrade()
		{
			int nextLevel = ((int)Level + 1);
			Money -= nextLevel * 20;
			Level = (Levels)nextLevel;
			PopulationGrowthSpeed += nextLevel;
			PopulationLimit += nextLevel * 10;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			Text = Population.ToString() + "/" + PopulationLimit.ToString();
			var cursorPos = this.PointToClient(Cursor.Position);
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.DrawString(Money.ToString(), Font, new SolidBrush(Color.Crimson),
				Width / 2, 3 * Height / 4, _format);
			if (_isMouseEntered)
			{
				var pen = new Pen(Color.MediumAquamarine, 2f);
				switch ((int)Level)
				{
					case 1:
						{
							Debug.WriteLine("ellipse");
							g.DrawEllipse(pen,
								new Rectangle(0, 0, Width, Height));
						}
						break;
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
			if (_isRightButton)
			{
				var form = this.Parent;

			}

		}

		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (e.Button == MouseButtons.Right)
			{
				UpgradeMenuForm umf = new UpgradeMenuForm(this);
				umf.Show();
				umf.Location = PointToClient(Cursor.Position);
			}
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{

			base.OnMouseDown(e);
			_isMouseDown = true;
			Invalidate();
		}

		private void CreateUnit(NeutralColony target)
		{
			Point centre = new Point(Location.X + Width / 2
							, Location.Y + Height / 2);
			Unit unit = new Unit(centre, Population, this, target)
			{
				Parent = this.Parent
			};
			while (true)
			{
				if (unit.Parent == null)
					_isArrived = true;
				break;
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_isMouseDown = false;
			if (e.Button == MouseButtons.Left)
			{
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
							CreateUnit(target);
							Population = 0;
						}
					}
					else if (control is Ally)
					{
						var target = control as NeutralColony;
						var targetLoc = target.Location;
						if (currentMousePos.X <= targetLoc.X + target.Width &&
							currentMousePos.X >= targetLoc.X &&
							currentMousePos.Y >= targetLoc.Y &&
							currentMousePos.Y <= targetLoc.Y + target.Height &&
							!(currentMousePos.X <= Location.X + Width &&
							currentMousePos.X >= Location.X &&
							currentMousePos.Y >= Location.Y &&
							currentMousePos.Y <= Location.Y + Height))
						{
							CreateUnit(target);
							Population = 0;

						}
					}


				}
			}
			Invalidate();
		}
	}
}
