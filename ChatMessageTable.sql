USE [YourDBNameGoesHere]
GO

/****** Object:  Table [dbo].[ChatMessage]    Script Date: 6/17/2015 10:39:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ChatMessage](
	[ChatMessageId] [int] IDENTITY(1,1) NOT NULL,
	[ChatId] [uniqueidentifier] NOT NULL,
	[From] [nvarchar](255) NOT NULL,
	[Msg] [nvarchar](max) NULL,
	[AddedOn] [datetime] NOT NULL CONSTRAINT [DF_ChatMessage_AddedOn]  DEFAULT (getdate())	
 CONSTRAINT [PK_ChatMessage] PRIMARY KEY CLUSTERED 
(
	[ChatMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

