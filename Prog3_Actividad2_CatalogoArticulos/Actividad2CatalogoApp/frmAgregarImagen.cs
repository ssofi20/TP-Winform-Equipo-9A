using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;

namespace Actividad2CatalogoApp
{
    public partial class frmAgregarImagen : Form
    {
        public List<Imagen> imagenes;
        public frmAgregarImagen()
        {
            InitializeComponent();
        }
        
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUrlImagen.Text))
            {
                Imagen nuevaImagen = new Imagen();
                nuevaImagen.Url = txtUrlImagen.Text;
                imagenes.Add(nuevaImagen);
                MessageBox.Show("Imagen agregada: " + txtUrlImagen.Text);
            }
            else
            {
                MessageBox.Show("Por favor, ingrese una URL válida.");
                txtUrlImagen.Clear();
            }
        }

        private void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pbxImagen.Load(txtUrlImagen.Text);
            }
            catch (Exception ex)
            {
                pbxImagen.Load("https://icon-library.com/images/no-image-icon/no-image-icon-0.jpg");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
