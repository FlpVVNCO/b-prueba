-- PREGUNTA 1
SELECT 
    SUM(VD.TotalLinea) AS MontoTotalVentas, 
    COUNT(*) AS CantidadTotalVentas
FROM 
    VentaDetalle VD
    INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
WHERE 
    V.Fecha >= DATEADD(day, -30, GETDATE());

-- PREGUNTA 2
SELECT TOP 1 
    V.Fecha, 
    VD.TotalLinea
FROM 
    VentaDetalle VD
    INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
WHERE 
    V.Fecha >= DATEADD(day, -30, GETDATE())
ORDER BY 
    VD.TotalLinea DESC;
   
-- PREGUNTA 3   
SELECT TOP 1 
    VD.ID_Producto, 
    SUM(VD.TotalLinea) AS MontoTotalVentas
FROM 
    VentaDetalle VD
    INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
WHERE 
    V.Fecha >= DATEADD(day, -30, GETDATE())
GROUP BY 
    VD.ID_Producto
ORDER BY 
    MontoTotalVentas DESC;

-- PREGUNTA 4   
SELECT TOP 1 
    V.ID_Local, 
    SUM(VD.TotalLinea) AS MontoTotalVentas
FROM 
    VentaDetalle VD
    INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
WHERE 
    V.Fecha >= DATEADD(day, -30, GETDATE())
GROUP BY 
    V.ID_Local
ORDER BY 
    MontoTotalVentas DESC;

-- PREGUNTA 5
SELECT TOP 1 
    P.ID_Marca, 
    SUM((VD.Cantidad * VD.Precio_Unitario) - (VD.Cantidad * P.Costo_Unitario)) AS MargenGanancias
FROM 
    VentaDetalle VD
    INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
    INNER JOIN Producto P ON VD.ID_Producto = P.ID_Producto
WHERE 
    V.Fecha >= DATEADD(day, -30, GETDATE())
GROUP BY 
    P.ID_Marca
ORDER BY 
    MargenGanancias DESC;

-- PREGUNTA 6
WITH ProductosMasVendidosPorLocal AS (
  SELECT 
    V.ID_Local, 
    VD.ID_Producto, 
    P.Nombre AS Nombre_Producto,
    SUM(VD.Cantidad) AS TotalVentas,
    RANK() OVER (PARTITION BY V.ID_Local ORDER BY SUM(VD.Cantidad) DESC) AS Ranking
  FROM 
    VentaDetalle VD
    INNER JOIN Venta V ON VD.ID_Venta = V.ID_Venta
    INNER JOIN Producto P ON VD.ID_Producto = P.ID_Producto
  WHERE 
    V.Fecha >= DATEADD(day, -30, GETDATE())
  GROUP BY 
    V.ID_Local, 
    VD.ID_Producto,
    P.Nombre
)
SELECT 
  ID_Local, 
  ID_Producto,
  Nombre_Producto,
  TotalVentas
FROM ProductosMasVendidosPorLocal
WHERE Ranking = 1;