using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANTWARS
{
	/// <summary>
	/// класс для создания аргумента событий 
	/// </summary>
	internal class LevelEventArgs : EventArgs
	{
		private Levels oldLevel;
		private Levels newLevel;
		public Levels OldLevel { get { return oldLevel; } }
		public Levels NewLevel { get { return newLevel; } }

		public LevelEventArgs(Levels oldLevel, Levels newLevel)
		{
			this.oldLevel = oldLevel;
			this.newLevel = newLevel;
		}
	}
}
