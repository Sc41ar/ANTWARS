using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANTWARS
{
	internal partial class GameForm : Form
	{
		int level;
		/// <summary>
		/// ссылка на основную форму для возвращения к ней
		/// </summary>
		readonly MainForm mainf;
		public int numberOfEnemy;
		public int numberOfAllies;
		/// <summary>
		/// список всех колоний
		/// </summary>
		public List<NeutralColony> Colonies;
		/// <summary>
		/// если не забыл добавить/удалить нужен для дебага
		/// </summary>
		private int ticks = 0;

		public GameForm()
		{
			InitializeComponent();
			this.Paint += GameForm_Paint;
		}

		private void LevelInit(int Level)
		{
			string directory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			using (StreamReader sr = new StreamReader(directory + @"\Lvl" + Level.ToString() + ".txt"))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					var info = line.Split('|');
					switch (info[info.Length - 1])
					{
						case "n":
							{
								numberOfEnemy++;
								Colonies.Add(
									  new NeutralColony(
										  new Point(int.Parse(info[0]), int.Parse(info[1])),
										  int.Parse(info[2]),
									(Levels)int.Parse(info[3])));
							}
							break;
						case "a":
							{
								numberOfAllies++;
								AddAllyColony(
									new Point(int.Parse(info[0]), int.Parse(info[1])),
									int.Parse(info[2]),
									(Levels)int.Parse(info[3]));
							}
							break;
						case "o":
							{
								numberOfEnemy++;
								AddOliveEnemy(
									new Point(int.Parse(info[0]), int.Parse(info[1])),
									int.Parse(info[2]),
									(Levels)int.Parse(info[3]));
							}
							break;
						case "b":
							{
								numberOfEnemy++;
								AddBlueEnemy
										(new Point(int.Parse(info[0]), int.Parse(info[1])),
										int.Parse(info[2]),
										(Levels)int.Parse(info[3]));
							}
							break;
						case "r":
							{
								numberOfEnemy++;
								AddRedEnemy(
									new Point(int.Parse(info[0]), int.Parse(info[1])),
									int.Parse(info[2]),
									(Levels)int.Parse(info[3]));
							}
							break;
						case "i":
							{
								numberOfEnemy++;
								AddIndigoEnemy(
									new Point(int.Parse(info[0]), int.Parse(info[1])),
									int.Parse(info[2]),
									(Levels)int.Parse(info[3]));
							}
							break;
					}
				}
			}
		}

		/// <summary>
		/// задаем список колоний
		/// </summary>
		/// <param name="mf"></param>
		public GameForm(MainForm mf, int Level)
		{
			InitializeComponent();
			mainf = mf;
			Colonies = new List<NeutralColony>();
			level = Level;
			LevelInit(Level);

		}
		/// <summary>
		/// мне казалось что это удобное создание колоний
		/// </summary>
		/// <param name="location"></param>
		/// <param name="population"></param>
		/// <param name="level"></param>
		public void AddAllyColony(Point location, int population, Levels level)
		{

			Colonies.Add(new Ally(location, population, Levels.first));
			Update();
		}

		public void AddOliveEnemy(Point location, int population, Levels level)
		{
			Colonies.Add(new OliveEnemy(location, population, Levels.first));
			Update();
		}

		public void AddBlueEnemy(Point location, int population, Levels level)
		{
			Colonies.Add(new BlueEnemy(location, population, Levels.first));
			Update();
		}

		public void AddRedEnemy(Point location, int population, Levels level)
		{
			Colonies.Add(new RedEnemy(location, population, Levels.first));
			Update();
		}

		public void AddIndigoEnemy(Point location, int population, Levels level)
		{
			Colonies.Add(new IndigoEnemy(location, population, Levels.first));
			Update();
		}

		/// <summary>
		/// как онпайнт
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		internal void GameForm_Paint(object sender, PaintEventArgs e)
		{
			//мерцает без этого, и вообще кайф имеется
			SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor
				| ControlStyles.UserPaint, true);
			foreach (var item in Colonies)
			{
				//из одного списочка в другой рисуем
				Controls.Add(item);
				item.Parent = this;
				item.Show();
				item.Update();
			}
		}
		/// <summary>
		/// необходимо для корректного отображения проигрыша/победы
		/// </summary>
		internal void ColonyCount()
		{
			int allies=0;
			int enemies=0;
			foreach (var a  in Colonies)
			{
				if(a is Ally)
					allies++;
				else
					enemies++;
			}
			numberOfAllies = allies;
			numberOfEnemy = enemies;
			ResultCheck();
		}
		 /// <summary>
		 /// проверка на победу или проигрыш
		 /// </summary>
		internal void ResultCheck()
		{
			Debug.WriteLine(numberOfEnemy);
			Debug.WriteLine(numberOfAllies);
			if (numberOfAllies < 1)
			{
				mainf.Show();
				ResultForm a = new ResultForm("Lose", this, mainf);
				a.Show();
				a.Location = Cursor.Position;
				timer1.Stop();
			}
			if (numberOfEnemy < 1)
			{
				mainf.Show();
				ResultForm a = new ResultForm("victory", this, mainf);
				a.Show();
				a.Location = Cursor.Position;
				timer1.Stop();
				mainf.currentLevel = level + 1;
				mainf.LevelRecord();

			}
			return;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			//я здесь делал секундомер, может еще верну)
			Graphics g = e.Graphics;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			//магичество LINQ
			//понравился мне
			//метод увеличивает население малышей
			var Allies =
				from t in Colonies
				where t is Ally
				select t;
			var enemies = from t in Colonies
							  where t is Enemy
							  select t;
			foreach (var enemy in enemies)
			{
				enemy.PopulationGrowth();
			}
			foreach (var item in Allies)
			{
				item.PopulationGrowth();
			}
			ticks++;
			ColonyCount();
		}
	}
}
