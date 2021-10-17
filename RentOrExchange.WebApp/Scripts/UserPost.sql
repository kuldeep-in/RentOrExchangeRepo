CREATE TABLE [dbo].[UserPost]
([UserPostId]     [INT] IDENTITY(1, 1) NOT NULL, 
 [Title]  [NVARCHAR](100) NOT NULL, 
 [Description]  [NVARCHAR](1000) NULL, 
 [IsApproved]          [BIT] NOT NULL, 
 [IsActive]          [BIT] NOT NULL, 
 [CreatedBy]          [NVARCHAR](256) NOT NULL, 
 [CreatedOn]          [DATETIME] NOT NULL, 
 [PostType]          [INT] NOT NULL, 
 [Price]          DECIMAL(5,2) NOT NULL, 
 [Address]          [NVARCHAR](256) NULL, 
 [PostalCode]    [NVARCHAR](15) NOT NULL,  
 CONSTRAINT [PK_UserPost_UserPostId] PRIMARY KEY CLUSTERED([UserPostId] ASC)
);
GO

ALTER TABLE [UserPost]
ADD CONSTRAINT [DF_UserPost_CreatedOn] DEFAULT(GETDATE()) FOR [CreatedOn];
GO