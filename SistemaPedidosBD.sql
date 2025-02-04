USE [SistemaPedidos]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 31/01/2025 4:04:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[ClienteID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](15) NOT NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClienteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Telefono] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallePedidos]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallePedidos](
	[DetalleID] [int] IDENTITY(1,1) NOT NULL,
	[PedidoID] [int] NOT NULL,
	[PlatoID] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Subtotal] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[DetalleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mesas]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesas](
	[MesaID] [int] IDENTITY(1,1) NOT NULL,
	[NumeroMesa] [int] NOT NULL,
	[Capacidad] [int] NOT NULL,
	[Ubicacion] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[MesaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NumeroMesa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[PedidoID] [int] IDENTITY(1,1) NOT NULL,
	[ClienteID] [int] NOT NULL,
	[MesaID] [int] NOT NULL,
	[FechaHora] [datetime] NOT NULL,
	[Estado] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PedidoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Platos]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Platos](
	[PlatoID] [int] IDENTITY(1,1) NOT NULL,
	[NombrePlato] [nvarchar](100) NOT NULL,
	[Precio] [decimal](10, 2) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Categoria] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PlatoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pedidos] ADD  DEFAULT (getdate()) FOR [FechaHora]
GO
ALTER TABLE [dbo].[Pedidos] ADD  DEFAULT ('Pendiente') FOR [Estado]
GO
ALTER TABLE [dbo].[DetallePedidos]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedidos_Pedidos] FOREIGN KEY([PedidoID])
REFERENCES [dbo].[Pedidos] ([PedidoID])
GO
ALTER TABLE [dbo].[DetallePedidos] CHECK CONSTRAINT [FK_DetallePedidos_Pedidos]
GO
ALTER TABLE [dbo].[DetallePedidos]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedidos_Platos] FOREIGN KEY([PlatoID])
REFERENCES [dbo].[Platos] ([PlatoID])
GO
ALTER TABLE [dbo].[DetallePedidos] CHECK CONSTRAINT [FK_DetallePedidos_Platos]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Clientes] FOREIGN KEY([ClienteID])
REFERENCES [dbo].[Clientes] ([ClienteID])
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Clientes]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Mesas] FOREIGN KEY([MesaID])
REFERENCES [dbo].[Mesas] ([MesaID])
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Mesas]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarCliente]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarCliente]
    @ClienteID INT,
    @Nombre NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Clientes
    SET Nombre = @Nombre, Telefono = @Telefono, Email = @Email
    WHERE ClienteID = @ClienteID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ActualizarDetallePedido]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarDetallePedido]
    @DetalleID INT,
    @PedidoID INT,
    @PlatoID INT,
    @Cantidad INT,
    @Subtotal DECIMAL(10, 2)
AS
BEGIN
    UPDATE DetallePedidos
    SET PedidoID = @PedidoID, PlatoID = @PlatoID, Cantidad = @Cantidad, Subtotal = @Subtotal
    WHERE DetalleID = @DetalleID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ActualizarMesa]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarMesa]
    @MesaID INT,
    @NumeroMesa INT,
    @Capacidad INT,
    @Ubicacion NVARCHAR(100)
AS
BEGIN
    UPDATE Mesas
    SET NumeroMesa = @NumeroMesa, Capacidad = @Capacidad, Ubicacion = @Ubicacion
    WHERE MesaID = @MesaID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ActualizarPedido]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarPedido]
    @PedidoID INT,
    @ClienteID INT,
    @MesaID INT,
    @Estado NVARCHAR(50)
AS
BEGIN
    UPDATE Pedidos
    SET ClienteID = @ClienteID, MesaID = @MesaID, Estado = @Estado
    WHERE PedidoID = @PedidoID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ActualizarPlato]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarPlato]
    @PlatoID INT,
    @NombrePlato NVARCHAR(100),
    @Precio DECIMAL(10, 2),
    @Descripcion NVARCHAR(MAX),
    @Categoria NVARCHAR(50)
AS
BEGIN
    UPDATE Platos
    SET NombrePlato = @NombrePlato, Precio = @Precio, Descripcion = @Descripcion, Categoria = @Categoria
    WHERE PlatoID = @PlatoID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarClientePorID]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarClientePorID]
    @ClienteID INT
AS
BEGIN
    SELECT * FROM Clientes WHERE ClienteID = @ClienteID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarClientes]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarClientes]
AS
BEGIN
    SELECT * FROM Clientes;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarDetallePedidoPorID]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarDetallePedidoPorID]
    @DetalleID INT
AS
BEGIN
    SELECT * FROM DetallePedidos WHERE DetalleID = @DetalleID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarDetallePedidos]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarDetallePedidos]
AS
BEGIN
    SELECT * FROM DetallePedidos;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarMesaPorID]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarMesaPorID]
    @MesaID INT
AS
BEGIN
    SELECT * FROM Mesas WHERE MesaID = @MesaID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarMesas]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarMesas]
AS
BEGIN
    SELECT * FROM Mesas;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarPedidoPorID]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarPedidoPorID]
    @PedidoID INT
AS
BEGIN
    SELECT * FROM Pedidos WHERE PedidoID = @PedidoID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarPedidos]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarPedidos]
AS
BEGIN
    SELECT * FROM Pedidos;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarPlatoPorID]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarPlatoPorID]
    @PlatoID INT
AS
BEGIN
    SELECT * FROM Platos WHERE PlatoID = @PlatoID;
END;

GO
/****** Object:  StoredProcedure [dbo].[ConsultarPlatos]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ConsultarPlatos]
AS
BEGIN
    SELECT * FROM Platos;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarCliente]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarCliente]
    @ClienteID INT
AS
BEGIN
    DELETE FROM Clientes WHERE ClienteID = @ClienteID;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarDetallePedido]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarDetallePedido]
    @DetalleID INT
AS
BEGIN
    DELETE FROM DetallePedidos WHERE DetalleID = @DetalleID;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarMesa]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarMesa]
    @MesaID INT
AS
BEGIN
    DELETE FROM Mesas WHERE MesaID = @MesaID;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarPedido]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarPedido]
    @PedidoID INT
AS
BEGIN
    DELETE FROM Pedidos WHERE PedidoID = @PedidoID;
END;

GO
/****** Object:  StoredProcedure [dbo].[EliminarPlato]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EliminarPlato]
    @PlatoID INT
AS
BEGIN
    DELETE FROM Platos WHERE PlatoID = @PlatoID;
END;

GO
/****** Object:  StoredProcedure [dbo].[InsertarCliente]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimientos Almacenados para la Tabla Clientes
CREATE PROCEDURE [dbo].[InsertarCliente]
    @Nombre NVARCHAR(100),
    @Telefono NVARCHAR(20),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Clientes (Nombre, Telefono, Email)
    VALUES (@Nombre, @Telefono, @Email);
END;

GO
/****** Object:  StoredProcedure [dbo].[InsertarDetallePedido]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimientos Almacenados para la Tabla DetallePedidos
CREATE PROCEDURE [dbo].[InsertarDetallePedido]
    @PedidoID INT,
    @PlatoID INT,
    @Cantidad INT,
    @Subtotal DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO DetallePedidos (PedidoID, PlatoID, Cantidad, Subtotal)
    VALUES (@PedidoID, @PlatoID, @Cantidad, @Subtotal);
END;

GO
/****** Object:  StoredProcedure [dbo].[InsertarMesa]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimientos Almacenados para la Tabla Mesas
CREATE PROCEDURE [dbo].[InsertarMesa]
    @NumeroMesa INT,
    @Capacidad INT,
    @Ubicacion NVARCHAR(100)
AS
BEGIN
    INSERT INTO Mesas (NumeroMesa, Capacidad, Ubicacion)
    VALUES (@NumeroMesa, @Capacidad, @Ubicacion);
END;

GO
/****** Object:  StoredProcedure [dbo].[InsertarPedido]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimientos Almacenados para la Tabla Pedidos
CREATE PROCEDURE [dbo].[InsertarPedido]
    @ClienteID INT,
    @MesaID INT,
    @FechaHora DATETIME,
    @Estado NVARCHAR(50)
AS
BEGIN
    INSERT INTO Pedidos (ClienteID, MesaID, FechaHora, Estado)
    VALUES (@ClienteID, @MesaID, @FechaHora, @Estado);
END;

GO
/****** Object:  StoredProcedure [dbo].[InsertarPlato]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Procedimientos Almacenados para la Tabla Platos
CREATE PROCEDURE [dbo].[InsertarPlato]
    @NombrePlato NVARCHAR(100),
    @Precio DECIMAL(10, 2),
    @Descripcion NVARCHAR(MAX),
    @Categoria NVARCHAR(50)
AS
BEGIN
    INSERT INTO Platos (NombrePlato, Precio, Descripcion, Categoria)
    VALUES (@NombrePlato, @Precio, @Descripcion, @Categoria);
END;

GO
/****** Object:  StoredProcedure [dbo].[ObtenerPedidosPorCliente]    Script Date: 31/01/2025 4:04:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerPedidosPorCliente]
    @ClienteID INT
AS
BEGIN
    SELECT 
        p.PedidoID,
        p.FechaHora,
        p.Estado,
        m.NumeroMesa,
        dp.PlatoID,
        dp.Cantidad,
        dp.Subtotal,
        pl.NombrePlato
    FROM Pedidos p
    INNER JOIN Mesas m ON p.MesaID = m.MesaID
    INNER JOIN DetallePedidos dp ON p.PedidoID = dp.PedidoID
    INNER JOIN Platos pl ON dp.PlatoID = pl.PlatoID
    WHERE p.ClienteID = @ClienteID;
END;
GO
