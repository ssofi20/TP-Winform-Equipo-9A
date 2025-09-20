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
    public partial class frmAgregarMarca : Form
    {
        Marca marca = null;
        public frmAgregarMarca()
        {
            InitializeComponent();
            Text = "Agregar Marca";
        }

        public frmAgregarMarca(Marca marca)
        {
            InitializeComponent();
            this.marca = marca;
            Text = "Modificar Marca";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool existe(string nombre)
        {

            List<Marca> listaMarcas = new List<Marca>();
            MarcaNegocio negocio = new MarcaNegocio();

            listaMarcas = negocio.listar();

            foreach (Marca marca in listaMarcas)
            {
                if (marca.Descripcion.ToLower() == nombre.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                if(marca == null)
                    marca = new Marca();

                marca.Descripcion = txtNombreMarca.Text;
                if (existe(marca.Descripcion))
                {
                    MessageBox.Show("El nombre indicado ya existe. Por favor elegir un nombre distinto.");
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNombreMarca.Text))
                {
                    MessageBox.Show("Escribir el nombre para poder continuar.");
                    return;
                }

                if (marca.Id != 0)
                {
                    negocio.modificar(marca);
                    MessageBox.Show("Marca modificada exitosamente");
                }
                else
                {
                    negocio.agregar(marca);
                    MessageBox.Show("Marca agregada exitosamente");
                }

                this.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
