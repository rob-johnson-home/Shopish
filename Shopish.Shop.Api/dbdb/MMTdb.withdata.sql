USE [MMTShop]
GO
/****** Object:  Table [dbo].[MMTCategories]    Script Date: 19/02/2021 17:17:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMTCategories](
	[ID] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY ,
	[Name] [nchar](50) NOT NULL,
	[SKUPrefix] [nchar](10) NOT NULL)
 
GO
/****** Object:  Table [dbo].[MMTFeaturedProducts]    Script Date: 19/02/2021 17:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMTFeaturedProducts](
	[ID] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY ,
	[IDPrefix] [nchar](10) NOT NULL)
 
GO
/****** Object:  Table [dbo].[MMTProducts]    Script Date: 19/02/2021 17:17:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MMTProducts](
	[ID] [uniqueidentifier] DEFAULT NEWID() PRIMARY KEY ,
	[SKU] [nchar](10) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[Description] [nchar](500) NOT NULL,
	[Price] [decimal](8, 2) NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL)
 
GO
ALTER TABLE [dbo].[MMTProducts]  WITH CHECK ADD  CONSTRAINT [FK_MMTProducts_MMTCategories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[MMTCategories] ([ID])
GO
ALTER TABLE [dbo].[MMTProducts] CHECK CONSTRAINT [FK_MMTProducts_MMTCategories]
GO


INSERT [dbo].[MMTCategories] (Name, SKUPrefix)
VALUES
	('Home', '1'),
	('Garden', '2'),
	('Electronics', '3'),
	('Fitness', '4'),
	('Toys', '5')

GO

INSERT [dbo].[MMTProducts] (SKU,Name,Description,Price,CategoryID)
VALUES
	('10001','Test Home Product', 'Test Home Product Description', 5.99, (select ID from dbo.mmtcategories where name='Home')),
	('20001','Test Garden Product', 'Test Garden Product Description', 7.56, (select ID from dbo.mmtcategories where name='Garden')),
	('30001','Test Electronics Product', 'Test Electronics Product Description', 199.99,( select ID from dbo.mmtcategories where name='Electronics')),
	('40001','Test Fitness Product', 'Test Fitness Product Description', 1200.00, (select ID from dbo.mmtcategories where name='Fitness')),
	('50001','Test Toys Product', 'Test Toys Product Description', 29.99, (select ID from dbo.mmtcategories where name='Toys'))

go

insert dbo.MMTFeaturedProducts (IDPrefix )
values ('1'),('2'),('3')


GO

CREATE PROCEDURE spMMTGetCategories

AS
BEGIN
	SET NOCOUNT ON;
	SELECT c.ID,c.Name, c.SKUPrefix from MMTCategories c
END
GO


CREATE PROCEDURE spMMTGetFeaturedProducts

AS
BEGIN
	SET NOCOUNT ON;
	SELECT p.ID,p.SKU,p.Name,p.Description,p.Price,c.ID CategoryID from MMTProducts p
	left join MMTCategories c on c.ID = p.CategoryID
	left join MMTFeaturedProducts f on SUBSTRING(p.SKU,1,LEN(f.IDPrefix)) = f.IDPrefix
	where f.IDPrefix IS NOT NULL
END
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
