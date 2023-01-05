using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANTWARS
{
	/// <summary>
	/// отдельный уровень для нейтральной колонии потому,
	///  что у нее спрайт отдельный и в ходе игры она свой уровень не повышает
	/// </summary>
	public enum Levels
	{
		neutral,
		first,
		second,
		third,
		fourth
	}
}
