using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad2CatalogoApp
{
    public partial class frmAgregarCategoria : Form
    {
        public frmAgregarCategoria()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                categoria.Descripcion = txtNombreCategoria.Text;
                negocio.agregar(categoria);
                MessageBox.Show("Categoria agregada exitosamente");
                this.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
