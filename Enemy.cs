using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace ANTWARS
{
	/// <summary>
	/// абстрактный, чтобы кайф был 
	/// Я просто подумал что иметь дело с каждым противником в своем классе кайфовее и удобнее
	/// Я бы даже сказал ООПешнее
	/// </summary>
	internal abstract class Enemy : NeutralColony
	{
		/// <summary>
		/// нужен таймер, для нападений и еще мб что-то выдумает голова
		/// </summary>
		protected Timer timer = new Timer();
		/// <summary>
		/// ну понятно ведь
		/// </summary>
		protected int tickCount;
		/// <summary>
		/// тоже
		/// </summary>
		protected int timeToUpgrade;//in seconds
		/// <summary>
		/// 0 ссылок, не портим
		/// </summary>
		public Enemy()
		{
			Size = new Size(75, 75);
			Population = 20;
			PopulationGrowthSpeed = 1;
			PopulationLimit = 20;
		}

		public Enemy(int population, int populationLimit, Levels level)
		{
			Level = level;
			Population = population;
			PopulationLimit = populationLimit;
			Size = new Size(75, 75);
			Text = Population + "/" + PopulationLimit;
		}
		/// <summary>
		/// улучшения метод по перегрузке
		/// </summary>
		protected internal virtual void Upgrade()
		{
			int nextLevel = (int)Level + 1;
			Level = (Levels)nextLevel;
			PopulationGrowthSpeed += nextLevel;
			PopulationLimit += nextLevel * 10;
		}
		/// <summary>
		/// почему-то я думаю что это обязательно, хотя по сути будет два вызова базы подраяд
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

		}
		/// <summary>
		/// это можо было и убрать
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}
	}
}
