using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace Actividad2CatalogoApp
{
    public partial class FromMarcas : Form
    {
        private List<Marca> listaMarcas;
        public FromMarcas()
        {
            InitializeComponent();
        }
        private void cargar()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                listaMarcas = marcaNegocio.listar();
                dgvMarcas.DataSource = listaMarcas;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FromMarcas_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAgregarMarca agregarMarca = new frmAgregarMarca();
            agregarMarca.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            Marca marcaSeleccionada;
            try
            {
                if (dgvMarcas.CurrentRow != null)
                {
                    marcaSeleccionada = (Marca)dgvMarcas.CurrentRow.DataBoundItem;
                    DialogResult respuesta = MessageBox.Show("¿Está seguro que desea eliminar la marca " + marcaSeleccionada.Descripcion + "?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (respuesta == DialogResult.Yes)
                    {
                        marcaNegocio.eliminar(marcaSeleccionada.Id);
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
            List<Marca> listaFiltrada;
            string filtro = txtFiltro.Text;

            if(filtro.Length >=2)
                listaFiltrada = listaMarcas.FindAll(x => x.Descripcion.ToUpper().Contains(filtro.ToUpper()));
            else 
                listaFiltrada = listaMarcas;

            dgvMarcas.DataSource = null;
            dgvMarcas.DataSource = listaFiltrada;
        }
    }
}
