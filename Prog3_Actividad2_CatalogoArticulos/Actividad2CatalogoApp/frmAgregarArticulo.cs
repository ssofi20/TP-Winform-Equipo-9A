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
        private List<Imagen> listaImagenes = new List<Imagen>();
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
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";

                cbxMarca.DataSource = auxMarca.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";

                //Si es una modificacion
                if(articulo != null)
                {
                    tbxCodigo.Text = articulo.Codigo;
                    tbxNombre.Text = articulo.Nombre;
                    tbxDescription.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
                    listaImagenes = articulo.Imagenes;
                }
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
                if(articulo == null)
                    articulo = new Articulo();

                articulo.Codigo = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDescription.Text;
                articulo.Marca = (Marca)cbxMarca.SelectedItem;
                articulo.Categoria = (Categoria)cbxCategoria.SelectedItem;
                articulo.Precio = decimal.Parse(txtPrecio.Text);

                Marca marcaSeleccionada = (Marca)cbxMarca.SelectedItem;
                Categoria categoriaSeleccionada = (Categoria)cbxCategoria.SelectedItem;

                articulo.Marca.Id = marcaSeleccionada.Id;
                articulo.Categoria.Id = categoriaSeleccionada.Id;

                if(articulo.Id != 0)
                {
                    //negocio.modificar(articulo);
                    //negocio.eliminarImagenes(articulo.Id);
                    MessageBox.Show("Modificado exitosamente");
                }
                else
                {
                    int idArticulo = negocio.agregar(articulo);
                    foreach (Imagen img in listaImagenes)
                    {
                        img.ArticuloId = idArticulo;
                        negocio.agregarImagen(img);
                    }
                    MessageBox.Show("Agregado exitosamente");
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregarImagen_Click(object sender, EventArgs e)
        {
            frmAgregarImagen agregarImagen = new frmAgregarImagen(listaImagenes);
            
            if(agregarImagen.ShowDialog() == DialogResult.OK)
            {
                listaImagenes = agregarImagen.imagenes;
            }
        }
    }
}
