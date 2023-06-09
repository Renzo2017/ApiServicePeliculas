USE [master]
GO
/****** Object:  Database [BDPeliculas]    Script Date: 10/03/2023 09:36:37 p. m. ******/
CREATE DATABASE [BDPeliculas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BDPeliculas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BDPeliculas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BDPeliculas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BDPeliculas_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BDPeliculas] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BDPeliculas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BDPeliculas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BDPeliculas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BDPeliculas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BDPeliculas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BDPeliculas] SET ARITHABORT OFF 
GO
ALTER DATABASE [BDPeliculas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BDPeliculas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BDPeliculas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BDPeliculas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BDPeliculas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BDPeliculas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BDPeliculas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BDPeliculas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BDPeliculas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BDPeliculas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BDPeliculas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BDPeliculas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BDPeliculas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BDPeliculas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BDPeliculas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BDPeliculas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BDPeliculas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BDPeliculas] SET RECOVERY FULL 
GO
ALTER DATABASE [BDPeliculas] SET  MULTI_USER 
GO
ALTER DATABASE [BDPeliculas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BDPeliculas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BDPeliculas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BDPeliculas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BDPeliculas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BDPeliculas] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BDPeliculas', N'ON'
GO
ALTER DATABASE [BDPeliculas] SET QUERY_STORE = OFF
GO
USE [BDPeliculas]
GO
/****** Object:  Table [dbo].[Actores]    Script Date: 10/03/2023 09:36:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
 CONSTRAINT [PK_Actores] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genero]    Script Date: 10/03/2023 09:36:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genero](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
 CONSTRAINT [PK_Genero] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Peliculas]    Script Date: 10/03/2023 09:36:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Peliculas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](100) NULL,
	[Descripcion] [varchar](max) NULL,
	[Año] [int] NULL,
	[IDActor] [int] NOT NULL,
	[IDGenero] [int] NOT NULL,
 CONSTRAINT [PK_Peliculas] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 10/03/2023 09:36:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NombreUsuario] [varchar](50) NULL,
	[Contraseña] [varchar](50) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Actores] ON 

INSERT [dbo].[Actores] ([ID], [Nombre], [Apellido]) VALUES (1, N'Sylvester', N'Stallone')
INSERT [dbo].[Actores] ([ID], [Nombre], [Apellido]) VALUES (2, N'
Brad', N'Pitt')
INSERT [dbo].[Actores] ([ID], [Nombre], [Apellido]) VALUES (3, N'Kevin', N'Hart')
INSERT [dbo].[Actores] ([ID], [Nombre], [Apellido]) VALUES (4, N'Arnold', N'Schwarzenegger')
SET IDENTITY_INSERT [dbo].[Actores] OFF
GO
SET IDENTITY_INSERT [dbo].[Genero] ON 

INSERT [dbo].[Genero] ([ID], [Descripcion]) VALUES (1, N'Drama')
INSERT [dbo].[Genero] ([ID], [Descripcion]) VALUES (2, N'Acción')
INSERT [dbo].[Genero] ([ID], [Descripcion]) VALUES (3, N'Suspenso')
INSERT [dbo].[Genero] ([ID], [Descripcion]) VALUES (4, N'Comedia')
INSERT [dbo].[Genero] ([ID], [Descripcion]) VALUES (5, N'Terror')
SET IDENTITY_INSERT [dbo].[Genero] OFF
GO
SET IDENTITY_INSERT [dbo].[Peliculas] ON 

INSERT [dbo].[Peliculas] ([ID], [Titulo], [Descripcion], [Año], [IDActor], [IDGenero]) VALUES (1, N'Rocky', N'Rocky Balboa es un desconocido boxeador a quien se le ofrece la posibilidad de pelear por el título mundial de los pesos pesados. Con una gran fuerza de voluntad, Rocky se prepara concienzudamente para el combate y de la misma manera para los cambios que acabarán produciéndose en su vida.', 1976, 1, 1)
INSERT [dbo].[Peliculas] ([ID], [Titulo], [Descripcion], [Año], [IDActor], [IDGenero]) VALUES (3, N'Rambo: La ultima misión', N'John Rambo vive tranquilo en un rancho en Arizona, pero cuando recibe la noticia de que una adolescente ha desaparecido tras haber cruzado la frontera a México para ir a una fiesta, decide ir en su búsqueda.', 2019, 1, 2)
INSERT [dbo].[Peliculas] ([ID], [Titulo], [Descripcion], [Año], [IDActor], [IDGenero]) VALUES (4, N'Guerra Mundial Z', N'Cuando una pandemia de zombis amenaza con destruir a la humanidad, un exinvestigador de Naciones Unidas es obligado a regresar al servicio para intentar descubrir la fuente de la infección.', 2013, 2, 5)
INSERT [dbo].[Peliculas] ([ID], [Titulo], [Descripcion], [Año], [IDActor], [IDGenero]) VALUES (5, N'Terminator', N'Un asesino cibernético del futuro es enviado a Los Ángeles para matar a la mujer que procreará a un líder.', 1984, 4, 2)
INSERT [dbo].[Peliculas] ([ID], [Titulo], [Descripcion], [Año], [IDActor], [IDGenero]) VALUES (7, N'Escuela Nocturna', N'Producto disponible asdf', 2016, 2, 1)
SET IDENTITY_INSERT [dbo].[Peliculas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([ID], [NombreUsuario], [Contraseña]) VALUES (1, N'admin', N'1234')
INSERT [dbo].[Usuarios] ([ID], [NombreUsuario], [Contraseña]) VALUES (2, N'user', N'123')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Peliculas]  WITH CHECK ADD  CONSTRAINT [FK_Peliculas_Actores] FOREIGN KEY([IDActor])
REFERENCES [dbo].[Actores] ([ID])
GO
ALTER TABLE [dbo].[Peliculas] CHECK CONSTRAINT [FK_Peliculas_Actores]
GO
ALTER TABLE [dbo].[Peliculas]  WITH CHECK ADD  CONSTRAINT [FK_Peliculas_Genero] FOREIGN KEY([IDGenero])
REFERENCES [dbo].[Genero] ([ID])
GO
ALTER TABLE [dbo].[Peliculas] CHECK CONSTRAINT [FK_Peliculas_Genero]
GO
/****** Object:  StoredProcedure [dbo].[EliminarActorProc]    Script Date: 10/03/2023 09:36:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[EliminarActorProc]
(@id int)
as

Begin
Delete From Actores where ID =@id

End

GO
/****** Object:  StoredProcedure [dbo].[EliminarGeneroProc]    Script Date: 10/03/2023 09:36:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[EliminarGeneroProc]
(@id int)
as

Begin
Delete From Genero where ID =@id

End

GO
/****** Object:  StoredProcedure [dbo].[EliminarPeliculaProc]    Script Date: 10/03/2023 09:36:37 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[EliminarPeliculaProc]
(@id int)
as

Begin
Delete From Peliculas where ID =@id

End

GO
USE [master]
GO
ALTER DATABASE [BDPeliculas] SET  READ_WRITE 
GO
