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
        private int indiceImagen = 0;
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
        private void cargarImagen()
        {
            if (listaImagenes != null && listaImagenes.Count > 0 && indiceImagen >= 0 && indiceImagen < articulo.Imagenes.Count)
            {
                try
                {
                    pbxImagenArticulo.Load(listaImagenes[indiceImagen].Url);
                    txtUrlImagen.Text = listaImagenes[indiceImagen].Url; //Agrego la url para poder eliminar si es deseado
                }
                catch
                {
                    pbxImagenArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s");
                    txtUrlImagen.Text = "URL Inválida";
                }
            }
            else
            {
                pbxImagenArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s");
                txtUrlImagen.Clear();
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
                    //MessageBox.Show("Cantidad de imágenes: " + articulo.Imagenes.Count);
                    tbxCodigo.Text = articulo.Codigo;
                    tbxNombre.Text = articulo.Nombre;
                    tbxDescription.Text = articulo.Descripcion;
                    txtPrecio.Text = articulo.Precio.ToString();
                    cbxMarca.SelectedValue = articulo.Marca.Id;
                    cbxCategoria.SelectedValue = articulo.Categoria.Id;
                    listaImagenes = articulo.Imagenes ?? new List<Imagen>();
                    if (listaImagenes.Count > 0)
                    {
                        indiceImagen = 0;
                        cargarImagen();
                    }
                    else
                    {
                        cargarImagen();
                    }
                }
                else
                {
                    articulo = new Articulo();
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
            if(txtUrlImagen.Text != null && txtUrlImagen.Text != "")
            {
                Imagen img = new Imagen();
                img.Url = url;

                listaImagenes.Add(img);

                indiceImagen = listaImagenes.Count - 1; //Mostrar la ultima agregada
                cargarImagen();
                txtUrlImagen.Clear();
                MessageBox.Show("Imagen agregada a la lista. Se agregará al guardar.");
            }
            else
            {
                MessageBox.Show("Ingrese una URL valida");
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

        private void actualizarBotones()
        {
            btnAnterior.Enabled = indiceImagen > 0;
            btnSiguiente.Enabled = indiceImagen < listaImagenes.Count - 1;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (articulo.Imagenes != null && indiceImagen < articulo.Imagenes.Count - 1)
            {
                indiceImagen++;
                cargarImagen();
                actualizarBotones();
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (articulo.Imagenes != null && indiceImagen > 0)
            {
                indiceImagen--;
                cargarImagen();
                actualizarBotones();
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 59) && e.KeyChar != 8)
                e.Handled = true;
        }
    }
}
