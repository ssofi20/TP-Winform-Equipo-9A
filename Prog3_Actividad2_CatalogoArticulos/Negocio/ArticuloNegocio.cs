using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    internal class ArticuloNegocio
    {
        private AccesoDatos datos = new AccesoDatos();

        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();

            try
            {
                datos.setearConsulta("SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.Precio, M.Id, M.Descripcion, C.Id, C.Descripcion, I.ImagenUrl, I.Id FROM ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id AND A.Id = I.IdArticulo");    
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["A.Id"];
                    aux.Codigo = (string)datos.Lector["A.Codigo"];
                    aux.Nombre = (string)datos.Lector["A.Nombre"];
                    aux.Descripcion = (string)datos.Lector["A.Descripcion"];
                    aux.Precio = (decimal)datos.Lector["A.Precio"];

                    aux.Imagenes = new List<Imagen>();
                    Imagen img = new Imagen();
                    img.Id = (int)datos.Lector["I.Id"];
                    img.Url = (string)datos.Lector["I.ImagenUrl"];
                    img.ArticuloId = aux.Id;
                    aux.Imagenes.Add(img);

                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["M.Id"];
                    aux.Marca.Descripcion = (string)datos.Lector["M.Descripcion"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["C.Id"];
                    aux.Categoria.Descripcion = (string)datos.Lector["C.Descripcion"];

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
