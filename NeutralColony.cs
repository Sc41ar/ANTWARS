using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ANTWARS
{
	delegate void LevelEventHandler(object sender, LevelEventArgs e);//делегат для создания событий смены уровня
	
	/// <summary>
	/// Нейтральная колония, базовая для Ally и Enemy
	/// </summary>
	internal class NeutralColony : Control
	{
		/// <summary>
		/// формат отображения текста на спрайте
		/// </summary>
		protected StringFormat _format = new StringFormat();
		/// <summary>
		/// флаг мыши над объектом
		/// </summary>
		protected bool _isMouseEntered = false;
		public bool _isAttacked = false;
		protected bool _isArrived = false;
		public int Population { get; set; }
		public int PopulationGrowthSpeed { get; set; }
		//поле фракции
		private Fractions _fraction;
		/// <summary>
		/// свойства фракции, необходимо для отрисовки корректного спрайта
		/// </summary>
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
		/// <summary>
		/// обработчик
		/// </summary>
		protected event LevelEventHandler LevelChanged;


		public int PopulationLimit { get; set; }
		/// <summary>
		/// поле уровня
		/// </summary>
		private Levels level;
		/// <summary>
		/// свойство уровня. Необходимо для корректной отрисовки, изменения характеристик
		/// </summary>
		public Levels Level
		{
			get { return level; }
			set
			{
				Levels oldLevel = level;
				level = value;
				if (oldLevel != level)
				{
					OnLevelChanged(new LevelEventArgs(oldLevel, level));
				}
			}
		}

		public NeutralColony()
		{
			Population = 20;
			PopulationGrowthSpeed = 1;
			PopulationLimit = 20;
			_fraction = Fractions.neutral;
			Level = Levels.neutral;
			Size = new Size(100, 100);
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			BackColor = Color.Transparent;
			BackgroundImage = Resource1.neutral1;
			BackgroundImageLayout = ImageLayout.Stretch;
			_format.Alignment = StringAlignment.Center;
			_format.LineAlignment = StringAlignment.Center;

			Text = Population.ToString() + "/" + PopulationLimit.ToString();
		}
		/// <summary>
		/// Конструктор для создания экземпляра объекта нужного в конкретном уровне
		/// </summary>
		/// <param name="location"></param>
		/// <param name="population"></param>
		/// <param name="level"></param>
		public NeutralColony(Point location, int population, Levels level)
		{
			Population = population;
			Location = location;
			PopulationGrowthSpeed = 1;
			PopulationLimit = 20;
			_fraction = Fractions.neutral;
			Level = level;
			Size = new Size(75, 75);
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			BackColor = Color.Transparent;
			BackgroundImage = Resource1.neutral1;
			BackgroundImageLayout = ImageLayout.Stretch;
			_format.Alignment = StringAlignment.Center;
			_format.LineAlignment = StringAlignment.Center;

			Text = Population.ToString() + "/" + PopulationLimit.ToString();
		}
		/// <summary>
		/// Виртуальный метод события
		/// </summary>
		/// <param name="e"></param>
		protected virtual void OnLevelChanged(LevelEventArgs e)
		{
			if (LevelChanged != null) LevelChanged(this, e);
		}
		/// <summary>
		/// виртуальный метод для роста популяции
		/// </summary>
		public virtual void PopulationGrowth()
		{
			if (Population < PopulationLimit)
			{
				Population += PopulationGrowthSpeed;
				if (Population > PopulationLimit)
				{
					Population = PopulationLimit;
				}
			}
			Text = Population.ToString() + "/" + PopulationLimit.ToString();
			Invalidate();
		}
		/// <summary>
		/// перегрузка события отрисовки
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;
			Text = Population.ToString() + "/" + PopulationLimit.ToString();
			g.SmoothingMode = SmoothingMode.HighQuality;

			g.DrawString(Text, Font, new SolidBrush(Color.DarkSlateGray),
				Width / 2, 2 * Height / 5, _format);//отрисовка иформации о колонии

			if (_isMouseEntered && !(this is Ally) && !(this is Enemy)) //обводка
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
		/// <summary>
		/// метод уничтожения,
		/// деструкторов то нет, 
		/// уборщик мусора непонятно когда приедет, он умный очень
		/// а так я себе еще в дебаг напишу, что убил
		/// </summary>

		public new void Dispose()
		{
			this.Parent = null;
			Dispose(true);
			Debug.WriteLine("Disposed " + this.GetType().Name);
			GC.SuppressFinalize(this);

		}
	}
}
