USE [master]
GO
/****** Object:  Database [Dragorant]    Script Date: 19.01.2021 00:11:01 ******/
CREATE DATABASE [Dragorant]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Dragorant', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Dragorant.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Dragorant_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Dragorant_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Dragorant] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Dragorant].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Dragorant] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Dragorant] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Dragorant] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Dragorant] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Dragorant] SET ARITHABORT OFF 
GO
ALTER DATABASE [Dragorant] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Dragorant] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Dragorant] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Dragorant] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Dragorant] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Dragorant] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Dragorant] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Dragorant] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Dragorant] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Dragorant] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Dragorant] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Dragorant] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Dragorant] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Dragorant] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Dragorant] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Dragorant] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Dragorant] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Dragorant] SET RECOVERY FULL 
GO
ALTER DATABASE [Dragorant] SET  MULTI_USER 
GO
ALTER DATABASE [Dragorant] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Dragorant] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Dragorant] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Dragorant] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Dragorant] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Dragorant', N'ON'
GO
ALTER DATABASE [Dragorant] SET QUERY_STORE = OFF
GO
USE [Dragorant]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](100) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Deliverer]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Deliverer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[cityId] [bigint] NOT NULL,
	[address] [nchar](100) NOT NULL,
	[username] [nchar](50) NOT NULL,
	[password] [varchar](max) NOT NULL,
	[email] [nchar](50) NOT NULL,
	[firstName] [nchar](50) NOT NULL,
	[lastName] [nchar](50) NOT NULL,
	[dragonCoinBalance] [float] NOT NULL,
 CONSTRAINT [PK_Deliverer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DelivererRate]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DelivererRate](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[delivererId] [bigint] NOT NULL,
	[date] [datetime2](7) NOT NULL,
	[value] [float] NOT NULL,
	[userId] [bigint] NOT NULL,
 CONSTRAINT [PK_DelivererRate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](100) NOT NULL,
	[price] [float] NOT NULL,
	[isActive] [bit] NULL,
	[idRestaurant] [bigint] NOT NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodIngredient]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodIngredient](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[foodId] [bigint] NOT NULL,
	[ingredientId] [bigint] NOT NULL,
 CONSTRAINT [PK_FoodIngredient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodOrder]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodOrder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[foodId] [bigint] NOT NULL,
	[orderId] [bigint] NOT NULL,
 CONSTRAINT [PK_FoodOrder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FoodRate]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodRate](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NOT NULL,
	[foodId] [bigint] NOT NULL,
	[date] [datetime2](7) NOT NULL,
	[value] [float] NOT NULL,
 CONSTRAINT [PK_FoodRate] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredient]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredient](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Ingredient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[orderStationId] [bigint] NOT NULL,
	[delivererId] [bigint] NULL,
	[userId] [bigint] NOT NULL,
	[status] [nchar](100) NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[endTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStation]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[resteurantId] [bigint] NOT NULL,
	[cityId] [bigint] NOT NULL,
	[address] [nchar](100) NOT NULL,
	[username] [nchar](50) NOT NULL,
	[password] [varchar](max) NOT NULL,
 CONSTRAINT [PK_OrderStation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Owner]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Owner](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[cityId] [bigint] NOT NULL,
	[address] [nchar](100) NOT NULL,
	[username] [nchar](50) NOT NULL,
	[password] [varchar](max) NOT NULL,
	[firstName] [nchar](50) NOT NULL,
	[lastName] [nchar](50) NOT NULL,
	[email] [nchar](50) NOT NULL,
 CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restaurant]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restaurant](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[ownerId] [bigint] NOT NULL,
	[name] [nchar](100) NOT NULL,
 CONSTRAINT [PK_Restaurant] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19.01.2021 00:11:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[cityId] [bigint] NOT NULL,
	[address] [nchar](100) NOT NULL,
	[username] [nchar](50) NOT NULL,
	[password] [varchar](max) NOT NULL,
	[email] [nchar](50) NOT NULL,
	[firstName] [nchar](50) NOT NULL,
	[lastName] [nchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Deliverer]  WITH CHECK ADD  CONSTRAINT [FK_Deliverer_City] FOREIGN KEY([cityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Deliverer] CHECK CONSTRAINT [FK_Deliverer_City]
GO
ALTER TABLE [dbo].[DelivererRate]  WITH CHECK ADD  CONSTRAINT [FK_DelivererRate_Deliverer] FOREIGN KEY([delivererId])
REFERENCES [dbo].[Deliverer] ([Id])
GO
ALTER TABLE [dbo].[DelivererRate] CHECK CONSTRAINT [FK_DelivererRate_Deliverer]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK_Food_Restaurant] FOREIGN KEY([idRestaurant])
REFERENCES [dbo].[Restaurant] ([id])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK_Food_Restaurant]
GO
ALTER TABLE [dbo].[FoodIngredient]  WITH CHECK ADD  CONSTRAINT [FK_FoodIngredient_Food] FOREIGN KEY([foodId])
REFERENCES [dbo].[Food] ([Id])
GO
ALTER TABLE [dbo].[FoodIngredient] CHECK CONSTRAINT [FK_FoodIngredient_Food]
GO
ALTER TABLE [dbo].[FoodIngredient]  WITH CHECK ADD  CONSTRAINT [FK_FoodIngredient_Ingredient] FOREIGN KEY([ingredientId])
REFERENCES [dbo].[Ingredient] ([Id])
GO
ALTER TABLE [dbo].[FoodIngredient] CHECK CONSTRAINT [FK_FoodIngredient_Ingredient]
GO
ALTER TABLE [dbo].[FoodOrder]  WITH CHECK ADD  CONSTRAINT [FK_FoodOrder_Food] FOREIGN KEY([foodId])
REFERENCES [dbo].[Food] ([Id])
GO
ALTER TABLE [dbo].[FoodOrder] CHECK CONSTRAINT [FK_FoodOrder_Food]
GO
ALTER TABLE [dbo].[FoodOrder]  WITH CHECK ADD  CONSTRAINT [FK_FoodOrder_Order] FOREIGN KEY([orderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[FoodOrder] CHECK CONSTRAINT [FK_FoodOrder_Order]
GO
ALTER TABLE [dbo].[FoodRate]  WITH CHECK ADD  CONSTRAINT [FK_FoodRate_Food] FOREIGN KEY([foodId])
REFERENCES [dbo].[Food] ([Id])
GO
ALTER TABLE [dbo].[FoodRate] CHECK CONSTRAINT [FK_FoodRate_Food]
GO
ALTER TABLE [dbo].[FoodRate]  WITH CHECK ADD  CONSTRAINT [FK_FoodRate_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[FoodRate] CHECK CONSTRAINT [FK_FoodRate_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Deliverer] FOREIGN KEY([delivererId])
REFERENCES [dbo].[Deliverer] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Deliverer]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderStation] FOREIGN KEY([orderStationId])
REFERENCES [dbo].[OrderStation] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderStation]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderStation]  WITH CHECK ADD  CONSTRAINT [FK_OrderStation_City] FOREIGN KEY([cityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[OrderStation] CHECK CONSTRAINT [FK_OrderStation_City]
GO
ALTER TABLE [dbo].[OrderStation]  WITH CHECK ADD  CONSTRAINT [FK_OrderStation_Restaurant] FOREIGN KEY([resteurantId])
REFERENCES [dbo].[Restaurant] ([id])
GO
ALTER TABLE [dbo].[OrderStation] CHECK CONSTRAINT [FK_OrderStation_Restaurant]
GO
ALTER TABLE [dbo].[Owner]  WITH CHECK ADD  CONSTRAINT [FK_Owner_City] FOREIGN KEY([cityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[Owner] CHECK CONSTRAINT [FK_Owner_City]
GO
ALTER TABLE [dbo].[Restaurant]  WITH CHECK ADD  CONSTRAINT [FK_Restaurant_Owner] FOREIGN KEY([ownerId])
REFERENCES [dbo].[Owner] ([Id])
GO
ALTER TABLE [dbo].[Restaurant] CHECK CONSTRAINT [FK_Restaurant_Owner]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_City] FOREIGN KEY([cityId])
REFERENCES [dbo].[City] ([Id])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_City]
GO
USE [master]
GO
ALTER DATABASE [Dragorant] SET  READ_WRITE 
GO
