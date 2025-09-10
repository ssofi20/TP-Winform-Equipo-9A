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
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.Precio, M.Id MarcaId, M.Descripcion NombreMarca, C.Id CategoriaId, C.Descripcion NombreCategoria, I.Id ImagenId, I.ImagenUrl\r\nFROM ARTICULOS A \r\nINNER JOIN MARCAS M ON A.IdMarca = M.Id\r\nINNER JOIN CATEGORIAS C ON A.IdCategoria = C.Id\r\nLEFT JOIN IMAGENES I ON A.Id = I.IdArticulo;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                    {
                        aux.Imagenes = new List<Imagen>();
                        Imagen img = new Imagen();
                        img.Id = (int)datos.Lector["ImagenId"];
                        img.Url = (string)datos.Lector["ImagenUrl"];
                        img.ArticuloId = aux.Id;
                        aux.Imagenes.Add(img);
                    }

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["MarcaId"];
                    aux.Marca.Descripcion = (string)datos.Lector["NombreMarca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["CategoriaId"];
                    aux.Categoria.Descripcion = (string)datos.Lector["NombreCategoria"];

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
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca,IdCategoria, Precio) values ('" + nuevo.Codigo + "', '" + nuevo.Nombre + "','" + nuevo.Descripcion + "', '" + nuevo.Marca.Id + "', '" + nuevo.Categoria.Id + "', '"+ nuevo.Precio+"')");
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
