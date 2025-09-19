﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using dominio;

namespace negocio
{
    public class MarcaNegocio
    {
        AccesoDatos datos = new AccesoDatos();

        public List<Marca> listar()
        {
            List<Marca> lista = new List<Marca>();

            try
            {
                datos.setearConsulta("SELECT Id, Descripcion FROM MARCAS");
                datos.ejecutarLectura();

                while(datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(aux);
                }

                return lista;
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

        public void agregar(Marca marca)
        {
            try
            {
                datos.setearConsulta("INSERT INTO MARCAS (Descripcion) VALUES (@descripcion)");
                datos.setearParametro("@descripcion", marca.Descripcion);
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

        public void modificar(Marca marca)
        {
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Descripcion = @descripcion WHERE Id = @id");
                datos.setearParametro("@descripcion", marca.Descripcion);
                datos.setearParametro("@id", marca.Id);
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

        public int verificar (int id)
        {
            try
            {
                datos.setearConsulta("SELECT COUNT (Id) FROM ARTICULOS WHERE IdMarca = @idVer");
                datos.setearParametro("@idVer", id);
                datos.ejecutarLectura();
                datos.Lector.Read();
                int cantArt = (int)datos.Lector[0];

                return cantArt;

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
                datos.setearConsulta("DELETE FROM MARCAS WHERE Id = @id");
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
