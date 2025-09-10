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
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulo = negocio.listar();
            dgvArticulos.DataSource = listaArticulo;
            cargarImagen(listaArticulo[0].Imagenes[0].Url);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pcbxArticulo.Load(imagen);
            }
            catch (Exception ex)
            {
                pcbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s");
            }

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            if(seleccionado.Imagenes != null && seleccionado.Imagenes.Count > 0)
                cargarImagen(seleccionado.Imagenes[0].Url);
            else 
                pcbxArticulo.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo alta = new frmAgregarArticulo();
            alta.ShowDialog();
        }

        private void tsmMarcas_Click(object sender, EventArgs e)
        {
            FromMarcas marcas = new FromMarcas();
            marcas.ShowDialog();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;

            try
            {
                DialogResult respuesta = MessageBox.Show("¿Seguro que desea eliminar el artículo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta == DialogResult.Yes)
                {
                    seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                    negocio.eliminar(seleccionado.Id);
                    listaArticulo = negocio.listar();
                    dgvArticulos.DataSource = listaArticulo;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
