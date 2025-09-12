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
        private Articulo articulo = null;
        public frmAgregarArticulo()
        {
            InitializeComponent();
            Text = "Agregar Articulo";
        }

        public frmAgregarArticulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Articulo";
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
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                // Si es alta, creo un artículo nuevo
                if (articulo == null)
                    articulo = new Articulo();

                // Cargo todos los datos en el mismo objeto
                articulo.Codigo = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDescription.Text;
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                // Si ya tiene Id, es modificación
                if (articulo.Id != 0)
                {
                    negocio.modificar(articulo);
                    MessageBox.Show("Modificado exitosamente");
                }
                else // Si no tiene Id, es alta
                {
                    negocio.agregar(articulo);
                    MessageBox.Show("Agregado exitosamente");
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

     
    }
}
