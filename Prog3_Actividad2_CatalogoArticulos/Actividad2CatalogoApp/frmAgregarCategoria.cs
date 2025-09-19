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
using negocio;

namespace Actividad2CatalogoApp
{
    public partial class frmAgregarCategoria : Form
    {
        private Categoria categoria = null;
        public frmAgregarCategoria()
        {
            InitializeComponent();
            Text = "Agregar Categoria";
        }

        public frmAgregarCategoria(Categoria categoria)
        {
            InitializeComponent();
            this.categoria = categoria;
            Text = "Modificar Categoria";
        }

        private void frmAgregarCategoria_Load(object sender, EventArgs e)
        {
            if (categoria != null)
            {
                txtNombreCategoria.Text = categoria.Descripcion;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                if(categoria == null)
                    categoria = new Categoria();

                categoria.Descripcion = txtNombreCategoria.Text;

                if(categoria.Id != 0)
                {
                    if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                    {
                        MessageBox.Show("Ingresar el nuevo nombre para continuar.");
                        return;
                    }
                    else
                    {
                        negocio.modificar(categoria);
                        MessageBox.Show("Categoria modificada exitosamente");
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                    {
                        MessageBox.Show("Ingrese un nombre para continuar.");
                        return;
                    }
                    else
                    {
                    negocio.agregar(categoria);
                    MessageBox.Show("Categoria agregada exitosamente");
                    }
                }

                this.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
