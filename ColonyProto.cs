using System.Drawing;
using System.Xml.Serialization;

namespace ANTWARS
{
	abstract class ColonyProto
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

		ColonyProto()
		{
			Population = 20;
			PopulationGrowthSpeed = 1;
			PopulationLimit = 20;
			IsAttacked = false;
			_fraction = (Fractions)1;
			Position = new Point(0, 0);
			Levels = Levels.first;
		}

		public void PopulationGrowth()
		{
			Population += PopulationGrowthSpeed;
		}


	}
}
