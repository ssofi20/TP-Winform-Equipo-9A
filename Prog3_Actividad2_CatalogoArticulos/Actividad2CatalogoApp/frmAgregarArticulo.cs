using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using negocio;

namespace Actividad2CatalogoApp
{
    public partial class frmAgregarArticulo : Form
    {
        public frmAgregarArticulo()
        {
            InitializeComponent();
        }

        private void frmAgregarArticulo_Load(object sender, EventArgs e)
        {
            CategoriaNegocio auxCat = new CategoriaNegocio();
            MarcaNegocio auxMarca = new MarcaNegocio();

            try
            {
                cbxCategoria.DataSource = auxCat.listar();
                cbxMarca.DataSource = auxMarca.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Articulo nuevo = new Articulo();
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                nuevo.Codigo = tbxCodigo.Text;
                nuevo.Nombre = tbxNombre.Text;
                nuevo.Descripcion = tbxDescription.Text;
                nuevo.Marca = (Marca)cbxMarca.SelectedItem;
                nuevo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);

                Marca marcaSeleccionada = (Marca)cbxMarca.SelectedItem;
                Categoria categoriaSeleccionada = (Categoria)cbxCategoria.SelectedItem;

                nuevo.Marca.Id = marcaSeleccionada.Id;

                nuevo.Categoria.Id = categoriaSeleccionada.Id;

                negocio.agregar(nuevo);
                MessageBox.Show("Agregado exitosamente");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
    }
}
