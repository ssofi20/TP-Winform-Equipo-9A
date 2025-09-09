USE CATALOGO_P3_DB
GO

--Consulta para armar listado de articulos
SELECT A.Id IdArticulo, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, A.IdCategoria, A.Precio, M.Id IdMarca, M.Descripcion NombreMarca, C.Id IdCategoria, C.Descripcion NombreCategoria, I.ImagenUrl, I.Id IdImagen
FROM ARTICULOS A, MARCAS M, CATEGORIAS C, IMAGENES I
WHERE A.IdMarca = M.Id AND A.IdCategoria = C.Id AND A.Id = I.IdArticulo

SELECT * FROM IMAGENES
