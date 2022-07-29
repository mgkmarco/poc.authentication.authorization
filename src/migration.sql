--DROP Table [dbo].[User]

CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[NormalizedEmail] [nvarchar](256) NOT NULL,
	[PasswordHash] [nvarchar](256) NULL,
	[UserName] [nvarchar](64) NOT NULL,
	[NormalizedUserName] [nvarchar](64) NOT NULL,
	[Name] [nvarchar](64) NULL,
	[PhoneNumber] [nvarchar] (64) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [Datetime2] NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedDate] [Datetime2] NOT NULL
	CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_NormalizedUserName 
	ON [dbo].[User] (NormalizedUserName);

CREATE UNIQUE NONCLUSTERED INDEX IX_UserName 
	ON [dbo].[User] (UserName);

CREATE UNIQUE NONCLUSTERED INDEX IX_NormalizedEmail
	ON [dbo].[User] (NormalizedEmail);

CREATE UNIQUE NONCLUSTERED INDEX IX_Email
	ON [dbo].[User] (Email);

GO

-- ROLE TABLE CREATION 

--DROP Table [dbo].[Role]

CREATE TABLE [dbo].[Role](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](16) NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedDate] [Datetime2] NOT NULL
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_Name
	ON	[dbo].[Role] (Name);

GO

-- User Role TABLE CREATION 

--DROP Table [dbo].[UserRole]

CREATE TABLE [dbo].[UserRole](
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedDate] [Datetime2] NOT NULL
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC, 
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_UserId
	ON	[dbo].[UserRole] (UserId);

CREATE UNIQUE NONCLUSTERED INDEX IX_RoleId
	ON	[dbo].[UserRole] (RoleId);

GO

ALTER TABLE [dbo].[UserRole]
   ADD CONSTRAINT FK_User_UserRole FOREIGN KEY (UserId)
      REFERENCES [dbo].[User] (Id)
      ON DELETE CASCADE

GO

ALTER TABLE [dbo].[UserRole]
   ADD CONSTRAINT FK_Role_UserRole FOREIGN KEY (RoleId)
      REFERENCES [dbo].[Role] (Id)
      ON DELETE CASCADE

GO

-- USERCLAIMS TABLE CREATION

--DROP Table [dbo].[UserClaim]

CREATE TABLE [dbo].[UserClaim](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[ClaimType] [nvarchar](64) NOT NULL,
	[ClaimValue] [nvarchar](64) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedDate] [Datetime2] NOT NULL
 CONSTRAINT [PK_dbo.UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_UserId
	ON	[dbo].[UserClaim] (UserId);

GO

ALTER TABLE [dbo].[UserClaim]
   ADD CONSTRAINT FK_User_UserClaim FOREIGN KEY (UserId)
      REFERENCES [dbo].[User] (Id)
      ON DELETE CASCADE

GO

-- USERLOGIN CREATE TABLE

--DROP Table [dbo].[UserLogin]

CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](128) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[AuthenticationType] [nvarchar](64) NOT NULL,
	[IssuedDate] [Datetime2] NOT NULL
 CONSTRAINT [PK_dbo.UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE NONCLUSTERED INDEX IX_UserId
	ON	[dbo].[UserLogin] (UserId);

GO

CREATE NONCLUSTERED INDEX IX_ProviderKey_LoginProvider_IssuedDate
	ON	[dbo].[UserLogin] (ProviderKey, LoginProvider)
	INCLUDE (IssuedDate)
GO

ALTER TABLE [dbo].[UserLogin]
   ADD CONSTRAINT FK_User_UserLogin FOREIGN KEY (UserId)
      REFERENCES [dbo].[User] (Id)
      ON DELETE CASCADE

GO


