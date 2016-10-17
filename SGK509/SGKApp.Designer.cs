/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 17.10.2016
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SGK509
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btnInstall;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.TabControl tabSGK;
		private System.Windows.Forms.TabPage tabData;
		private System.Windows.Forms.TabPage tabDB;
		private System.Windows.Forms.ComboBox cbProtocol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabSerialPort;
	
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnInstall = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.tabSGK = new System.Windows.Forms.TabControl();
			this.tabData = new System.Windows.Forms.TabPage();
			this.tabSerialPort = new System.Windows.Forms.TabPage();
			this.tabDB = new System.Windows.Forms.TabPage();
			this.cbProtocol = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabSGK.SuspendLayout();
			this.tabSerialPort.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnInstall
			// 
			this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInstall.Location = new System.Drawing.Point(617, 458);
			this.btnInstall.Name = "btnInstall";
			this.btnInstall.Size = new System.Drawing.Size(75, 37);
			this.btnInstall.TabIndex = 0;
			this.btnInstall.Text = "Установить Службу";
			this.btnInstall.UseVisualStyleBackColor = true;
			this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(717, 458);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 37);
			this.btnDelete.TabIndex = 0;
			this.btnDelete.Text = "Удалить Службу";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.Location = new System.Drawing.Point(617, 501);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 35);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Запустить Службы";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStop.Location = new System.Drawing.Point(717, 501);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 35);
			this.btnStop.TabIndex = 0;
			this.btnStop.Text = "Остановить Службу";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// tabSGK
			// 
			this.tabSGK.Controls.Add(this.tabData);
			this.tabSGK.Controls.Add(this.tabSerialPort);
			this.tabSGK.Controls.Add(this.tabDB);
			this.tabSGK.Location = new System.Drawing.Point(0, 1);
			this.tabSGK.Name = "tabSGK";
			this.tabSGK.SelectedIndex = 0;
			this.tabSGK.Size = new System.Drawing.Size(808, 545);
			this.tabSGK.TabIndex = 1;
			// 
			// tabData
			// 
			this.tabData.Location = new System.Drawing.Point(4, 22);
			this.tabData.Name = "tabData";
			this.tabData.Padding = new System.Windows.Forms.Padding(3);
			this.tabData.Size = new System.Drawing.Size(800, 519);
			this.tabData.TabIndex = 1;
			this.tabData.Text = "Данные";
			this.tabData.UseVisualStyleBackColor = true;
			// 
			// tabSerialPort
			// 
			this.tabSerialPort.Controls.Add(this.cbProtocol);
			this.tabSerialPort.Controls.Add(this.label1);
			this.tabSerialPort.Location = new System.Drawing.Point(4, 22);
			this.tabSerialPort.Name = "tabSerialPort";
			this.tabSerialPort.Padding = new System.Windows.Forms.Padding(3);
			this.tabSerialPort.Size = new System.Drawing.Size(800, 519);
			this.tabSerialPort.TabIndex = 0;
			this.tabSerialPort.Text = "COM-порт";
			this.tabSerialPort.UseVisualStyleBackColor = true;
			// 
			// tabDB
			// 
			this.tabDB.Location = new System.Drawing.Point(4, 22);
			this.tabDB.Name = "tabDB";
			this.tabDB.Padding = new System.Windows.Forms.Padding(3);
			this.tabDB.Size = new System.Drawing.Size(800, 519);
			this.tabDB.TabIndex = 2;
			this.tabDB.Text = "База Данных";
			this.tabDB.UseVisualStyleBackColor = true;
			// 
			// cbProtocol
			// 
			this.cbProtocol.FormattingEnabled = true;
			this.cbProtocol.Location = new System.Drawing.Point(103, 6);
			this.cbProtocol.Name = "cbProtocol";
			this.cbProtocol.Size = new System.Drawing.Size(121, 21);
			this.cbProtocol.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label1.Location = new System.Drawing.Point(8, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Протокол";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 544);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnInstall);
			this.Controls.Add(this.tabSGK);
			this.Name = "MainForm";
			this.Text = "SGK509";
			this.tabSGK.ResumeLayout(false);
			this.tabSerialPort.ResumeLayout(false);
			this.ResumeLayout(false);

			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 544);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnInstall);
			this.Controls.Add(this.tabSGK);
			this.Name = "MainForm";
			this.Text = "SGK509";
			this.tabSGK.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
