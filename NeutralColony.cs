using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ANTWARS
{
	public abstract class NeutralColony : Control
	{
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

		public Point Position { get; set; }

		public int PopulationLimit { get; set; }
		public Levels Levels { get; set; }

		NeutralColony()
		{
			Population = 20;
			PopulationGrowthSpeed = 1;
			PopulationLimit = 20;
			IsAttacked = false;
			_fraction = Fractions.neutral;
			Position = new Point(0, 0);
			Levels = Levels.first;
		}

		public void PopulationGrowth()
		{
			Population += PopulationGrowthSpeed;
		}


	}
}
