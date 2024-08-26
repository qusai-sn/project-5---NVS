create database [Election]

USE [Election]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Advertisement]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Advertisement](
	[National_ID] [bigint] IDENTITY(3234567891,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[list_Or_party_name] [varchar](255) NOT NULL,
	[Circle_ID] [int] NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[URL] [varchar](max) NOT NULL,
	[Status] [varchar](255) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactForm]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactForm](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](255) NULL,
	[Name] [varchar](255) NULL,
	[Subject] [varchar](255) NULL,
	[Message] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Debates]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Debates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Circle_ID] [int] NULL,
	[Date_Of_Debate] [date] NULL,
	[Topic] [nvarchar](255) NULL,
	[First_Candidate] [nvarchar](255) NULL,
	[First_Candidate_List] [nvarchar](255) NULL,
	[Second_Candidate] [nvarchar](255) NULL,
	[Second_Candidate_List] [nvarchar](255) NULL,
	[Status] [varchar](255) NULL,
	[Zoom_link] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ElectionAdvertisements]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElectionAdvertisements](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[National_ID] [bigint] NOT NULL,
	[List_Name] [nvarchar](255) NULL,
	[Party_Name] [nvarchar](255) NULL,
	[Circle] [nvarchar](255) NULL,
	[Name] [nvarchar](255) NULL,
	[Circle_ID] [int] NULL,
	[Description] [nvarchar](255) NOT NULL,
	[URL] [varchar](max) NULL,
	[Status] [varchar](255) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[CreatedAt] [datetime] NULL,
	[PaymentID] [int] NULL,
	[Plan] [nvarchar](50) NULL,
	[LikeCount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ElectionPosts]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElectionPosts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[LikeCount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ElectionSchedule]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElectionSchedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Start_Date_Of_Nominate] [date] NULL,
	[Start_Hour_Of_Nominate] [time](7) NULL,
	[End_Date_Of_Nominate] [date] NULL,
	[End_Hour_Of_Nominate] [time](7) NULL,
	[Start_Date_Of_Election] [date] NULL,
	[Start_Hour_Of_Election] [time](7) NULL,
	[End_Date_Of_Election] [date] NULL,
	[End_Hour_Of_Election] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[local_Candidates_Request]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[local_Candidates_Request](
	[Circle_ID] [int] NOT NULL,
	[List_Name] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[National_ID] [bigint] IDENTITY(3234567891,1) NOT NULL,
	[Gender] [varchar](255) NOT NULL,
	[Birth_Date] [date] NOT NULL,
	[Type_of_Chair] [varchar](255) NOT NULL,
	[Religion] [varchar](255) NOT NULL,
	[Status] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[localCandidate]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[localCandidate](
	[CandidateId] [int] IDENTITY(1,1) NOT NULL,
	[National_ID] [char](10) NULL,
	[Candidate_Name] [nvarchar](255) NULL,
	[Type_Of_Chair] [nvarchar](50) NULL,
	[List_Name] [nvarchar](100) NULL,
	[Counter] [int] NULL,
	[Picture] [nvarchar](255) NULL,
	[List_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CandidateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[localList]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[localList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[list_Name] [varchar](255) NOT NULL,
	[Circle_ID] [int] NOT NULL,
	[Counter] [int] NOT NULL,
	[List_Logo] [varchar](255) NULL,
	[ActualSeats] [int] NULL,
	[list_Candidates] [varchar](max) NULL,
	[Delegate_ID] [int] NULL,
	[Delegate_Name] [varchar](255) NULL,
	[Delegate_Phone] [varchar](255) NULL,
	[Delegate_Email] [varchar](255) NULL,
	[Status] [varchar](255) NULL,
	[reason] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartyCandidates]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartyCandidates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[National_ID] [nvarchar](20) NULL,
	[Name] [nvarchar](100) NULL,
	[Gender] [nvarchar](10) NULL,
	[Religion] [nvarchar](50) NULL,
	[PartyLIST_ID] [int] NULL,
	[picture] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartyList]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartyList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Party_Name] [nvarchar](100) NULL,
	[List_Name] [nvarchar](100) NULL,
	[Delegate_ID] [int] NULL,
	[Delegate_Name] [nvarchar](100) NULL,
	[Delegate_Phone] [nvarchar](20) NULL,
	[Delegate_Email] [nvarchar](100) NULL,
	[Counter] [int] NULL,
	[Status] [nvarchar](50) NULL,
	[reason] [nvarchar](255) NULL,
	[List_Logo] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayPalPayments]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayPalPayments](
	[PaymentID] [int] IDENTITY(1,1) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentMethod] [nvarchar](50) NULL,
	[TransactionID] [nvarchar](100) NULL,
	[Status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[PaymentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USERS]    Script Date: 8/11/2024 12:29:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[National_ID] [bigint] IDENTITY(1234567891,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Password] [varchar](255) NULL,
	[Circle_ID] [int] NOT NULL,
	[Gender] [varchar](255) NOT NULL,
	[local_Vote] [int] NOT NULL,
	[Party_Vote] [int] NOT NULL,
	[White_Local_Vote] [int] NOT NULL,
	[White_Party_Vote] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[National_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ElectionAdvertisements] ADD  DEFAULT ('pending') FOR [Status]
GO
ALTER TABLE [dbo].[ElectionAdvertisements] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ElectionAdvertisements] ADD  DEFAULT ((0)) FOR [LikeCount]
GO
ALTER TABLE [dbo].[ElectionPosts] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[ElectionPosts] ADD  DEFAULT ((0)) FOR [LikeCount]
GO
ALTER TABLE [dbo].[localList] ADD  DEFAULT ('pending') FOR [Status]
GO
ALTER TABLE [dbo].[PayPalPayments] ADD  DEFAULT (getdate()) FOR [PaymentDate]
GO
ALTER TABLE [dbo].[PayPalPayments] ADD  DEFAULT ('Pending') FOR [Status]
GO
ALTER TABLE [dbo].[ElectionAdvertisements]  WITH CHECK ADD FOREIGN KEY([PaymentID])
REFERENCES [dbo].[PayPalPayments] ([PaymentID])
GO
