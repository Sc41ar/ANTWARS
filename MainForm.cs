using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANTWARS
{
	public partial class MainForm : Form
	{
		public int currentLevel;
		public MainForm()
		{
			InitializeComponent();
		}

		private void GameStart_Click(object sender, EventArgs e)
		{
			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			StreamReader sr = new StreamReader(path + @"\levelRecord.txt");
			int fileLevel = int.Parse(sr.ReadLine());
			if (fileLevel < 1 || fileLevel > 20)
				currentLevel = 1;
			else
				currentLevel = fileLevel;
			sr.Close();
			GameForm gameForm = new GameForm(this, currentLevel);
			gameForm.Show();
			this.Hide();
		}

		internal void LevelRecord()
		{
			string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
			File.Delete(path + @"\levelRecord.txt");
			StreamWriter sw = new StreamWriter(path + @"\levelRecord.txt");
			sw.Write(currentLevel);
			sw.Close();
		}
	}
}
