using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, M.Id AS IdMarca, M.Descripcion AS Marca, C.Id AS IdCategoria, C.Descripcion AS Categoria, A.Precio, (SELECT TOP 1 I.ImagenUrl FROM IMAGENES I WHERE I.IdArticulo = A.Id ORDER BY I.Id) AS ImagenPrimera FROM ARTICULOS A LEFT JOIN MARCAS M ON M.Id = A.IdMarca LEFT JOIN CATEGORIAS C ON C.Id = A.IdCategoria");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                    if (!(datos.Lector["ImagenPrimera"] is DBNull))
                    {
                        Imagen img = new Imagen();
                        img.Url = (string)datos.Lector["ImagenPrimera"];
                        aux.Imagenes = new List<Imagen>();
                        aux.Imagenes.Add(img);
                    }

                    lista.Add(aux);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lista;
        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca,IdCategoria, Precio) values ('" + nuevo.Codigo + "', '" + nuevo.Nombre + "','" + nuevo.Descripcion + "', '" + nuevo.Marca.Id + "', '" + nuevo.Categoria.Id + "', '" + nuevo.Precio + "') INSERT into IMAGENES (ImagenUrl, IdArticulo) SELECT '" + nuevo.Imagenes[0].Url + "', A.Id FROM ARTICULOS A WHERE A.Codigo = '"+nuevo.Codigo+"'");
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

        public void eliminar(int id)
        {
            try
            {
                datos.setearConsulta("Delete from ARTICULOS where Id = @id");
                datos.setearParametro("@id", id);

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

    }
}
