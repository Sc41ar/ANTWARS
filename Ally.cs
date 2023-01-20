using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace ANTWARS
{
	internal class Ally : NeutralColony
	{
		/// <summary>
		/// грязные зеленые бумажки
		/// </summary>
		public int Money { get; set; }
		/// <summary>
		/// флажок опущенной кнопки мыши
		/// </summary>
		internal bool _isMouseDown = false;
		/// <summary>
		/// флажок правой кнопки мыщи, для улучшений
		/// </summary>
		internal bool _isRightButton = false;
	/// <summary>
	/// тут даже написано что 0 ссылок, пусть будет 
	/// </summary>
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
		/// <summary>
		/// Что нас интересует для создания колонии : ее расположение, популяция, урове
		/// </summary>
		/// <param name="location"></param>
		/// <param name="population"></param>
		/// <param name="level">
		/// при захвате то всегда первый,
		/// но в планах на какие-то уровни садить уже высокого уровня противников
		/// , мб и союзников)
		/// </param>

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
			//не уверен, что это форматирование влияет
			_format.Alignment = StringAlignment.Center;
			_format.LineAlignment = StringAlignment.Center;
		}
		/// <summary>
		/// событие, точнее его перегрузка
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLevelChanged(LevelEventArgs e)
		{ 
			//база.
			base.OnLevelChanged(e);
			//изменение характеристик происходит в другом методе,
			//здесь же меняем спрайт таким хитрым способом
			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName +
				@"\Resources\" + Fraction.ToString() + ((int)Level).ToString() + ".png";
			BackgroundImage = Bitmap.FromFile(path); // ну и меняем 
		}
		/// <summary>
		/// улучшение
		/// </summary>
		internal void Upgrade()
		{
			//удобно создать переменную для рассчета коэффициентов
			//(я их беру с потолка)
			int nextLevel = ((int)Level + 1);
			//вроде справедливо
			Money -= nextLevel * 20;
			Level = (Levels)nextLevel; // тут событие как раз таки kicks in
			PopulationGrowthSpeed += nextLevel;
			PopulationLimit += nextLevel * 10;//не ну а че
		}
		/// <summary>
		/// очередная перегрузка метода отрисовки, что тут еще объяснять
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);//база.
			Graphics g = e.Graphics; // для удобства использования графического контекста
			Text = Population.ToString() + "/" + PopulationLimit.ToString(); // это популяция
			var cursorPos = this.PointToClient(Cursor.Position);
			g.SmoothingMode = SmoothingMode.HighQuality;
			g.DrawString(Money.ToString(), Font, new SolidBrush(Color.Crimson),
				Width / 2, 3 * Height / 4, _format);//это деньги
			if (_isMouseEntered)//обработочка обводки, в зависимости от уровня, кстати треугольник кривой
			{
				var pen = new Pen(Color.MediumAquamarine, 2f);
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
			if (_isRightButton) // а это точно надо?
			{
				var form = this.Parent;

			}

		}
		//вызов формочки для апгрейда
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			if (e.Button == MouseButtons.Right)
			{
				UpgradeMenuForm umf = new UpgradeMenuForm(this);
				umf.Show();
				umf.Location = Cursor.Position;
			}
		}
		/// <summary>
		/// НУЖНЫ только чтобы флажки поменять
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseDown(MouseEventArgs e)
		{

			base.OnMouseDown(e);
			_isMouseDown = true;
			Invalidate();
		}
		/// <summary>
		/// создается юнит и происходит атака
		/// </summary>
		/// <param name="target"></param>
		private void CreateUnit(NeutralColony target)
		{
			//чтобы он красиво вырывался из центра
			Point centre = new Point(Location.X + Width / 2
							, Location.Y + Height / 2);
			if (target._isAttacked)
				return;
			try
			{
				//выбрасывает иногда исключение disposed object
				//все потому что я так намудрил в том классе,
				//что лучше в него не смотреть 
				_ = new Unit(centre, Population, this, target) //черточка вот эта имба
				{
					Parent = this.Parent
				};
				Population = 0;//обнуляемся
			}
			catch
			{
				//ловим ошибочки
				Debug.WriteLine("!!!!!!!!!!!!!!!");
			}
		}
		/// <summary>
		/// проверяем , где мышка и атакуем
		/// .флажок убираем еще
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			_isMouseDown = false;
			if (e.Button == MouseButtons.Left)
			{
				var form = this.Parent as GameForm;
				var currentMousePos = form.PointToClient(Cursor.Position);
				//не смог ничего придумать изящнее и лучше(
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

						}
					}


				}
			}
			Invalidate();//обновление
		}
	}
}
