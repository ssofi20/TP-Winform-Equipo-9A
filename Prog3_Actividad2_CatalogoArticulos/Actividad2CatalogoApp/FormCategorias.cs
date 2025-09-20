using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Actividad2CatalogoApp
{
    public partial class FormCategorias : Form
    {
        List<Categoria> listaCategorias;
        public FormCategorias()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                listaCategorias = negocio.listar();
                dgvCategorias.DataSource = listaCategorias;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormCategorias_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                frmAgregarCategoria agregarCategoria = new frmAgregarCategoria();
                agregarCategoria.ShowDialog();
                cargar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
            frmAgregarCategoria modificar = new frmAgregarCategoria(categoria);
            modificar.ShowDialog();
            cargar();
        }

        public bool asociado(int id)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            List<Articulo> listaArticulos = new List<Articulo>();
            listaArticulos = articuloNegocio.listar();

            foreach (Articulo articulo in listaArticulos)
            {
                if (articulo.Categoria.Id == id)
                {
                    return true;
                }
            }
            return false;
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            CategoriaNegocio negocio = new CategoriaNegocio();

            try
            {
                if (dgvCategorias.CurrentRow != null)
                {
                    categoria = (Categoria)dgvCategorias.CurrentRow.DataBoundItem;
                    if (asociado(categoria.Id))
                    {
                        MessageBox.Show("No se puede eliminar esta categoria porque esta asociada a uno o mas artículos.");
                        return;
                    }

                    DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la categoría " + categoria.Descripcion + "?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (respuesta == DialogResult.Yes)
                    {
                        negocio.eliminar(categoria.Id);
                        cargar();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Categoria> listaFiltrada;
            string filtro = txtFiltro.Text;

            if (filtro.Length >= 2)
                listaFiltrada = listaCategorias.FindAll(x => x.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            else 
                listaFiltrada = listaCategorias;

            dgvCategorias.DataSource = null;
            dgvCategorias.DataSource = listaFiltrada;
        }
    }
}
