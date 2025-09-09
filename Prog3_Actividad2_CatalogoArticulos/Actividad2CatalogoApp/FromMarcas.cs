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

namespace Actividad2CatalogoApp
{
    public partial class FromMarcas : Form
    {
        public FromMarcas()
        {
            InitializeComponent();
        }

        private void FromMarcas_Load(object sender, EventArgs e)
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                dgvMarcas.DataSource = marcaNegocio.listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

        }
    }
}
