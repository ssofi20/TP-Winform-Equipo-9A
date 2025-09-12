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
        public FormCategorias()
        {
            InitializeComponent();
        }

        private void cargar()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            try
            {
                List<Categoria> listaCategorias = new List<Categoria>();
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
    }
}
