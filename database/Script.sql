USE [UserManagement]
GO
/*===========================================[dbo].[tblUsers]==========================================================*/

/****** Object:  Table [dbo].[tblUsers]    Script Date: 12/07/2024 6:48:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblUsers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[MiddleName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Zip] [nvarchar](50) NULL,
	[CityId] [int] NULL,
	[StateId] [int] NULL,
	[CountryId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[LastUpdated] [datetime] NULL,
	[LastUpdatedBy] [int] NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [EmailConfirmed]
GO

ALTER TABLE [dbo].[tblUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [PhoneNumberConfirmed]
GO

ALTER TABLE [dbo].[tblUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [TwoFactorEnabled]
GO

ALTER TABLE [dbo].[tblUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO

ALTER TABLE [dbo].[tblUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO


/*===========================================[dbo].[tblApplicationException]==========================================================*/

/****** Object:  Table [dbo].[tblApplicationException]    Script Date: 12/07/2024 6:50:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblApplicationException](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[Source] [nvarchar](max) NULL,
	[Method] [nvarchar](max) NULL,
	[StackTrace] [nvarchar](max) NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_tblApplicationException] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


/*===========================================[dbo].[AddEditUserDetails]==========================================================*/

/****** Object:  StoredProcedure [dbo].[AddEditUserDetails]    Script Date: 23/04/2024 8:11:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddEditUserDetails]
@Id INT,
@UserName NVARCHAR(256),
@Email NVARCHAR(256),
@Password NVARCHAR(MAX),
@FirstName NVARCHAR(100),
--@MiddleName NVARCHAR(100),
@LastName NVARCHAR(100),
@PhoneNumber NVARCHAR(MAX),
@LoginUserId INT
AS
BEGIN
	DECLARE @Error BIT
	DECLARE @Message NVARCHAR(MAX)
	IF @Id > 0
	BEGIN
		IF (SELECT COUNT(Id) FROM tblUsers WHERE Id <> @Id AND UserName = @UserName) > 0
		BEGIN
			SET @Error = 1
			SET @Message = 'Username already exist'
		END
		ELSE IF (SELECT COUNT(Id) FROM tblUsers WHERE Id <> @Id AND Email = @Email) > 0
		BEGIN
			SET @Error = 1
			SET @Message = 'Email already exist'
		END
		ELSE
		BEGIN
			BEGIN TRY
				UPDATE tblUsers
				SET UserName = @UserName,
					Email = @Email,
					Password = @Password,
					FirstName = @FirstName, 
					MiddleName = NULL,
					LastName = @LastName, 
					PhoneNumber = @PhoneNumber, 
					LastUpdated = GETDATE(),
					LastUpdatedBy = @LoginUserId
				WHERE Id = @Id
				
				SET @Error = 0
				SET @Message = 'User updated successfully'
			END TRY
			BEGIN CATCH
				SET @Error = 1
			    SET @Message  = ERROR_MESSAGE()
			END CATCH
		END
	END
	ELSE
	BEGIN
		BEGIN TRY
			INSERT INTO tblUsers
				(UserName, Email, Password, FirstName, MiddleName, LastName, PhoneNumber, PhoneNumberConfirmed, TwoFactorEnabled, IsActive, CreatedOn, CreatedBy, IsDeleted)
			VALUES
				(@UserName, @Email, @Password, @FirstName, NULL, @LastName, @PhoneNumber, 0, 0, 1, GETDATE(), @LoginUserId, 0)
			
			SET @Error = 0
			SET @Message = 'User inserted successfully'
		END TRY
		BEGIN CATCH
			SET @Error = 1
		    SET @Message  = ERROR_MESSAGE()
		END CATCH
	END
	SELECT @Error AS Error, @Message AS [Message]
END


/*===========================================[dbo].[DeleteUser]==========================================================*/

/****** Object:  StoredProcedure [dbo].[DeleteUser]    Script Date: 12/07/2024 12:07:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[DeleteUser]
@Id INT
AS
BEGIN
	DECLARE @Error BIT
	DECLARE @Message NVARCHAR(MAX)
	IF @Id > 0
	BEGIN
		IF (SELECT COUNT(Id) FROM tblUsers WHERE Id = @Id) > 0
		BEGIN
			BEGIN TRY
				UPDATE tblUsers SET IsDeleted = 1, IsActive = 0 WHERE Id = @Id
				SET @Error = 0
				SET @Message = 'User deleted successfully'
			END TRY
			BEGIN CATCH
				SET @Error = 1
			    SET @Message  = ERROR_MESSAGE()
			END CATCH
		END
		ELSE
		BEGIN
			SET @Error = 1
			SET @Message = 'User not found'
		END
	END
	ELSE
	BEGIN
		SET @Error = 1
		SET @Message = 'Please provice user'
	END
	SELECT @Error AS Error, @Message AS [Message]
END