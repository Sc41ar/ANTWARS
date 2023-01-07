using System;
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
		/// <summary>
		/// ссылка на основную форму для возвращения к ней
		/// </summary>
		readonly MainForm mainf;
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

		private void GameForm_Paint1(object sender, PaintEventArgs e)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// задаем прикольчик, массив малышей
		/// </summary>
		/// <param name="mf"></param>
		public GameForm(MainForm mf)
		{
			InitializeComponent();
			mainf = mf;
			Colonies = new List<NeutralColony>();
			Colonies.Add(new Ally(new Point(75, 100), 20, Levels.first));
			Colonies.Add(new NeutralColony(new Point(225, 300), 10, Levels.neutral));
			Colonies.Add(new NeutralColony(new Point(75, 540), 10, Levels.neutral));
			Colonies.Add(new NeutralColony(new Point(125, 200), 10, Levels.neutral));
			Colonies.Add(new NeutralColony(new Point(225, 100), 10, Levels.neutral));
			Colonies.Add(new OliveEnemy(new Point(331, 100), 14, Levels.first));
			//Colonies.Add(new OliveEnemy(new Point(331, 350), 14, Levels.first));
			//Colonies.Add(new OliveEnemy(new Point(331, 200), 14, Levels.first));
			//Colonies.Add(new NeutralColony(new Point(456, 100), 10, Levels.neutral));
			//Colonies.Add(new NeutralColony(new Point(581, 100), 10, Levels.neutral));
			//Colonies.Add(new NeutralColony(new Point(706, 100), 10, Levels.neutral));

			Colonies.Add(new BlueEnemy(new Point(456, 100), 14, Levels.first));
			Colonies.Add(new RedEnemy(new Point(581, 100), 20, Levels.first));
			Colonies.Add(new IndigoEnemy(new Point(706, 100), 25, Levels.first));

		}
		/// <summary>
		/// мне казалось что это удобно, те перь не уверен
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

		/// <summary>
		/// думал будет вызывать, когда закрывается, а он вызывает даже, если свернуть
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GameForm_Deactivate(object sender, EventArgs e)
		{
			//mainf.Show();
			//mainf.Close();
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
		}
	}
}
