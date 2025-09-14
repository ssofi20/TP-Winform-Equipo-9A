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
        private void cargarImagen(string imagen)
        {
            try
            {
                pbxImagenArticulo.Load(imagen);
            }
            catch (Exception)
            {
                pbxImagenArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s");
            }
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
                    txtUrlImagen.Text = articulo.Imagenes[0].Url;
                    cargarImagen(articulo.Imagenes[0].Url);
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
                    negocio.modificar(articulo);
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
            string url = txtUrlImagen.Text;
            if(!string.IsNullOrWhiteSpace(url))
            {
                Imagen img = new Imagen();
                img.Url = url;
                listaImagenes.Add(img);
                cargarImagen(url);
                txtUrlImagen.Clear();
                MessageBox.Show("Imagen agregada");
            }
            else
            {
                MessageBox.Show("Ingrese una URL valida");
            }
        }

        private void txtUrlImagen_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtUrlImagen.Text != null || txtUrlImagen.Text != "")
                    cargarImagen(txtUrlImagen.Text);
                else
                    pbxImagenArticulo.Load("https://icon-library.com/images/no-image-icon/no-image-icon-0.jpg");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnEliminarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                Imagen imagenEliminar = listaImagenes.Find(i => i.Url == txtUrlImagen.Text);
                if(imagenEliminar != null)
                {
                    DialogResult dialogResult = MessageBox.Show("¿Está seguro que desea eliminar la imagen?", "Eliminar Imagen", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                        return;

                    listaImagenes.Remove(imagenEliminar);
                    MessageBox.Show("Imagen eliminada de la lista. Se eliminará al guardar.");
                    txtUrlImagen.Clear();
                }
                else
                {
                    MessageBox.Show("La imagen no se encuentra en la lista.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
