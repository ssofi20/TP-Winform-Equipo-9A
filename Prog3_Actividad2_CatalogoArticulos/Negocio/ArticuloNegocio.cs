using System;
using System.Collections.Generic;
using System.Linq;
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
                datos.setearConsulta("SELECT A.Id IdArticulo, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.Precio, M.Id IdMarca, M.Descripcion NombreMarca, C.Id IdCategoria, C.Descripcion NombreCategoria, I.ImagenUrl, I.Id IdImagen FROM ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id AND A.Id = I.IdArticulo");    
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["IdArticulo"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    aux.Precio = (decimal)datos.Lector["Precio"];

                    aux.Imagenes = new List<Imagen>();
                    Imagen img = new Imagen();
                    img.Id = (int)datos.Lector["IdImagen"];
                    img.Url = (string)datos.Lector["ImagenUrl"];
                    img.ArticuloId = aux.Id;
                    aux.Imagenes.Add(img);

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["NombreMarca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
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
    }
}
