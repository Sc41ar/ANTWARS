using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANTWARS
{
	public partial class UpgradeMenuForm : Form
	{

		private Ally caster;
		public UpgradeMenuForm()
		{
			InitializeComponent();
		}

		internal UpgradeMenuForm(Ally ally)
		{
			InitializeComponent();
			caster = ally;
			

		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if ((int)caster.Level == 3)
				Dispose();
			button1.Text = "Улучшить до " + ((int)caster.Level + 1) + " уровня, стоимость: " + ((int)caster.Level + 1) * 20;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if(caster.Money >= (((int)caster.Level + 1) * 20) && (int)caster.Level <= 3)
			{
				caster.Upgrade();
			}

			Dispose();
		}
	}
}
