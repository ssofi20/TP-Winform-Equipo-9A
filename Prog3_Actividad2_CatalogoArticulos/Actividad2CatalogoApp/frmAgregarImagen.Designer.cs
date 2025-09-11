namespace Actividad2CatalogoApp
{
    partial class frmAgregarImagen
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblUrlAddImg = new System.Windows.Forms.Label();
            this.txtUrlAddImg = new System.Windows.Forms.TextBox();
            this.pcbxOtraImg = new System.Windows.Forms.PictureBox();
            this.btnAgregarImg = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnAgregarOtro = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pcbxOtraImg)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUrlAddImg
            // 
            this.lblUrlAddImg.AutoSize = true;
            this.lblUrlAddImg.Location = new System.Drawing.Point(84, 41);
            this.lblUrlAddImg.Name = "lblUrlAddImg";
            this.lblUrlAddImg.Size = new System.Drawing.Size(88, 16);
            this.lblUrlAddImg.TabIndex = 0;
            this.lblUrlAddImg.Text = "URL imagen: ";
            // 
            // txtUrlAddImg
            // 
            this.txtUrlAddImg.Location = new System.Drawing.Point(181, 38);
            this.txtUrlAddImg.Name = "txtUrlAddImg";
            this.txtUrlAddImg.Size = new System.Drawing.Size(184, 22);
            this.txtUrlAddImg.TabIndex = 1;
            this.txtUrlAddImg.Leave += new System.EventHandler(this.txtUrlAddImg_Leave);
            // 
            // pcbxOtraImg
            // 
            this.pcbxOtraImg.Location = new System.Drawing.Point(114, 82);
            this.pcbxOtraImg.Name = "pcbxOtraImg";
            this.pcbxOtraImg.Size = new System.Drawing.Size(251, 251);
            this.pcbxOtraImg.TabIndex = 2;
            this.pcbxOtraImg.TabStop = false;
            // 
            // btnAgregarImg
            // 
            this.btnAgregarImg.Location = new System.Drawing.Point(28, 365);
            this.btnAgregarImg.Name = "btnAgregarImg";
            this.btnAgregarImg.Size = new System.Drawing.Size(84, 35);
            this.btnAgregarImg.TabIndex = 3;
            this.btnAgregarImg.Text = "Aceptar";
            this.btnAgregarImg.UseVisualStyleBackColor = true;
            this.btnAgregarImg.Click += new System.EventHandler(this.btnAgregarImg_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(127, 365);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 35);
            this.btnCancelar.TabIndex = 4;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(241, 365);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(92, 35);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnAgregarOtro
            // 
            this.btnAgregarOtro.Location = new System.Drawing.Point(350, 365);
            this.btnAgregarOtro.Name = "btnAgregarOtro";
            this.btnAgregarOtro.Size = new System.Drawing.Size(114, 35);
            this.btnAgregarOtro.TabIndex = 6;
            this.btnAgregarOtro.Text = "Agregar otro";
            this.btnAgregarOtro.UseVisualStyleBackColor = true;
            // 
            // frmAgregarImagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(485, 477);
            this.Controls.Add(this.btnAgregarOtro);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAgregarImg);
            this.Controls.Add(this.pcbxOtraImg);
            this.Controls.Add(this.txtUrlAddImg);
            this.Controls.Add(this.lblUrlAddImg);
            this.Name = "frmAgregarImagen";
            this.Text = "frmAgregarImagen";
            ((System.ComponentModel.ISupportInitialize)(this.pcbxOtraImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUrlAddImg;
        private System.Windows.Forms.TextBox txtUrlAddImg;
        private System.Windows.Forms.PictureBox pcbxOtraImg;
        private System.Windows.Forms.Button btnAgregarImg;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Button btnAgregarOtro;
    }
}