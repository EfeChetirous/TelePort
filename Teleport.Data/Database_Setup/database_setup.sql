USE [teleport_db]
GO
/****** Object:  Table [dbo].[User]    Script Date: 28.07.2020 10:29:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](250) NULL,
	[Email] [nvarchar](500) NULL,
	[Name] [nvarchar](250) NULL,
	[Surname] [nvarchar](250) NULL,
	[Password] [nvarchar](max) NULL,
	[UserToken] [nvarchar](max) NULL,
	[DateCreated] [datetime] NULL,
	[UserCreated] [nvarchar](250) NULL,
	[DateModified] [datetime] NULL,
	[UserModified] [nvarchar](250) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [UserName], [Email], [Name], [Surname], [Password], [UserToken], [DateCreated], [UserCreated], [DateModified], [UserModified]) VALUES (4, N'admin', N'admin@admin.com', N'admin', N'admin', N'g6pO0X8CGdMspixWghZFXokWCIlt9kqY6MiYmLUeKcytds8i', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjQiLCJuYmYiOjE1OTU5MjAwNjUsImV4cCI6MTYyNzQ1NjA2NSwiaWF0IjoxNTk1OTIwMDY1fQ.94CajnV-oSs1vJymg213h7owpobtpuyMHJl-C1NqnEk', CAST(N'2020-07-27T23:50:58.410' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
