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

CREATE TABLE [dbo].[UserPostImage]
([UserPostImageId]    INT IDENTITY(1, 1) NOT NULL, 
 [UserPostId]         INT NOT NULL, 
 [FileName]           NVARCHAR(256) NOT NULL,
 [Description]        NVARCHAR(1000) NULL, 
 [CreatedBy]          NVARCHAR(256) NOT NULL, 
 [CreatedOn]          DATETIME NOT NULL, 
 
 CONSTRAINT [PK_UserPostImage_UserPostImageId] PRIMARY KEY CLUSTERED([UserPostImageId] ASC)
);
GO

ALTER TABLE [UserPostImage]
ADD CONSTRAINT [DF_UserPostImage_CreatedOn] DEFAULT(GETDATE()) FOR [CreatedOn];
GO

CREATE PROCEDURE [dbo].[CreateUserPost]
(
	@title NVARCHAR(100),
	@description NVARCHAR(1000),
	@createdBy NVARCHAR(256),
	@postType INT,
	@price DECIMAL(5,2),
	@address NVARCHAR(256),
	@postalCode NVARCHAR(15),
	@fileName NVARCHAR(256)
)
AS
	BEGIN

	DECLARE @postId INT;

	INSERT INTO [dbo].[UserPost]
	(
		 [Title]  , 
		 [Description]  , 
		 [IsApproved]         , 
		 [IsActive]         , 
		 [CreatedBy]         , 
		 [CreatedOn]       , 
		 [PostType]   , 
		 [Price]      , 
		 [Address]  , 
		 [PostalCode]
	)
	VALUES
	(
		@title ,
		@description ,
		'false',
		'false',
		@createdBy ,
		GETUTCDATE(),
		@postType ,
		@price ,
		@address ,
		@postalCode 
	)

	SELECT @postId = SCOPE_IDENTITY();

	INSERT INTO [dbo].[UserPostImage]
	(
		[UserPostId] ,
		 [FileName] ,
		 [CreatedBy]    ,
		 [CreatedOn]
	)
	VALUES(@postId, @fileName, @createdBy, GETUTCDATE())
	END

GO