using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ANTWARS
{
	internal abstract class Enemy : NeutralColony
	{
		protected Timer timer = new Timer();
		protected int tickCount;
		protected int timeToUpgrade;//in seconds
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

		protected internal virtual void Upgrade()
		{
			int nextLevel = (int)Level + 1;
			Level = (Levels)nextLevel;
			PopulationGrowthSpeed += nextLevel;
			PopulationLimit += nextLevel * 10;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

		}

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
