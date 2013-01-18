/*
 * Creado por SharpDevelop.
 * Usuario: Usuario
 * Fecha: 13/03/2010
 * Hora: 07:02 p.m.
 * 
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
namespace Formulacion_del_nectar
{
	partial class FrmAcercaDe
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAcercaDe));
			this.btnAceptar = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lnkAcercaDe = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnAceptar
			// 
			this.btnAceptar.Location = new System.Drawing.Point(100, 141);
			this.btnAceptar.Name = "btnAceptar";
			this.btnAceptar.Size = new System.Drawing.Size(90, 22);
			this.btnAceptar.TabIndex = 1;
			this.btnAceptar.Text = "Aceptar";
			this.btnAceptar.UseVisualStyleBackColor = true;
			this.btnAceptar.Click += new System.EventHandler(this.BtnAceptarClick);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Location = new System.Drawing.Point(5, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(70, 70);
			this.pictureBox1.TabIndex = 2;
			this.pictureBox1.TabStop = false;
			// 
			// lnkAcercaDe
			// 
			this.lnkAcercaDe.Location = new System.Drawing.Point(78, 9);
			this.lnkAcercaDe.Name = "lnkAcercaDe";
			this.lnkAcercaDe.Size = new System.Drawing.Size(217, 129);
			this.lnkAcercaDe.TabIndex = 3;
			this.lnkAcercaDe.TabStop = true;
			this.lnkAcercaDe.Text = resources.GetString("lnkAcercaDe.Text");
			this.lnkAcercaDe.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkAcercaDeLinkClicked);
			// 
			// FrmAcercaDe
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(298, 175);
			this.Controls.Add(this.lnkAcercaDe);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.btnAceptar);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmAcercaDe";
			this.Text = "Acerca De Formulacion del nectar";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.LinkLabel lnkAcercaDe;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Button btnAceptar;
		
		void BtnAceptarClick(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
