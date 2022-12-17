using System;

namespace ANTWARS
{
	public class LevelEventArgs : EventArgs
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
