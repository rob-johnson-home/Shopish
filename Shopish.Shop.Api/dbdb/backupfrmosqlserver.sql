USE [MMTShop]
GO
/****** Object:  Table [dbo].[MMTCategories]    Script Date: 21/02/2021 14:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMTCategories](
	[ID] [uniqueidentifier] NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[SKUPrefix] [nchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MMTFeaturedProducts]    Script Date: 21/02/2021 14:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMTFeaturedProducts](
	[ID] [uniqueidentifier] NOT NULL,
	[IDPrefix] [nchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MMTProducts]    Script Date: 21/02/2021 14:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMTProducts](
	[ID] [uniqueidentifier] NOT NULL,
	[SKU] [nchar](10) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Description] [nchar](500) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MMTCategories] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[MMTFeaturedProducts] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[MMTProducts] ADD  DEFAULT (newid()) FOR [ID]
GO
ALTER TABLE [dbo].[MMTProducts]  WITH CHECK ADD  CONSTRAINT [FK_MMTProducts_MMTCategories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[MMTCategories] ([ID])
GO
ALTER TABLE [dbo].[MMTProducts] CHECK CONSTRAINT [FK_MMTProducts_MMTCategories]
GO
/****** Object:  StoredProcedure [dbo].[spMMTGetCategories]    Script Date: 21/02/2021 14:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spMMTGetCategories]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT c.ID,c.Name, c.SKUPrefix from MMTCategories c
END
GO
/****** Object:  StoredProcedure [dbo].[spMMTGetFeaturedProducts]    Script Date: 21/02/2021 14:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spMMTGetFeaturedProducts]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT p.ID,p.SKU,p.Name,p.Description,p.Price,c.ID CategoryID from MMTProducts p
	left join MMTCategories c on c.ID = p.CategoryID
	left join MMTFeaturedProducts f on SUBSTRING(p.SKU,1,LEN(f.IDPrefix)) = f.IDPrefix
	where f.IDPrefix IS NOT NULL
END
GO
/****** Object:  StoredProcedure [dbo].[spMMTGetProductsByCategory]    Script Date: 21/02/2021 14:05:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spMMTGetProductsByCategory]
	@Category varchar(36)
AS
BEGIN

	declare @CategoryID uniqueidentifier;
	set @CategoryID = CONVERT( uniqueidentifier, @Category );
	SET NOCOUNT ON;

	SELECT p.ID,p.SKU,p.Name,p.Description,p.Price,CategoryID from MMTProducts p
	where p.CategoryID = @CategoryID
END
GO
