USE [master]
GO
/****** Object:  Database [Quickstart]    Script Date: 28/08/2021 1:20:07 p. m. ******/
CREATE DATABASE [Quickstart] 
USE [Quickstart]

GO
CREATE TABLE [dbo].[Blog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
 CONSTRAINT [PK_Blog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Post]    Script Date: 28/08/2021 1:20:07 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Content] [varchar](max) NULL,
	[BlogId] [int] NULL,
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Blog] ON 

INSERT [dbo].[Blog] ([Id], [Name]) VALUES (1, N'Angular JS')
INSERT [dbo].[Blog] ([Id], [Name]) VALUES (2, N'Node')
INSERT [dbo].[Blog] ([Id], [Name]) VALUES (3, N'Flutter')
INSERT [dbo].[Blog] ([Id], [Name]) VALUES (4, N'React')
INSERT [dbo].[Blog] ([Id], [Name]) VALUES (5, N'Java SpringBoot')
SET IDENTITY_INSERT [dbo].[Blog] OFF
GO
ALTER TABLE [dbo].[Post]  WITH CHECK ADD  CONSTRAINT [FK_Post_Blog] FOREIGN KEY([BlogId])
REFERENCES [dbo].[Blog] ([Id])
GO
ALTER TABLE [dbo].[Post] CHECK CONSTRAINT [FK_Post_Blog]
GO
/****** Object:  StoredProcedure [dbo].[CreateBlog]    Script Date: 28/08/2021 1:20:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CreateBlog]
	@Name	VARCHAR(50),
	@Message VARCHAR(100) OUTPUT
AS

IF EXISTS (SELECT 1 FROM [dbo].[Blog] WHERE LOWER([Name]) = LOWER(@Name))
BEGIN
	SET @Message = 'The blog exist';
	RETURN;
END

BEGIN TRAN Process 

INSERT INTO [dbo].[Blog]([Name])
VALUES(@Name);
SET @Message = 'The process is successful';

COMMIT TRAN

IF @@ERROR != 0
BEGIN
	SET @Message = ERROR_MESSAGE()
	ROLLBACK TRAN
END
GO
/****** Object:  StoredProcedure [dbo].[EditBlog]    Script Date: 28/08/2021 1:20:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[EditBlog]
	@Id		INT,
	@Name	VARCHAR(50),
	@Message VARCHAR(100) OUTPUT
AS

BEGIN TRAN Process 

UPDATE [dbo].[Blog]
SET [Name] = @Name
WHERE [Id] = @Id;

SET @Message = 'The process is successful';

COMMIT TRAN

IF @@ERROR != 0
BEGIN
	SET @Message = ERROR_MESSAGE()
	ROLLBACK TRAN
END
GO
/****** Object:  StoredProcedure [dbo].[GetBlogs]    Script Date: 28/08/2021 1:20:08 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBlogs]
	@Id	INT
AS

SELECT [Id]
      ,UPPER([Name]) Name
  FROM [dbo].[Blog]
  WHERE ([Id] = @Id OR @Id = -1);
GO
USE [master]
GO
ALTER DATABASE [Quickstart] SET  READ_WRITE 
GO
