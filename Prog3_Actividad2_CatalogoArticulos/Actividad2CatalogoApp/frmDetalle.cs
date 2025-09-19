using dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Actividad2CatalogoApp
{
    public partial class frmDetalle : Form
    {
        private Articulo articulo = null;
        private List<Imagen> listaImag = new List<Imagen>();
        private int indiceActual = 0;

        public frmDetalle()
        {
            InitializeComponent();
            Text = "Ver detalle";
        }

        public frmDetalle(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Ver detalle";
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            if (articulo != null)
            {
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();
                txtMarca.Text = articulo.Marca.ToString();
                txtCategoria.Text = articulo.Categoria.ToString();

                listaImag = articulo.Imagenes;
                if (listaImag != null)
                {
                    txtUrl.Text = articulo.Imagenes[indiceActual].Url;
                    cargarImagen();
                }
            }
        }

        private void cargarImagen()
        {
            if (listaImag != null && listaImag.Count > 0)
            {
                try
                {
                    pcbxDetalle.Load(listaImag[indiceActual].Url);
                    txtUrl.Text = listaImag[indiceActual].Url;
                }
                catch
                {
                    pcbxDetalle.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s");
                    txtUrl.Text = "URL Invalída";
                }
            }
            else
            {
                pcbxDetalle.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s");
                txtUrl.Clear();
            }
        }
        private void actualizarBotones()
        {
            btnAnterior.Enabled = indiceActual > 0;
            btnSiguiente.Enabled = indiceActual < listaImag.Count - 1;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (listaImag != null && listaImag.Count > 0 && indiceActual < listaImag.Count - 1)
            {
                indiceActual++;
                cargarImagen();
                actualizarBotones();
            }
        }
            

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (listaImag != null && listaImag.Count > 0 && indiceActual > 0)
            {
                indiceActual--;
                cargarImagen();
                actualizarBotones();
            }
        }
    }


}
