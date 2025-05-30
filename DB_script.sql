USE [master]
GO
/****** Object:  Database [Production_ERP]    Script Date: 18/02/2025 18:30:54 ******/
CREATE DATABASE [Production_ERP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Production_ERP', FILENAME = N'C:\Users\kboyl\Production_ERP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Production_ERP_log', FILENAME = N'C:\Users\kboyl\Production_ERP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Production_ERP] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Production_ERP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Production_ERP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Production_ERP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Production_ERP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Production_ERP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Production_ERP] SET ARITHABORT OFF 
GO
ALTER DATABASE [Production_ERP] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Production_ERP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Production_ERP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Production_ERP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Production_ERP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Production_ERP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Production_ERP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Production_ERP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Production_ERP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Production_ERP] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Production_ERP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Production_ERP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Production_ERP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Production_ERP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Production_ERP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Production_ERP] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Production_ERP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Production_ERP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Production_ERP] SET  MULTI_USER 
GO
ALTER DATABASE [Production_ERP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Production_ERP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Production_ERP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Production_ERP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Production_ERP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Production_ERP] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Production_ERP] SET QUERY_STORE = OFF
GO
USE [Production_ERP]
GO
/****** Object:  Table [dbo].[BOM]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOM](
	[BOMID] [int] IDENTITY(1,1) NOT NULL,
	[BOMName] [nvarchar](255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[Status] [int] NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[Comments] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[BOMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOMItem]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOMItem](
	[BOMItemID] [int] IDENTITY(1,1) NOT NULL,
	[BOMID] [int] NOT NULL,
	[ItemName] [nvarchar](255) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Unit] [nvarchar](50) NOT NULL,
	[Supplier] [nvarchar](255) NULL,
	[UnitCost] [decimal](18, 2) NULL,
	[TotalCost]  AS ([Quantity]*[UnitCost]) PERSISTED,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NOT NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[Comments] [nvarchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[BOMItemID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BOMStatus]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BOMStatus](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[StatusName] [nvarchar](50) NOT NULL,
	[StatusDescription] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materials]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materials](
	[MaterialID] [int] IDENTITY(1,1) NOT NULL,
	[MaterialName] [nvarchar](max) NOT NULL,
	[MaterialType] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[CurrentStock] [decimal](18, 2) NOT NULL,
	[UOMID] [int] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED 
(
	[MaterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MaterialTypes]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaterialTypes](
	[MaterialTypeID] [int] IDENTITY(1,1) NOT NULL,
	[MaterialTypeName] [nvarchar](100) NOT NULL,
	[MaterialTypeAbbreviation] [nvarchar](10) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaterialTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PackagingMaterials]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackagingMaterials](
	[PackagingMaterialID] [int] IDENTITY(1,1) NOT NULL,
	[MaterialName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[UnitOfMeasure] [nvarchar](max) NOT NULL,
	[CurrentStock] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_FinishedMaterials] PRIMARY KEY CLUSTERED 
(
	[PackagingMaterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionBatches]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionBatches](
	[ProductionBatchID] [int] IDENTITY(1,1) NOT NULL,
	[ProductionOrderID] [int] NOT NULL,
	[BatchNumber] [nvarchar](max) NOT NULL,
	[BatchStartDate] [datetime2](7) NULL,
	[BatchEndDate] [datetime2](7) NULL,
	[Status] [nvarchar](max) NOT NULL,
	[QuantityProduced] [decimal](18, 2) NULL,
	[ProductionOrderID1] [int] NOT NULL,
 CONSTRAINT [PK_ProductionBatches] PRIMARY KEY CLUSTERED 
(
	[ProductionBatchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionOrderProcesses]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionOrderProcesses](
	[ProductionOrderProcessID] [int] IDENTITY(1,1) NOT NULL,
	[ProductionOrderID] [int] NOT NULL,
	[ProductionProcessID] [int] NOT NULL,
	[StartDate] [datetime2](7) NULL,
	[EndDate] [datetime2](7) NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProductionOrderProcesses] PRIMARY KEY CLUSTERED 
(
	[ProductionOrderProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionOrderRawMaterials]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionOrderRawMaterials](
	[ProductionOrderRawMaterialID] [int] IDENTITY(1,1) NOT NULL,
	[ProductionOrderID] [int] NOT NULL,
	[RawMaterialID] [int] NOT NULL,
	[QuantityUsed] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_ProductionOrderRawMaterials] PRIMARY KEY CLUSTERED 
(
	[ProductionOrderRawMaterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionOrders]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionOrders](
	[ProductionOrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [nvarchar](max) NOT NULL,
	[ProductID] [int] NULL,
	[OrderDate] [datetime2](7) NOT NULL,
	[DueDate] [datetime2](7) NULL,
	[Status] [nvarchar](max) NOT NULL,
	[QuantityRequested] [decimal](18, 2) NOT NULL,
	[QuantityProduced] [decimal](18, 2) NOT NULL,
	[ProductionBatchID] [int] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[UpdatedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_ProductionOrders] PRIMARY KEY CLUSTERED 
(
	[ProductionOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductionProcesses]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductionProcesses](
	[ProductionProcessID] [int] IDENTITY(1,1) NOT NULL,
	[ProcessName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[EstimatedTimeInHours] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProductionProcesses] PRIMARY KEY CLUSTERED 
(
	[ProductionProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[ProductCode] [nvarchar](100) NOT NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK__Products__B40CC6CDEE1BB3AB] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QualityControls]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QualityControls](
	[QualityControlID] [int] IDENTITY(1,1) NOT NULL,
	[ProductionOrderID] [int] NOT NULL,
	[QCDate] [datetime2](7) NOT NULL,
	[TestName] [nvarchar](max) NOT NULL,
	[TestResult] [nvarchar](max) NOT NULL,
	[ResultValue] [decimal](18, 2) NULL,
	[Status] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_QualityControls] PRIMARY KEY CLUSTERED 
(
	[QualityControlID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RawMaterials]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RawMaterials](
	[RawMaterialID] [int] IDENTITY(1,1) NOT NULL,
	[MaterialName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[UnitOfMeasure] [nvarchar](max) NOT NULL,
	[CurrentStock] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_RawMaterials] PRIMARY KEY CLUSTERED 
(
	[RawMaterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UOM]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UOM](
	[UOMID] [int] IDENTITY(1,1) NOT NULL,
	[UOMCode] [nvarchar](10) NOT NULL,
	[UOMDescription] [nvarchar](100) NOT NULL,
	[UOMType] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UOMID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](255) NOT NULL,
	[LastName] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[Active] [bit] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[DateModified] [datetime] NOT NULL,
	[UserTypeID] [int] NULL,
 CONSTRAINT [PK__Users__1788CCACABD4FFA5] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTypes]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTypes](
	[UserTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](255) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BOMStatus] ON 

INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (1, N'Active', N'The BOM is currently in use and valid for production.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (2, N'Inactive', N'The BOM is no longer in use for production but is retained for historical reference.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (3, N'Archived', N'The BOM is no longer in active use and has been archived for record-keeping or compliance purposes.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (4, N'Draft', N'The BOM is still in development or under review and should not be used for production yet.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (5, N'Obsolete', N'The BOM is no longer used because it has been replaced by a newer version or is no longer required.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (6, N'Under Review', N'The BOM is currently under review for approval or modification.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (7, N'On Hold', N'The BOM is temporarily placed on hold and cannot be used for production at this moment.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (8, N'Expired', N'The BOM has passed its validity period and is no longer valid for use in manufacturing.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (9, N'Approved', N'The BOM has been officially reviewed and approved for production.')
INSERT [dbo].[BOMStatus] ([StatusID], [StatusName], [StatusDescription]) VALUES (10, N'Rejected', N'The BOM has been reviewed and rejected, typically due to quality, regulatory, or operational issues.')
SET IDENTITY_INSERT [dbo].[BOMStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[Materials] ON 

INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (1, N'Lactose', 1, N'A sugar used as an excipient in tablets and capsules', CAST(5000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (2, N'Magnesium Stearate', 1, N'Used as a lubricant in tablet manufacturing', CAST(3000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (3, N'Amoxicillin', 1, N'Antibiotic used to treat a wide variety of bacterial infections', CAST(20000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (4, N'Cellulose', 1, N'Used as a binder and filler in tablets and capsules', CAST(4500.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (5, N'Ethanol', 1, N'Used as a solvent and disinfectant in pharmaceutical formulations', CAST(10000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (6, N'Sodium Chloride', 1, N'Common salt, used in saline solutions and injectable formulations', CAST(2500.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (7, N'Purified Water', 1, N'Water used as a solvent and diluent in formulations', CAST(10000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (8, N'Gelatin', 1, N'Used for capsule shells and in the formulation of tablets', CAST(7000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (9, N'Yeast', 1, N'Used in the fermentation process for biopharmaceutical products', CAST(2000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (10, N'E. coli cultures', 1, N'Bacterial cultures used for producing recombinant proteins and other biopharmaceuticals', CAST(500.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (11, N'Cell lines', 1, N'Cultured cells used for the production of therapeutic proteins', CAST(150.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (12, N'Glass Vials', 2, N'Glass containers used to store injectable medicines', CAST(5000.00 AS Decimal(18, 2)), 36, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (13, N'Blister Packs', 2, N'Plastic or aluminum packaging for individual tablets or capsules', CAST(10000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (14, N'Bottles (Plastic)', 2, N'Plastic containers used for liquid medicines or tablets', CAST(8000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (15, N'Syringes', 2, N'Used for injectable formulations and vaccines', CAST(3000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (16, N'Ampoules', 2, N'Glass containers for single-dose injectable medications', CAST(4000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (17, N'Dropper Bottles', 2, N'Small bottles with a dropper for eye drops or liquid medications', CAST(6000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (18, N'Plastic Pouches', 2, N'Flexible packaging material, often used for small doses or sachets', CAST(15000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (19, N'IV Bags', 2, N'Plastic bags used for intravenous fluids and medications', CAST(2000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (20, N'Cartons', 2, N'Paperboard used for secondary packaging and shipment', CAST(1000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (21, N'Labels', 2, N'Adhesive labels for identifying products', CAST(10000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (22, N'Foil (Aluminum)', 2, N'Aluminum foil for sealing and protecting products in blister packs', CAST(3000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (23, N'Plastic Caps', 2, N'Caps used for bottles to seal contents', CAST(12000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (24, N'Foam Inserts', 2, N'Protective foam used for cushioning items in boxes', CAST(2500.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (25, N'Tamper-Evident Seals', 2, N'Seals that indicate if packaging has been opened', CAST(5000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (26, N'Desiccant Packs', 2, N'Used to absorb moisture and protect sensitive medications', CAST(7000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (27, N'Tubes', 2, N'Used for creams, ointments, or gels', CAST(4000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (28, N'Roller Bottles', 2, N'Bottles with a roller ball for applying liquid medications', CAST(3500.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (29, N'Blister Card', 2, N'Card used for holding individual doses in blister packaging', CAST(9000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (30, N'Bags (Sterile Packaging)', 2, N'Sterile packaging used for medical devices or other sterile products', CAST(5000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (31, N'Sealing Films', 2, N'Films used to seal packages for protection and tamper evidence', CAST(8000.00 AS Decimal(18, 2)), 32, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (32, N'test1', 1, N'test2', CAST(100.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (33, N'qq', 1, N'ww', CAST(1000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (34, N'Isopropyl Alcohol (IPA)', 1, N'Used for sanitizing surfaces and equipment.', CAST(30000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (35, N'Kboylan Testing', 1, N'yadda yadda', CAST(13700.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (36, N'Fake Material', 1, N'fake fake fake', CAST(2000.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (37, N'Peracetic Acid', 1, N'A strong oxidizing agent used for sterilization.', CAST(20000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (38, N'Quaternary Ammonium Compounds', 1, N'Used as disinfectants in cleanrooms.', CAST(20000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (39, N'Sodium Hypochlorite', 1, N'A disinfectant used for cleaning equipment and facilities.', CAST(10000.00 AS Decimal(18, 2)), 3, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (40, N'Polypropylene (PP)', 1, N'Used for caps, closures, and containers.', CAST(500.00 AS Decimal(18, 2)), 2, 1)
INSERT [dbo].[Materials] ([MaterialID], [MaterialName], [MaterialType], [Description], [CurrentStock], [UOMID], [Active]) VALUES (41, N'Hydroxypropyl Methylcellulose (HPMC)', 1, N'A polymer used as a coating agent and binder.', CAST(3000.00 AS Decimal(18, 2)), 3, 1)
SET IDENTITY_INSERT [dbo].[Materials] OFF
GO
SET IDENTITY_INSERT [dbo].[MaterialTypes] ON 

INSERT [dbo].[MaterialTypes] ([MaterialTypeID], [MaterialTypeName], [MaterialTypeAbbreviation], [IsActive]) VALUES (1, N'Raw Material', N'RM', 1)
INSERT [dbo].[MaterialTypes] ([MaterialTypeID], [MaterialTypeName], [MaterialTypeAbbreviation], [IsActive]) VALUES (2, N'Finished Material', N'FM', 1)
SET IDENTITY_INSERT [dbo].[MaterialTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[PackagingMaterials] ON 

INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (1, N'Glass Vials', N'Glass containers used to store injectable medicines', N'unit', CAST(5000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (2, N'Blister Packs', N'Plastic or aluminum packaging for individual tablets or capsules', N'pack', CAST(10000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (3, N'Bottles (Plastic)', N'Plastic containers used for liquid medicines or tablets', N'unit', CAST(8000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (4, N'Syringes', N'Used for injectable formulations and vaccines', N'unit', CAST(3000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (5, N'Ampoules', N'Glass containers for single-dose injectable medications', N'unit', CAST(4000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (6, N'Dropper Bottles', N'Small bottles with a dropper for eye drops or liquid medications', N'unit', CAST(6000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (7, N'Plastic Pouches', N'Flexible packaging material, often used for small doses or sachets', N'unit', CAST(15000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (8, N'IV Bags', N'Plastic bags used for intravenous fluids and medications', N'unit', CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (9, N'Cartons', N'Paperboard used for secondary packaging and shipment', N'box', CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (10, N'Labels', N'Adhesive labels for identifying products', N'roll', CAST(10000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (11, N'Foil (Aluminum)', N'Aluminum foil for sealing and protecting products in blister packs', N'roll', CAST(3000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (12, N'Plastic Caps', N'Caps used for bottles to seal contents', N'unit', CAST(12000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (13, N'Foam Inserts', N'Protective foam used for cushioning items in boxes', N'sheet', CAST(2500.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (14, N'Tamper-Evident Seals', N'Seals that indicate if packaging has been opened', N'roll', CAST(5000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (15, N'Desiccant Packs', N'Used to absorb moisture and protect sensitive medications', N'pack', CAST(7000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (16, N'Tubes', N'Used for creams, ointments, or gels', N'unit', CAST(4000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (17, N'Roller Bottles', N'Bottles with a roller ball for applying liquid medications', N'unit', CAST(3500.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (18, N'Blister Card', N'Card used for holding individual doses in blister packaging', N'card', CAST(9000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (19, N'Bags (Sterile Packaging)', N'Sterile packaging used for medical devices or other sterile products', N'unit', CAST(5000.00 AS Decimal(18, 2)))
INSERT [dbo].[PackagingMaterials] ([PackagingMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (20, N'Sealing Films', N'Films used to seal packages for protection and tamper evidence', N'roll', CAST(8000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[PackagingMaterials] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (1, N'Anadin', N'ANA-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (2, N'Paracetomol', N'PAR-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (3, N'Ibuprofen', N'IBU-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (4, N'Amoxicillin', N'AMO-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (5, N'Lisinopril', N'LIS-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (6, N'Metformin', N'MET-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (7, N'Atorvastatin', N'ATO-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (8, N'Simvastatin', N'SIM-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (9, N'Omeprazole', N'OME-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (10, N'Ciprofloxacin', N'CIP-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (11, N'Lemslip', N'LEM-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (12, N'Claritin', N'CLA-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (13, N'Gaviscon', N'GAV-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (14, N'Ibuprofen Z', N'string', 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (15, N'Robitussin', N'ROB-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (16, N'test', N'test-1234', 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (17, N'qwerty', N'qwe-1234', 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (18, N'ttttttt', N'ttt-1234', 0)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (19, N'Amlodipine', N'AML-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (20, N'Lopressor', N'LOP-1234', 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [ProductCode], [Active]) VALUES (21, N'Prozac', N'PRO-1234', 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[RawMaterials] ON 

INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (1, N'Lactose', N'A sugar used as an excipient in tablets and capsules', N'g', CAST(5000.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (2, N'Magnesium Stearate', N'Used as a lubricant in tablet manufacturing', N'g', CAST(3000.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (3, N'Amoxicillin', N'Antibiotic used to treat a wide variety of bacterial infections', N'mg', CAST(20000.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (4, N'Cellulose', N'Used as a binder and filler in tablets and capsules', N'g', CAST(4500.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (5, N'Ethanol', N'Used as a solvent and disinfectant in pharmaceutical formulations', N'mL', CAST(10000.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (6, N'Sodium Chloride', N'Common salt, used in saline solutions and injectable formulations', N'g', CAST(2500.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (7, N'Purified Water', N'Water used as a solvent and diluent in formulations', N'L', CAST(10000.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (8, N'Gelatin', N'Used for capsule shells and in the formulation of tablets', N'g', CAST(7000.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (9, N'Yeast', N'Used in the fermentation process for biopharmaceutical products', N'g', CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (10, N'E. coli cultures', N'Bacterial cultures used for producing recombinant proteins and other biopharmaceuticals', N'mL', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[RawMaterials] ([RawMaterialID], [MaterialName], [Description], [UnitOfMeasure], [CurrentStock]) VALUES (11, N'Cell lines', N'Cultured cells used for the production of therapeutic proteins', N'vial', CAST(150.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[RawMaterials] OFF
GO
SET IDENTITY_INSERT [dbo].[UOM] ON 

INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (1, N'kg', N'Kilogram', N'Weight', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (2, N'g', N'Gram', N'Weight', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (3, N'mg', N'Milligram', N'Weight', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (4, N'lb', N'Pound', N'Weight', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (5, N'oz', N'Ounce', N'Weight', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (6, N'L', N'Liter', N'Volume', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (7, N'mL', N'Milliliter', N'Volume', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (8, N'cm3', N'Cubic Centimeter', N'Volume', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (9, N'gal', N'Gallon', N'Volume', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (10, N'fl oz', N'Fluid Ounce', N'Volume', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (11, N'm', N'Meter', N'Length', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (12, N'cm', N'Centimeter', N'Length', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (13, N'mm', N'Millimeter', N'Length', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (14, N'km', N'Kilometer', N'Length', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (15, N'in', N'Inch', N'Length', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (16, N'ft', N'Foot', N'Length', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (17, N'yd', N'Yard', N'Length', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (18, N'hr', N'Hour', N'Time', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (19, N'min', N'Minute', N'Time', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (20, N'sec', N'Second', N'Time', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (21, N'°C', N'Celsius', N'Temperature', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (22, N'°F', N'Fahrenheit', N'Temperature', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (23, N'Pa', N'Pascal', N'Pressure', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (24, N'psi', N'Pounds per Square Inch', N'Pressure', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (25, N'atm', N'Atmosphere', N'Pressure', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (26, N'mol', N'Mole', N'Quantity', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (27, N'piece', N'Piece', N'Count', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (28, N'bag', N'Bag', N'Count', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (29, N'box', N'Box', N'Count', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (30, N'carton', N'Carton', N'Count', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (31, N'set', N'Set', N'Count', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (32, N'pkg', N'Package', N'Count', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (33, N'dose', N'Dose', N'Pharmaceutical', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (34, N'tablet', N'Tablet', N'Pharmaceutical', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (35, N'capsule', N'Capsule', N'Pharmaceutical', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (36, N'vial', N'Vial', N'Pharmaceutical', 1)
INSERT [dbo].[UOM] ([UOMID], [UOMCode], [UOMDescription], [UOMType], [IsActive]) VALUES (37, N'bottle', N'Bottle', N'Pharmaceutical', 1)
SET IDENTITY_INSERT [dbo].[UOM] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [Email], [Username], [Password], [Active], [DateCreated], [DateModified], [UserTypeID]) VALUES (1, N'Kevin', N'Boylan', N'kboylan@testing.com', N'kboylan', N'$2a$11$GPxcYvGVvnpTuNPXsdLQPeY/B1izGOOpt4zZfUvL773hzswjflbM6', 1, CAST(N'2025-01-31T10:15:54.840' AS DateTime), CAST(N'2025-02-06T14:41:24.603' AS DateTime), 5)
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [Email], [Username], [Password], [Active], [DateCreated], [DateModified], [UserTypeID]) VALUES (2, N'John', N'Doe', N'jdoe@test.com', N'jdoe', N'$2a$11$r44BCz.LQMHqhuMrWwDSqe4xcZJtphmW.bPK/C36MXEXgLBQWezCy', 1, CAST(N'2025-01-31T10:15:54.840' AS DateTime), CAST(N'2025-01-31T10:15:54.840' AS DateTime), 2)
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [Email], [Username], [Password], [Active], [DateCreated], [DateModified], [UserTypeID]) VALUES (3, N'Jane', N'Doe', N'janedoe456@test.com', N'janedoe', N'blahblah', 1, CAST(N'2025-01-31T10:15:54.840' AS DateTime), CAST(N'2025-02-01T14:31:17.587' AS DateTime), 2)
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [Email], [Username], [Password], [Active], [DateCreated], [DateModified], [UserTypeID]) VALUES (7, N'Joe', N'Shmoe', N'jshmoe@testing.com', N'jshmoe', N'$2a$11$grfXhdyz5P7tIBhsI7rz4OnyC0SY9OlTQ8SHR1Bbe5AgwjvKh3oaS', 1, CAST(N'2025-01-31T15:16:31.040' AS DateTime), CAST(N'2025-01-31T15:16:32.583' AS DateTime), 2)
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [Email], [Username], [Password], [Active], [DateCreated], [DateModified], [UserTypeID]) VALUES (8, N'Fred', N'Smith', N'fsmith@test123.com', N'fsmith', N'$2a$11$NuEk4aTn806AV4QZfP0TqeVFWv2x9Rk9Fzema/XPLKtsu/B7oy9cy', 1, CAST(N'2025-02-02T15:38:23.637' AS DateTime), CAST(N'2025-02-06T10:49:59.677' AS DateTime), 2)
INSERT [dbo].[Users] ([UserID], [FirstName], [LastName], [Email], [Username], [Password], [Active], [DateCreated], [DateModified], [UserTypeID]) VALUES (9, N'Shane', N'MacGowan', N'shane@test.com', N'shanemac', N'$2a$11$1sHj2rxxY4Q6rBiw8qEQueFEPel/1oCPVmY2x1qxo.9sGfbJ8W.fK', 1, CAST(N'2025-02-16T14:20:11.083' AS DateTime), CAST(N'2025-02-16T14:20:12.447' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[UserTypes] ON 

INSERT [dbo].[UserTypes] ([UserTypeID], [Type], [Active]) VALUES (1, N'Operator', 1)
INSERT [dbo].[UserTypes] ([UserTypeID], [Type], [Active]) VALUES (2, N'Support', 1)
INSERT [dbo].[UserTypes] ([UserTypeID], [Type], [Active]) VALUES (3, N'User', 1)
INSERT [dbo].[UserTypes] ([UserTypeID], [Type], [Active]) VALUES (4, N'Superuser', 1)
INSERT [dbo].[UserTypes] ([UserTypeID], [Type], [Active]) VALUES (5, N'Admin', 1)
SET IDENTITY_INSERT [dbo].[UserTypes] OFF
GO
/****** Object:  Index [IX_ProductionBatches_ProductionOrderID1]    Script Date: 18/02/2025 18:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_ProductionBatches_ProductionOrderID1] ON [dbo].[ProductionBatches]
(
	[ProductionOrderID1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductionOrderProcesses_ProductionOrderID]    Script Date: 18/02/2025 18:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_ProductionOrderProcesses_ProductionOrderID] ON [dbo].[ProductionOrderProcesses]
(
	[ProductionOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductionOrderProcesses_ProductionProcessID]    Script Date: 18/02/2025 18:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_ProductionOrderProcesses_ProductionProcessID] ON [dbo].[ProductionOrderProcesses]
(
	[ProductionProcessID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductionOrderRawMaterials_ProductionOrderID]    Script Date: 18/02/2025 18:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_ProductionOrderRawMaterials_ProductionOrderID] ON [dbo].[ProductionOrderRawMaterials]
(
	[ProductionOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ProductionOrderRawMaterials_RawMaterialID]    Script Date: 18/02/2025 18:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_ProductionOrderRawMaterials_RawMaterialID] ON [dbo].[ProductionOrderRawMaterials]
(
	[RawMaterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Products__2F4E024FC6845AC2]    Script Date: 18/02/2025 18:30:54 ******/
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [UQ__Products__2F4E024FC6845AC2] UNIQUE NONCLUSTERED 
(
	[ProductCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_QualityControls_ProductionOrderID]    Script Date: 18/02/2025 18:30:54 ******/
CREATE NONCLUSTERED INDEX [IX_QualityControls_ProductionOrderID] ON [dbo].[QualityControls]
(
	[ProductionOrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BOM] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[BOMItem] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[MaterialTypes] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UOM] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[BOM]  WITH CHECK ADD  CONSTRAINT [FK_BOM_BOMStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[BOMStatus] ([StatusID])
GO
ALTER TABLE [dbo].[BOM] CHECK CONSTRAINT [FK_BOM_BOMStatus]
GO
ALTER TABLE [dbo].[BOMItem]  WITH CHECK ADD  CONSTRAINT [FK_BOMItem_BOM] FOREIGN KEY([BOMID])
REFERENCES [dbo].[BOM] ([BOMID])
GO
ALTER TABLE [dbo].[BOMItem] CHECK CONSTRAINT [FK_BOMItem_BOM]
GO
ALTER TABLE [dbo].[Materials]  WITH CHECK ADD  CONSTRAINT [FK_Materials_MaterialType] FOREIGN KEY([MaterialType])
REFERENCES [dbo].[MaterialTypes] ([MaterialTypeID])
GO
ALTER TABLE [dbo].[Materials] CHECK CONSTRAINT [FK_Materials_MaterialType]
GO
ALTER TABLE [dbo].[Materials]  WITH CHECK ADD  CONSTRAINT [FK_Materials_UOM] FOREIGN KEY([UOMID])
REFERENCES [dbo].[UOM] ([UOMID])
GO
ALTER TABLE [dbo].[Materials] CHECK CONSTRAINT [FK_Materials_UOM]
GO
ALTER TABLE [dbo].[ProductionBatches]  WITH CHECK ADD  CONSTRAINT [FK_ProductionBatches_ProductionOrders_ProductionOrderID1] FOREIGN KEY([ProductionOrderID1])
REFERENCES [dbo].[ProductionOrders] ([ProductionOrderID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductionBatches] CHECK CONSTRAINT [FK_ProductionBatches_ProductionOrders_ProductionOrderID1]
GO
ALTER TABLE [dbo].[ProductionOrderProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderProcesses_ProductionOrders_ProductionOrderID] FOREIGN KEY([ProductionOrderID])
REFERENCES [dbo].[ProductionOrders] ([ProductionOrderID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductionOrderProcesses] CHECK CONSTRAINT [FK_ProductionOrderProcesses_ProductionOrders_ProductionOrderID]
GO
ALTER TABLE [dbo].[ProductionOrderProcesses]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderProcesses_ProductionProcesses_ProductionProcessID] FOREIGN KEY([ProductionProcessID])
REFERENCES [dbo].[ProductionProcesses] ([ProductionProcessID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductionOrderProcesses] CHECK CONSTRAINT [FK_ProductionOrderProcesses_ProductionProcesses_ProductionProcessID]
GO
ALTER TABLE [dbo].[ProductionOrderRawMaterials]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderRawMaterials_ProductionOrders_ProductionOrderID] FOREIGN KEY([ProductionOrderID])
REFERENCES [dbo].[ProductionOrders] ([ProductionOrderID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductionOrderRawMaterials] CHECK CONSTRAINT [FK_ProductionOrderRawMaterials_ProductionOrders_ProductionOrderID]
GO
ALTER TABLE [dbo].[ProductionOrderRawMaterials]  WITH CHECK ADD  CONSTRAINT [FK_ProductionOrderRawMaterials_RawMaterials_RawMaterialID] FOREIGN KEY([RawMaterialID])
REFERENCES [dbo].[RawMaterials] ([RawMaterialID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductionOrderRawMaterials] CHECK CONSTRAINT [FK_ProductionOrderRawMaterials_RawMaterials_RawMaterialID]
GO
ALTER TABLE [dbo].[QualityControls]  WITH CHECK ADD  CONSTRAINT [FK_QualityControls_ProductionOrders_ProductionOrderID] FOREIGN KEY([ProductionOrderID])
REFERENCES [dbo].[ProductionOrders] ([ProductionOrderID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[QualityControls] CHECK CONSTRAINT [FK_QualityControls_ProductionOrders_ProductionOrderID]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_UserTypes] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserTypes] ([UserTypeID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_UserTypes]
GO
/****** Object:  StoredProcedure [dbo].[InsertMaterial]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertMaterial]
    @MaterialName NVARCHAR(MAX),
    @MaterialType INT,
    @Description NVARCHAR(MAX),
    @CurrentStock DECIMAL(18, 2),
    @UOMID INT = NULL,  -- Optional parameter
    @Active BIT = NULL -- Optional parameter
AS
BEGIN
    -- Insert the new record into the Materials table
    INSERT INTO [dbo].[Materials] (
        [MaterialName],
        [MaterialType],
        [Description],
        [CurrentStock],
        [UOMID],
        [Active]
    )
    VALUES (
        @MaterialName,
        @MaterialType,
        @Description,
        @CurrentStock,
        @UOMID,
        @Active
    );

    -- Return the newly generated MaterialID
    SELECT SCOPE_IDENTITY() AS MaterialID;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateMaterial]    Script Date: 18/02/2025 18:30:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateMaterial]
    @MaterialID INT,
    @MaterialName NVARCHAR(MAX) = NULL,
    @MaterialType INT = NULL,
    @Description NVARCHAR(MAX) = NULL,
    @CurrentStock DECIMAL(18, 2) = NULL,
    @UOMID INT = NULL,
    @Active BIT = NULL
AS
BEGIN
    -- Start the update process
    UPDATE [dbo].[Materials]
    SET
        [MaterialName] = ISNULL(@MaterialName, [MaterialName]),
        [MaterialType] = ISNULL(@MaterialType, [MaterialType]),
        [Description] = ISNULL(@Description, [Description]),
        [CurrentStock] = ISNULL(@CurrentStock, [CurrentStock]),
        [UOMID] = ISNULL(@UOMID, [UOMID]),
        [Active] = ISNULL(@Active, [Active])
    WHERE
        [MaterialID] = @MaterialID;

    -- Check if any rows were affected
    IF @@ROWCOUNT = 0
    BEGIN
        -- If no rows were affected, it means the MaterialID was not found
        RAISERROR('No material found with the specified MaterialID.', 16, 1);
    END
END
GO
USE [master]
GO
ALTER DATABASE [Production_ERP] SET  READ_WRITE 
GO
