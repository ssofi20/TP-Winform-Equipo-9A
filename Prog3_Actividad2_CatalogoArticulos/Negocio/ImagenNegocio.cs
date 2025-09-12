using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class ImagenNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public void agregar(Imagen nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into IMAGENES (ImagenUrl, IdArticulo) values ('" + nuevo.Url + "','" + nuevo.ArticuloId + "')");
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int ultimoId()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT MAX(Id) FROM ARTICULOS");
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return datos.Lector.GetInt32(0); 
                }else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
            
    }
}
