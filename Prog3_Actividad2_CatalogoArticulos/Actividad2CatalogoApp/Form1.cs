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

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            try
            {
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;
                dgvArticulos.Columns["Id"].Visible = false;
                cargarImagen(listaArticulo[0].Imagenes[0].Url);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarFiltro()
        {
            cbxCampos.Items.Add("Codigo");
            cbxCampos.Items.Add("Nombre");
            cbxCampos.Items.Add("Descripcion");
            cbxCampos.Items.Add("Marca");
            cbxCampos.Items.Add("Categoria");
            cbxCampos.Items.Add("Precio");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
            cargarFiltro();
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
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;

                if (seleccionado.Imagenes != null && seleccionado.Imagenes.Count > 0)
                    cargarImagen(seleccionado.Imagenes[0].Url);
                else
                    pcbxArticulo.Load("https://icon-library.com/images/no-image-icon/no-image-icon-0.jpg");
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarArticulo alta = new frmAgregarArticulo();
            alta.ShowDialog();
            cargar();
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
                    cargar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void tsmCategorias_Click(object sender, EventArgs e)
        {
            FormCategorias categorias = new FormCategorias();
            categorias.ShowDialog();

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAgregarArticulo modificar = new frmAgregarArticulo(seleccionado);
            modificar.ShowDialog();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmDetalle mostrar = new frmDetalle(seleccionado);
            mostrar.ShowDialog();
        }

        private void cbxCampos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cbxCampos.SelectedItem.ToString();

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            switch (opcion)
            {
                case "Codigo":
                    cbxCriterio.DataSource = null;
                    break;
                case "Nombre":
                    cbxCriterio.DataSource = null;
                    cbxCriterio.DataSource = new List<string>() { "Comienza con", "Termina con", "Contiene" };
                    break;
                case "Descripcion":
                    cbxCriterio.DataSource = null;
                    cbxCriterio.DataSource = new List<string>() { "Comienza con", "Termina con", "Contiene" };
                    break;
                case "Marca":
                    cbxCriterio.DataSource = null;
                    cbxCriterio.DataSource = marcaNegocio.listar();
                    break;
                case "Categoria":
                    cbxCriterio.DataSource = null;
                    cbxCriterio.DataSource = categoriaNegocio.listar();
                    break;
                case "Precio":
                    cbxCriterio.DataSource = null;
                    cbxCriterio.DataSource = new List<string>() { "Mayor a", "Menor a", "Igual a" };
                    break;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                string campo = cbxCampos.SelectedItem.ToString();
                if(campo == "Marca" || campo == "Categoria")
                {
                    txtFiltro.Text = "";
                }
                string criterio = cbxCriterio.SelectedItem.ToString();
                string filtro = txtFiltro.Text;
                dgvArticulos.DataSource = negocio.filtrar(campo, criterio, filtro);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
