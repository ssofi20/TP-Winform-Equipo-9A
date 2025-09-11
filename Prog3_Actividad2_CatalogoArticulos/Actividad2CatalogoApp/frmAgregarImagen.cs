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
using negocio; 

namespace Actividad2CatalogoApp
{
    public partial class frmAgregarImagen : Form
    {

        public frmAgregarImagen()
        {
            InitializeComponent();
        }

        private void txtUrlAddImg_Leave(object sender, EventArgs e)
        {
            cargarImg(txtUrlAddImg.Text);
        }

        private void cargarImg(string imagen)
        {
            try
            {
                pcbxOtraImg.Load(imagen);
            }
            catch (Exception ex)
            {
                pcbxOtraImg.Load("https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ432ju-gdS2nl6CEobTaFXEe6_gRmK5DkWuQ&s");
            }
        }

        private void btnAgregarImg_Click(object sender, EventArgs e)
        {
         
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
