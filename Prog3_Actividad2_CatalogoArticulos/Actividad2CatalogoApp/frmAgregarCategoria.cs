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

        private bool existe(string nombre)
        {

            List<Categoria> listaCategorias = new List<Categoria>();
            CategoriaNegocio negocio = new CategoriaNegocio();

            listaCategorias = negocio.listar();

            foreach (Categoria categoria in listaCategorias)
            {
                if (categoria.Descripcion.ToLower() == nombre.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                if(categoria == null)
                    categoria = new Categoria();

                categoria.Descripcion = txtNombreCategoria.Text;

                if(existe(categoria.Descripcion))
                {
                    MessageBox.Show("El nombre indicado ya existe. Por favor elegir un nombre distinto.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNombreCategoria.Text))
                {
                    MessageBox.Show("Escribir el nombre para poder continuar.");
                    return;
                }

                if (categoria.Id != 0)
                {
                    negocio.modificar(categoria);
                    MessageBox.Show("Categoria modificada exitosamente");
                }
                else
                {
                    negocio.agregar(categoria);
                    MessageBox.Show("Categoria agregada exitosamente");
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
