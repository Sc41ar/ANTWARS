namespace ANTWARS
{
	partial class GameForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
		private Ally ally1;
		private NeutralColony neutralColony1;
		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameForm));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.neutralColony1 = new ANTWARS.NeutralColony();
			this.ally1 = new ANTWARS.Ally();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 20;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// neutralColony1
			// 
			this.neutralColony1.BackColor = System.Drawing.Color.Transparent;
			this.neutralColony1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("neutralColony1.BackgroundImage")));
			this.neutralColony1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.neutralColony1.ColonySprite = ((System.Drawing.Bitmap)(resources.GetObject("neutralColony1.ColonySprite")));
			this.neutralColony1.Fraction = ANTWARS.Fractions.neutral;
			this.neutralColony1.IsAttacked = false;
			this.neutralColony1.Levels = ANTWARS.Levels.first;
			this.neutralColony1.Location = new System.Drawing.Point(500, 209);
			this.neutralColony1.Name = "neutralColony1";
			this.neutralColony1.Population = 20;
			this.neutralColony1.PopulationGrowthSpeed = 1;
			this.neutralColony1.PopulationLimit = 20;
			this.neutralColony1.Size = new System.Drawing.Size(100, 100);
			this.neutralColony1.TabIndex = 1;
			this.neutralColony1.Text = "20/20";
			// 
			// ally1
			// 
			this.ally1.BackColor = System.Drawing.Color.Transparent;
			this.ally1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ally1.BackgroundImage")));
			this.ally1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ally1.ColonySprite = ((System.Drawing.Bitmap)(resources.GetObject("ally1.ColonySprite")));
			this.ally1.ForeColor = System.Drawing.Color.BlueViolet;
			this.ally1.Fraction = ANTWARS.Fractions.player;
			this.ally1.IsAttacked = false;
			this.ally1.Levels = ANTWARS.Levels.first;
			this.ally1.Location = new System.Drawing.Point(256, 111);
			this.ally1.Name = "ally1";
			this.ally1.Population = 20;
			this.ally1.PopulationGrowthSpeed = 1;
			this.ally1.PopulationLimit = 30;
			this.ally1.Size = new System.Drawing.Size(100, 100);
			this.ally1.TabIndex = 0;
			this.ally1.Text = "20/30";
			this.ally1.DragLeave += new System.EventHandler(this.ally1_DragLeave);
			// 
			// GameForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.BackgroundImage = global::ANTWARS.Resource1.hmADm_mDdpTr3SYiT6a4YYSZUKa_wfeJ_UukU_55StHj3RthXKAQVX3ujTV5s3qOhS3n8pkFzeAa6WLPtphhERIh;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.neutralColony1);
			this.Controls.Add(this.ally1);
			this.ForeColor = System.Drawing.Color.Red;
			this.Name = "GameForm";
			this.Text = "GameForm";
			this.Deactivate += new System.EventHandler(this.GameForm_Deactivate);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameForm_Paint);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timer1;

	}
}