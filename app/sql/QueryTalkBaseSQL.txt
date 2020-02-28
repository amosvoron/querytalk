IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QueryTalkBase')
BEGIN
	CREATE DATABASE [QueryTalkBase];
END;
GO

USE [QueryTalkBase];
GO

SET LANGUAGE us_english;

IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Person_Sex]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [CK_Person_Sex]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_PetType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet] DROP CONSTRAINT [FK_Pet_PetType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_Owner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet] DROP CONSTRAINT [FK_Pet_Owner]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_FirstOwner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet] DROP CONSTRAINT [FK_Pet_FirstOwner]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_RelationTypeCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation] DROP CONSTRAINT [FK_PersonRelation_RelationTypeCode]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_RelatedPersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation] DROP CONSTRAINT [FK_PersonRelation_RelatedPersonID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation] DROP CONSTRAINT [FK_PersonRelation_PersonID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonNote_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonNote]'))
ALTER TABLE [dbo].[PersonNote] DROP CONSTRAINT [FK_PersonNote_PersonID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonJob_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonJob]'))
ALTER TABLE [dbo].[PersonJob] DROP CONSTRAINT [FK_PersonJob_PersonID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonJob_JobID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonJob]'))
ALTER TABLE [dbo].[PersonJob] DROP CONSTRAINT [FK_PersonJob_JobID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonExtra_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonExtra]'))
ALTER TABLE [dbo].[PersonExtra] DROP CONSTRAINT [FK_PersonExtra_PersonID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonContact_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonContact]'))
ALTER TABLE [dbo].[PersonContact] DROP CONSTRAINT [FK_PersonContact_PersonID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonContact_ContactTypeCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonContact]'))
ALTER TABLE [dbo].[PersonContact] DROP CONSTRAINT [FK_PersonContact_ContactTypeCode]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_MotherID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_MotherID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_FatherID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_FatherID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_EmploymentStatusCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_EmploymentStatusCode]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_CountryOfBirthID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_CountryOfBirthID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_AddressID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [FK_Person_AddressID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Job_CompanyID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Job]'))
ALTER TABLE [dbo].[Job] DROP CONSTRAINT [FK_Job_CompanyID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CountryContinent_CountryID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryContinent]'))
ALTER TABLE [dbo].[CountryContinent] DROP CONSTRAINT [FK_CountryContinent_CountryID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CountryContinent_ContinentID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryContinent]'))
ALTER TABLE [dbo].[CountryContinent] DROP CONSTRAINT [FK_CountryContinent_ContinentID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_ParentCompanyID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company] DROP CONSTRAINT [FK_Company_ParentCompanyID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_IndustryCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company] DROP CONSTRAINT [FK_Company_IndustryCode]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_AddressID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company] DROP CONSTRAINT [FK_Company_AddressID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_City_CountryID]') AND parent_object_id = OBJECT_ID(N'[dbo].[City]'))
ALTER TABLE [dbo].[City] DROP CONSTRAINT [FK_City_CountryID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_CityID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [FK_Address_CityID]
GO
/****** Object:  Index [UK_Person]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND name = N'UK_Person')
ALTER TABLE [dbo].[Person] DROP CONSTRAINT [UK_Person]
GO
/****** Object:  Index [UK_Address]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND name = N'UK_Address')
ALTER TABLE [dbo].[Address] DROP CONSTRAINT [UK_Address]
GO
/****** Object:  View [dbo].[v_PersonJob]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_PersonJob]'))
DROP VIEW [dbo].[v_PersonJob]
GO
/****** Object:  UserDefinedFunction [dbo].[GetPersonRelations]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPersonRelations]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetPersonRelations]
GO
/****** Object:  UserDefinedFunction [dbo].[GetContinents]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetContinents]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetContinents]
GO
/****** Object:  UserDefinedFunction [dbo].[GetPersonByContinent]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPersonByContinent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetPersonByContinent]
GO
/****** Object:  View [dbo].[v_Person]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_Person]'))
DROP VIEW [dbo].[v_Person]
GO
/****** Object:  Table [dbo].[TempData]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempData]') AND type in (N'U'))
DROP TABLE [dbo].[TempData]
GO
/****** Object:  Table [dbo].[RelationType]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RelationType]') AND type in (N'U'))
DROP TABLE [dbo].[RelationType]
GO
/****** Object:  Table [dbo].[PetType]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PetType]') AND type in (N'U'))
DROP TABLE [dbo].[PetType]
GO
/****** Object:  Table [dbo].[Pet]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pet]') AND type in (N'U'))
DROP TABLE [dbo].[Pet]
GO
/****** Object:  Table [dbo].[PersonRelation]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonRelation]') AND type in (N'U'))
DROP TABLE [dbo].[PersonRelation]
GO
/****** Object:  Table [dbo].[PersonNote]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonNote]') AND type in (N'U'))
DROP TABLE [dbo].[PersonNote]
GO
/****** Object:  Table [dbo].[PersonJob]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonJob]') AND type in (N'U'))
DROP TABLE [dbo].[PersonJob]
GO
/****** Object:  Table [dbo].[PersonExtra]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonExtra]') AND type in (N'U'))
DROP TABLE [dbo].[PersonExtra]
GO
/****** Object:  Table [dbo].[PersonContact]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonContact]') AND type in (N'U'))
DROP TABLE [dbo].[PersonContact]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
DROP TABLE [dbo].[Person]
GO
/****** Object:  Table [dbo].[Job]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Job]') AND type in (N'U'))
DROP TABLE [dbo].[Job]
GO
/****** Object:  Table [dbo].[Industry]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Industry]') AND type in (N'U'))
DROP TABLE [dbo].[Industry]
GO
/****** Object:  Table [dbo].[EmploymentStatus]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmploymentStatus]') AND type in (N'U'))
DROP TABLE [dbo].[EmploymentStatus]
GO
/****** Object:  Table [dbo].[CountryContinent]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryContinent]') AND type in (N'U'))
DROP TABLE [dbo].[CountryContinent]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
DROP TABLE [dbo].[Country]
GO
/****** Object:  Table [dbo].[Continent]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Continent]') AND type in (N'U'))
DROP TABLE [dbo].[Continent]
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactType]') AND type in (N'U'))
DROP TABLE [dbo].[ContactType]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
DROP TABLE [dbo].[Company]
GO
/****** Object:  Table [dbo].[City]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
DROP TABLE [dbo].[City]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
DROP TABLE [dbo].[Address]
GO
/****** Object:  UserDefinedFunction [dbo].[GetName]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetName]
GO
/****** Object:  StoredProcedure [dbo].[SelectCountry]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectCountry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SelectCountry]
GO
/****** Object:  StoredProcedure [dbo].[SelectCountries]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectCountries]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SelectCountries]
GO
/****** Object:  StoredProcedure [dbo].[SelectContinentsByTVP]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectContinentsByTVP]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SelectContinentsByTVP]
GO
/****** Object:  UserDefinedTableType [dbo].[utt_Continent]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'utt_Continent' AND ss.name = N'dbo')
DROP TYPE [dbo].[utt_Continent]
GO
/****** Object:  UserDefinedDataType [dbo].[udt_Char16]    Script Date: 20. 10. 2016 13:00:53 ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'udt_Char16' AND ss.name = N'dbo')
DROP TYPE [dbo].[udt_Char16]
GO

/****** Object:  UserDefinedDataType [dbo].[udt_Char16]    Script Date: 20. 10. 2016 13:00:53 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'udt_Char16' AND ss.name = N'dbo')
CREATE TYPE [dbo].[udt_Char16] FROM [char](16) NULL
GO
/****** Object:  UserDefinedTableType [dbo].[utt_Continent]    Script Date: 20. 10. 2016 13:00:53 ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'utt_Continent' AND ss.name = N'dbo')
CREATE TYPE [dbo].[utt_Continent] AS TABLE(
	[ContinentID] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ContinentID] ASC
)WITH (IGNORE_DUP_KEY = OFF)
)
GO
/****** Object:  StoredProcedure [dbo].[SelectContinentsByTVP]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectContinentsByTVP]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SelectContinentsByTVP]
	@Continents AS [dbo].[utt_Continent] READONLY
AS

SELECT * FROM @Continents;


' 
END
GO
/****** Object:  StoredProcedure [dbo].[SelectCountries]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectCountries]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SelectCountries]
AS

SELECT Name AS Country FROM dbo.Country;


' 
END
GO
/****** Object:  StoredProcedure [dbo].[SelectCountry]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SelectCountry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'

CREATE PROCEDURE [dbo].[SelectCountry]
	@CountryID int,
	@Output int OUTPUT
AS

SELECT CountryID, Name, TIS_A, Area
FROM dbo.Country
WHERE CountryID = @CountryID;

SET @Output = 499;

RETURN @CountryID;


' 
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetName]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetName]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetName](@FirstName nvarchar(50), @LastName nvarchar(50))
RETURNS nvarchar(101)
BEGIN

RETURN @FirstName + '' '' + @LastName;

END;
' 
END

GO
/****** Object:  Table [dbo].[Address]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Address](
	[AddressID] [int] NOT NULL,
	[Description] [nvarchar](150) NOT NULL,
	[CityID] [int] NOT NULL,
	[PostCode] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[City]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[City]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[City](
	[CityID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CountryID] [int] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Company]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Company]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Company](
	[CompanyID] [int] NOT NULL,
	[ParentCompanyID] [int] NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AddressID] [int] NOT NULL,
	[IndustryCode] [int] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ContactType]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ContactType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ContactType](
	[ContactTypeCode] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ContactType] PRIMARY KEY CLUSTERED 
(
	[ContactTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Continent]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Continent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Continent](
	[ContinentID] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Continent] PRIMARY KEY CLUSTERED 
(
	[ContinentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Country]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country](
	[CountryID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[TIS_A] [varchar](3) NOT NULL,
	[Area] [int] NOT NULL,
 CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CountryContinent]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryContinent]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CountryContinent](
	[CountryID] [int] NOT NULL,
	[ContinentID] [int] NOT NULL,
 CONSTRAINT [PK_CountryContinent] PRIMARY KEY CLUSTERED 
(
	[CountryID] ASC,
	[ContinentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[EmploymentStatus]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmploymentStatus]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EmploymentStatus](
	[EmploymentStatusCode] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[N] [int] NULL,
 CONSTRAINT [PK_EmploymentStatus] PRIMARY KEY CLUSTERED 
(
	[EmploymentStatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Industry]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Industry]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Industry](
	[IndustryCode] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Industry] PRIMARY KEY CLUSTERED 
(
	[IndustryCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Job]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Job]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Job](
	[JobID] [int] NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[CompanyID] [int] NOT NULL,
 CONSTRAINT [PK_Job] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Person]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Person](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[Timestamp] [timestamp] NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[AddressID] [int] NOT NULL,
	[DateOfBirth] [datetime] NOT NULL,
	[CountryOfBirthID] [int] NOT NULL,
	[Sex] [char](1) NOT NULL,
	[Age]  AS (datediff(year,[DateOfBirth],'2015-12-31')),
	[EmploymentStatusCode] [int] NOT NULL,
	[FatherID] [int] NULL,
	[MotherID] [int] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PersonContact]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonContact]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PersonContact](
	[PersonContactID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[ContactTypeCode] [int] NOT NULL,
	[Contact] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_PersonContact] PRIMARY KEY CLUSTERED 
(
	[PersonContactID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PersonExtra]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonExtra]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PersonExtra](
	[PersonID] [int] NOT NULL,
	[Extra] [nvarchar](1000) NOT NULL CONSTRAINT [DF_PersonExtra_Extra]  DEFAULT (N'This is a DEFAULT extra information about the person.'),
 CONSTRAINT [PK_PersonExtra] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PersonJob]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonJob]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PersonJob](
	[PersonJobID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[JobID] [int] NOT NULL,
	[DateFrom] [datetime] NOT NULL,
	[DateTo] [datetime] NULL,
	[IsCurrent]  AS (CONVERT([bit],case when [DateTo] IS NULL then (1) else (0) end)),
 CONSTRAINT [PK_PersonJob] PRIMARY KEY CLUSTERED 
(
	[PersonJobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PersonNote]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonNote]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PersonNote](
	[PersonNoteID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[Note] [nvarchar](1000) NOT NULL,
 CONSTRAINT [PK_PersonNote] PRIMARY KEY CLUSTERED 
(
	[PersonNoteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PersonRelation]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PersonRelation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PersonRelation](
	[PersonID] [int] NOT NULL,
	[RelatedPersonID] [int] NOT NULL,
	[RelationTypeCode] [int] NOT NULL,
 CONSTRAINT [PK_PersonRelation] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC,
	[RelatedPersonID] ASC,
	[RelationTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Pet]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pet]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Pet](
	[PetID] [int] NOT NULL,
	[PetTypeCode] [int] NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[OwnerFirstName] [nvarchar](50) NULL,
	[OwnerLastName] [nvarchar](50) NOT NULL,
	[OwnerCountryOfBirth] [int] NOT NULL,
	[OwnerDateOfBirth] [datetime] NOT NULL,
	[FirstOwnerID] [int] NULL,
 CONSTRAINT [PK_Pet] PRIMARY KEY CLUSTERED 
(
	[PetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[PetType]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PetType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[PetType](
	[PetTypeCode] [int] NOT NULL,
	[Description] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_PetType] PRIMARY KEY CLUSTERED 
(
	[PetTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RelationType]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RelationType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RelationType](
	[RelationTypeCode] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_RelationType] PRIMARY KEY CLUSTERED 
(
	[RelationTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[TempData]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TempData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TempData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Data] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__TempData__3214EC27F12E896A] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  View [dbo].[v_Person]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_Person]'))
EXEC dbo.sp_executesql @statement = N'



CREATE VIEW [dbo].[v_Person]
AS

SELECT 
	A.[PersonID]
	, dbo.GetName(A.[FirstName], A.[LastName]) AS [Name]
	, D1.[Name] AS CountryOfBirth
	, B.[Description] AS [Address] 
	, C.[Name] AS [City]
	, D2.[Name] AS [Country]
	, A.[DateOfBirth]
	, A.[Sex]
	, A.[Age]
	, E.[Description] AS EmploymentStatus
	, A.[AddressID]
FROM [dbo].[Person] AS A
INNER JOIN [dbo].[Address] AS B ON A.AddressID = B.AddressID
INNER JOIN [dbo].[City] AS C ON B.CityID = C.CityID
INNER JOIN [dbo].[Country] AS D1 ON A.CountryOfBirthID = D1.CountryID
INNER JOIN [dbo].[Country] AS D2 ON C.CountryID = D2.CountryID
INNER JOIN [dbo].[EmploymentStatus] AS E ON A.EmploymentStatusCode = E.EmploymentStatusCode 





' 
GO
/****** Object:  UserDefinedFunction [dbo].[GetPersonByContinent]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPersonByContinent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetPersonByContinent](@Continent nvarchar(20))
RETURNS TABLE
AS RETURN
(
	SELECT A.[PersonID], A.[Name], A.[Country]
	FROM [dbo].[v_Person] AS A
	INNER JOIN [dbo].[Person] AS B ON A.[PersonID] = B.[PersonID]
	INNER JOIN [dbo].[Address] AS C ON B.[AddressID] = C.[AddressID]
	INNER JOIN [dbo].[City] AS D ON C.[CityID] = D.[CityID]
	INNER JOIN [dbo].[Country] AS E ON D.[CountryID] = E.[CountryID]
	INNER JOIN [dbo].[CountryContinent] AS F ON E.[CountryID] = F.[CountryID]
	INNER JOIN [dbo].[Continent] AS G ON F.[ContinentID] = G.[ContinentID]
	WHERE G.Name = @Continent
)

' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetContinents]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetContinents]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetContinents]()
RETURNS TABLE
AS RETURN
(
	SELECT * FROM dbo.Continent
)

' 
END

GO
/****** Object:  UserDefinedFunction [dbo].[GetPersonRelations]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPersonRelations]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
CREATE FUNCTION [dbo].[GetPersonRelations](@PersonID int)
RETURNS TABLE
AS RETURN
(
	SELECT MIN(B.[Description]) AS RelationType, COUNT(*) AS N
	FROM [dbo].[PersonRelation] AS A
	INNER JOIN [dbo].[RelationType] AS B ON A.RelationTypeCode = B.RelationTypeCode
	WHERE A.[PersonID] = @PersonID
	GROUP BY A.[RelationTypeCode]
)

' 
END

GO
/****** Object:  View [dbo].[v_PersonJob]    Script Date: 20. 10. 2016 13:00:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_PersonJob]'))
EXEC dbo.sp_executesql @statement = N'



CREATE VIEW [dbo].[v_PersonJob]
AS

SELECT 
	B.[PersonJobID]
	, A.[PersonID]
	, C.[JobID]
	, dbo.GetName(A.[FirstName], A.[LastName]) AS [Name]
	, ISNULL(C.Title, ''N/A'') AS Job
	, ISNULL(D.Name, ''N/A'') AS Company
	, B.DateTo
FROM [dbo].[Person] AS A
LEFT OUTER JOIN [dbo].[PersonJob] AS B ON A.PersonID = B.PersonID
LEFT OUTER JOIN [dbo].[Job] AS C ON B.JobID = C.JobID
LEFT OUTER JOIN [dbo].[Company] AS D ON C.CompanyID = D.CompanyID





' 
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (1, N'112 Wen Hui Yuan Lu', 1, N'100082')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (2, N'Sudostroitelnaya ulitsa, 33', 2, N'115407')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (3, N'Calle Talavera, 6', 3, N'28016')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (4, N'Calle de Albasanz, 21', 3, N'28037')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (5, N'227 Sullivan St', 4, N'10012')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (6, N'222 Rue de Belleville', 5, N'75020')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (7, N'5 Chome-17-4, Higashigotanda, Shinagawa-ku', 6, N'141-0022')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (8, N'160 Merrow St', 7, N'SE17 2NP')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (9, N'Jl. Menteng Atas No.16, Kec. Setiabudi, Kota Jkt Sel., Daerah Khusus', 8, N'12960')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (10, N'57 Mowowale Street', 9, N'100233')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (11, N'18 Fuxingmennei Street, Xicheng District', 1, N'100031')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (12, N'1 Fuxingmen Nei Dajie', 1, N'100818')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (13, N'Sushchevskaya, 21, оф. 537	', 2, N'109004')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (14, N'1 Brestskaya, 15', 2, N'125047')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (15, N'55 E 52nd St', 4, N'10022')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (16, N'Calle de Sagasta, 33', 3, N'28004')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (17, N'Rotherstraße 11', 10, N'10245')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (18, N'49 Avenue Georges Pompidou, Levallois-Perret', 5, N'92300')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (19, N'75 Rue de Rivoli', 5, N'75001')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (20, N'Dilshad Garden', 11, N'110095')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (21, N'Lajpat Nagar III, Near Moolchand Metro Station', 11, N'110024')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (22, N'60 Great Ormond Street', 7, N'WC1N 3HR')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (23, N'11th and 16th Floor, Raghuleela Arcade, IT park, Sector 30 A, Opp. Vashi Railway Station, Vashi', 12, N'400703')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (24, N'Wisma Tamara Suite 101-201 Jl. Jend. Sudirman Kav. 24', 8, N'12920')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (25, N'Lateef Jakande Rd', 9, N'100230')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (26, N'1-11 Commercial Ave', 9, N'101212')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (27, N'72 Adeniyi Jones Ave', 9, N'101592')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (28, N'3 Obanta Rd', 9, N'100501')
GO
INSERT [dbo].[Address] ([AddressID], [Description], [CityID], [PostCode]) VALUES (29, N'Av Francisco Sosa 59, Santa Catarina', 17, N'04100')
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (1, N'Beijing', 2)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (2, N'Moscow', 9)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (3, N'Madrid', 10)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (4, N'New York', 12)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (5, N'Paris', 3)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (6, N'Tokyo', 7)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (7, N'London', 11)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (8, N'Jakarta', 6)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (9, N'Lagos', 8)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (10, N'Berlin', 4)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (11, N'New Delhi', 5)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (12, N'Navi Mumbai', 5)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (13, N'Los Angeles', 12)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (14, N'Saint Petersburg', 9)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (15, N'Hong Kong', 2)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (16, N'Shanghai', 2)
GO
INSERT [dbo].[City] ([CityID], [Name], [CountryID]) VALUES (17, N'Mexico City', 13)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (1, NULL, N'China Development Bank', 11, 1)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (2, NULL, N'Bank of China', 12, 1)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (3, NULL, N'Otkrytie', 13, 2)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (4, NULL, N'Central Travel Agency', 14, 2)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (5, NULL, N'McKinsey & Company', 15, 3)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (6, 5, N'McKinsey Spain', 16, 3)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (7, NULL, N'BASF', 17, 4)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (8, 7, N'BASF France', 18, 4)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (9, NULL, N'Sephora', 19, 5)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (10, NULL, N'Susumu Yukimura', 7, 6)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (11, NULL, N'Guru Teg Bahadur Hospital', 20, 10)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (12, NULL, N'Moolchand Medcity', 21, 10)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (13, NULL, N'Royal London Hospital for Integrated Medicine', 22, 10)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (14, NULL, N'Star Union Dai-ichi Life Insurance Company Limited', 23, 11)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (15, NULL, N'Bank of China, Jakarta', 24, 1)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (16, NULL, N'Cadbury Nigeria Plc', 1, 5)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (17, NULL, N'Lamudi Nigeria
', 26, 12)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (18, NULL, N'Bolar Pharmaceuticals Nigeria Limited', 26, 13)
GO
INSERT [dbo].[Company] ([CompanyID], [ParentCompanyID], [Name], [AddressID], [IndustryCode]) VALUES (19, NULL, N'The Park Private School', 28, 7)
GO
INSERT [dbo].[ContactType] ([ContactTypeCode], [Description]) VALUES (1, N'Phone')
GO
INSERT [dbo].[ContactType] ([ContactTypeCode], [Description]) VALUES (2, N'Email')
GO
INSERT [dbo].[Continent] ([ContinentID], [Name]) VALUES (1, N'AFRICA')
GO
INSERT [dbo].[Continent] ([ContinentID], [Name]) VALUES (2, N'ANTARCTICA')
GO
INSERT [dbo].[Continent] ([ContinentID], [Name]) VALUES (3, N'ASIA')
GO
INSERT [dbo].[Continent] ([ContinentID], [Name]) VALUES (4, N'AUSTRALIA')
GO
INSERT [dbo].[Continent] ([ContinentID], [Name]) VALUES (5, N'EUROPE')
GO
INSERT [dbo].[Continent] ([ContinentID], [Name]) VALUES (6, N'NORTH AMERICA')
GO
INSERT [dbo].[Continent] ([ContinentID], [Name]) VALUES (7, N'SOUTH AMERICA')
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (1, N'Argentina', N'ARG', 2780400)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (2, N'China', N'CN', 9596961)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (3, N'France', N'FR', 640679)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (4, N'Germany', N'DE', 357168)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (5, N'India', N'IN', 3287590)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (6, N'Indonesia', N'ID', 1904569)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (7, N'Japan', N'JP', 377944)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (8, N'Nigeria', N'NG', 923768)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (9, N'Russian Federation', N'RU', 17098242)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (10, N'Spain', N'ES', 505990)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (11, N'United Kingdom', N'GB', 243610)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (12, N'United States', N'US', 9826675)
GO
INSERT [dbo].[Country] ([CountryID], [Name], [TIS_A], [Area]) VALUES (13, N'Mexico', N'MX', 1964375)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (1, 7)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (2, 3)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (3, 5)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (4, 5)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (5, 3)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (6, 3)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (7, 3)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (8, 1)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (9, 3)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (9, 5)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (10, 5)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (11, 5)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (12, 6)
GO
INSERT [dbo].[CountryContinent] ([CountryID], [ContinentID]) VALUES (13, 6)
GO
INSERT [dbo].[EmploymentStatus] ([EmploymentStatusCode], [Description], [N]) VALUES (1, N'Employed', NULL)
GO
INSERT [dbo].[EmploymentStatus] ([EmploymentStatusCode], [Description], [N]) VALUES (2, N'Self-employed', NULL)
GO
INSERT [dbo].[EmploymentStatus] ([EmploymentStatusCode], [Description], [N]) VALUES (3, N'Not employed', NULL)
GO
INSERT [dbo].[EmploymentStatus] ([EmploymentStatusCode], [Description], [N]) VALUES (4, N'Retired', NULL)
GO
INSERT [dbo].[EmploymentStatus] ([EmploymentStatusCode], [Description], [N]) VALUES (5, N'Other', NULL)
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (1, N'Banking')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (2, N'Entertainment & Leisure')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (3, N'Consulting')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (4, N'Chemical')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (5, N'Retail & Wholesale')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (6, N'Software')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (7, N'Education')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (8, N'Music')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (9, N'Publishing')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (10, N'Health Care')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (11, N'Insurance')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (12, N'Real Estate')
GO
INSERT [dbo].[Industry] ([IndustryCode], [Name]) VALUES (13, N'Pharmaceuticals')
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (1, N'Online Customer Service Representative', 1)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (2, N'Consumer Finance Assistant Manager', 1)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (3, N'Lending Manager', 1)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (4, N'Lead Personal Banker', 2)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (5, N'Travel Agent', 3)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (6, N'Travel Agent', 4)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (7, N'Junior Research Analyst', 5)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (8, N'Research Analyst', 5)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (9, N'Risk Advanced Analyst', 5)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (10, N'Senior Research Analyst', 5)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (11, N'TMT- Research Manager', 5)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (12, N'Technical Service Specialist', 7)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (13, N'Technical Consultant', 8)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (14, N'Make Up Advisor', 9)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (15, N'Web Developer and IT Consultant', 10)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (16, N'Pulmonologist', 11)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (17, N'Pulmonologist', 12)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (18, N'Pulmonologist', 13)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (19, N'Life Insurance Sales Agent', 14)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (20, N'Personal Banker', 15)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (21, N'Distributor Manager', 16)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (22, N'Estate Surveyor', 17)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (23, N'Pharmaceutical Sales Representative', 18)
GO
INSERT [dbo].[Job] ([JobID], [Title], [CompanyID]) VALUES (24, N'English Teacher', 19)
GO
SET IDENTITY_INSERT [dbo].[Person] ON 

GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (1, N'Chen', N'Li', 1, CAST(N'1975-06-19T00:00:00.000' AS DateTime), 2, N'M', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (2, N'Tatiana', N'Gorokhina', 2, CAST(N'1980-05-28T00:00:00.000' AS DateTime), 9, N'F', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (3, N'Juan', N'Maldonado', 3, CAST(N'1987-07-16T00:00:00.000' AS DateTime), 1, N'M', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (4, N'Blanca', N'Suárez Velázquez', 4, CAST(N'1989-03-30T00:00:00.000' AS DateTime), 10, N'F', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (5, N'Betsy', N'Puckett', 5, CAST(N'1962-02-01T00:00:00.000' AS DateTime), 12, N'F', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (6, N'Matthias', N'Hellewege', 6, CAST(N'1970-12-23T00:00:00.000' AS DateTime), 4, N'M', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (7, N'Monique', N'Hellewege', 6, CAST(N'1977-04-20T00:00:00.000' AS DateTime), 3, N'F', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (8, N'Giselle', N'Hellewege', 6, CAST(N'2008-06-15T00:00:00.000' AS DateTime), 3, N'F', 5, 6, 7)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (9, N'Masami', N'Yukimura', 7, CAST(N'1996-09-10T00:00:00.000' AS DateTime), 7, N'F', 3, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (10, N'Susumu', N'Yukimura', 7, CAST(N'1992-08-02T00:00:00.000' AS DateTime), 7, N'M', 2, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (11, N'Jaywant', N'Wadhwa', 8, CAST(N'1967-11-08T00:00:00.000' AS DateTime), 5, N'M', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (12, N'Krisna', N'Suwandi', 9, CAST(N'1983-12-17T00:00:00.000' AS DateTime), 6, N'M', 1, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (13, N'Tombari', N'Okeke', 10, CAST(N'1948-03-29T00:00:00.000' AS DateTime), 8, N'M', 4, NULL, NULL)
GO
INSERT [dbo].[Person] ([PersonID], [FirstName], [LastName], [AddressID], [DateOfBirth], [CountryOfBirthID], [Sex], [EmploymentStatusCode], [FatherID], [MotherID]) VALUES (14, N'Anette', N'Hellewege', 6, CAST(N'2014-04-11T00:00:00.000' AS DateTime), 13, N'F', 5, 6, 7)
GO
SET IDENTITY_INSERT [dbo].[Person] OFF
GO
SET IDENTITY_INSERT [dbo].[PersonContact] ON 

GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (1, 1, 1, N'86-10-6449 0000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (2, 1, 2, N'chen.li@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (3, 2, 1, N'812 555-0000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (4, 2, 2, N'tatiana.gorokhina@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (5, 3, 1, N'630 349 000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (6, 3, 2, N'juan.maldonado@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (7, 4, 1, N'609 723 000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (8, 4, 2, N'blanca.velazquez@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (9, 5, 1, N'716-621-0000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (10, 5, 2, N'betsy.puckett@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (11, 6, 1, N'1 84 89 00 00')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (12, 6, 2, N'matthias.hellewege@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (13, 7, 1, N'1 83 75 00 00')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (14, 7, 2, N'monique.hellewege@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (15, 9, 1, N'090-1782-0000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (16, 9, 2, N'masami.yukimura@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (17, 10, 1, N'090-2831-0000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (18, 10, 2, N'susumu.yukimura@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (19, 11, 1, N'070 1283 0000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (20, 11, 2, N'jaywant.wadhwa@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (21, 12, 1, N'021 3514 0000')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (22, 12, 2, N'krisna.suwandi@email.com')
GO
INSERT [dbo].[PersonContact] ([PersonContactID], [PersonID], [ContactTypeCode], [Contact]) VALUES (23, 13, 1, N'0704 716 0000')
GO
SET IDENTITY_INSERT [dbo].[PersonContact] OFF
GO
INSERT [dbo].[PersonExtra] ([PersonID], [Extra]) VALUES (1, N'This is an additional information about the person.')
GO
SET IDENTITY_INSERT [dbo].[PersonJob] ON 

GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (1, 1, 1, CAST(N'2000-06-01T00:00:00.000' AS DateTime), CAST(N'2004-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (2, 1, 2, CAST(N'2004-01-01T00:00:00.000' AS DateTime), CAST(N'2007-10-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (3, 1, 3, CAST(N'2007-10-01T00:00:00.000' AS DateTime), CAST(N'2012-04-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (4, 1, 4, CAST(N'2012-04-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (5, 2, 5, CAST(N'2003-07-01T00:00:00.000' AS DateTime), CAST(N'2009-05-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (6, 2, 6, CAST(N'2009-05-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (7, 3, 7, CAST(N'2010-03-01T00:00:00.000' AS DateTime), CAST(N'2009-05-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (8, 3, 8, CAST(N'2009-05-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (9, 4, 7, CAST(N'2011-12-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (10, 5, 7, CAST(N'1990-01-01T00:00:00.000' AS DateTime), CAST(N'1995-08-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (11, 5, 8, CAST(N'1995-08-01T00:00:00.000' AS DateTime), CAST(N'2002-02-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (12, 5, 9, CAST(N'2002-02-01T00:00:00.000' AS DateTime), CAST(N'2007-07-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (13, 5, 10, CAST(N'2007-07-01T00:00:00.000' AS DateTime), CAST(N'2013-10-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (14, 5, 11, CAST(N'2013-10-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (15, 6, 12, CAST(N'2005-01-01T00:00:00.000' AS DateTime), CAST(N'2010-01-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (16, 6, 13, CAST(N'2010-01-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (17, 7, 14, CAST(N'2009-07-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (18, 10, 15, CAST(N'2010-08-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (19, 11, 19, CAST(N'1991-08-01T00:00:00.000' AS DateTime), CAST(N'1994-04-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (20, 11, 16, CAST(N'1997-03-01T00:00:00.000' AS DateTime), CAST(N'2007-09-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (21, 11, 17, CAST(N'2007-09-01T00:00:00.000' AS DateTime), CAST(N'2015-05-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (22, 11, 18, CAST(N'2015-05-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (23, 12, 20, CAST(N'2011-12-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (24, 13, 24, CAST(N'1966-09-01T00:00:00.000' AS DateTime), CAST(N'1969-09-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (25, 13, 21, CAST(N'1969-09-01T00:00:00.000' AS DateTime), CAST(N'1980-02-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (26, 13, 22, CAST(N'1982-11-01T00:00:00.000' AS DateTime), CAST(N'1997-06-01T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[PersonJob] ([PersonJobID], [PersonID], [JobID], [DateFrom], [DateTo]) VALUES (27, 13, 23, CAST(N'1997-06-01T00:00:00.000' AS DateTime), CAST(N'2013-06-01T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[PersonJob] OFF
GO
SET IDENTITY_INSERT [dbo].[PersonNote] ON 

GO
INSERT [dbo].[PersonNote] ([PersonNoteID], [PersonID], [Note]) VALUES (1, 9, N'Masami Yukimura is a student.')
GO
INSERT [dbo].[PersonNote] ([PersonNoteID], [PersonID], [Note]) VALUES (2, 11, N'No information about the professional activity between 1994-04-01 and 1997-03-01.')
GO
INSERT [dbo].[PersonNote] ([PersonNoteID], [PersonID], [Note]) VALUES (3, 13, N'No information about the professional activity between 1980-02-01 and 1982-11-01.')
GO
INSERT [dbo].[PersonNote] ([PersonNoteID], [PersonID], [Note]) VALUES (1008, 14, N'She was a premature baby.')
GO
SET IDENTITY_INSERT [dbo].[PersonNote] OFF
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (1, 2, 1)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (1, 10, 1)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (1, 12, 13)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (2, 1, 1)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (2, 9, 1)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (3, 4, 2)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (3, 4, 12)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (3, 5, 14)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (4, 3, 3)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (4, 3, 12)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (4, 5, 14)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (5, 3, 13)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (5, 4, 13)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (5, 11, 13)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (6, 7, 4)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (6, 8, 6)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (6, 14, 6)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (7, 6, 5)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (7, 8, 7)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (7, 14, 7)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (8, 6, 9)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (8, 7, 9)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (8, 14, 11)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (9, 2, 1)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (9, 10, 11)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (10, 1, 1)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (10, 9, 10)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (11, 5, 14)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (12, 1, 14)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (14, 6, 9)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (14, 7, 9)
GO
INSERT [dbo].[PersonRelation] ([PersonID], [RelatedPersonID], [RelationTypeCode]) VALUES (14, 8, 11)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (1, 1, N'Kogo', N'Masami', N'Yukimura', 7, CAST(N'1996-09-10T00:00:00.000' AS DateTime), 10)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (2, 2, N'Alice', N'Monique', N'Hellewege', 3, CAST(N'1977-04-20T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (3, 5, N'Elsa', N'Giselle', N'Hellewege', 3, CAST(N'2008-06-15T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (4, 1, N'Hector', N'Betsy', N'Puckett', 12, CAST(N'1962-02-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (5, 2, N'Lara', N'Tatiana', N'Gorokhina', 9, CAST(N'1980-05-28T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (6, 4, N'Jacinto', N'Juan', N'Maldonado', 1, CAST(N'1987-07-16T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (7, 4, N'Azul', N'Juan', N'Maldonado', 1, CAST(N'1987-07-16T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (8, 3, N'Who', N'Betsy', N'Puckett', 12, CAST(N'1962-02-01T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[Pet] ([PetID], [PetTypeCode], [Name], [OwnerFirstName], [OwnerLastName], [OwnerCountryOfBirth], [OwnerDateOfBirth], [FirstOwnerID]) VALUES (9, 1, N'Maya', N'Susumu', N'Yukimura', 7, CAST(N'1992-08-02T00:00:00.000' AS DateTime), NULL)
GO
INSERT [dbo].[PetType] ([PetTypeCode], [Description]) VALUES (1, N'Dog')
GO
INSERT [dbo].[PetType] ([PetTypeCode], [Description]) VALUES (2, N'Cat')
GO
INSERT [dbo].[PetType] ([PetTypeCode], [Description]) VALUES (3, N'Bird')
GO
INSERT [dbo].[PetType] ([PetTypeCode], [Description]) VALUES (4, N'Reptile')
GO
INSERT [dbo].[PetType] ([PetTypeCode], [Description]) VALUES (5, N'Fish')
GO
INSERT [dbo].[PetType] ([PetTypeCode], [Description]) VALUES (6, N'Other')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (1, N'Friend')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (2, N'Boyfriend')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (3, N'Girlfriend')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (4, N'Husband')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (5, N'Wife')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (6, N'Father')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (7, N'Mother')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (8, N'Son')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (9, N'Daughter')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (10, N'Brother')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (11, N'Sister')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (12, N'Co-worker')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (13, N'Boss')
GO
INSERT [dbo].[RelationType] ([RelationTypeCode], [Description]) VALUES (14, N'Subordinate')
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Address]    Script Date: 20. 10. 2016 13:00:53 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Address]') AND name = N'UK_Address')
ALTER TABLE [dbo].[Address] ADD  CONSTRAINT [UK_Address] UNIQUE NONCLUSTERED 
(
	[Description] ASC,
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UK_Person]    Script Date: 20. 10. 2016 13:00:53 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND name = N'UK_Person')
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [UK_Person] UNIQUE NONCLUSTERED 
(
	[FirstName] ASC,
	[LastName] ASC,
	[DateOfBirth] ASC,
	[CountryOfBirthID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_CityID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_CityID] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([CityID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Address_CityID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Address]'))
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_CityID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_City_CountryID]') AND parent_object_id = OBJECT_ID(N'[dbo].[City]'))
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_CountryID] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([CountryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_City_CountryID]') AND parent_object_id = OBJECT_ID(N'[dbo].[City]'))
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_CountryID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_AddressID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_AddressID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_AddressID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_IndustryCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_IndustryCode] FOREIGN KEY([IndustryCode])
REFERENCES [dbo].[Industry] ([IndustryCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_IndustryCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_IndustryCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_ParentCompanyID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_ParentCompanyID] FOREIGN KEY([ParentCompanyID])
REFERENCES [dbo].[Company] ([CompanyID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Company_ParentCompanyID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Company]'))
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_ParentCompanyID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CountryContinent_ContinentID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryContinent]'))
ALTER TABLE [dbo].[CountryContinent]  WITH CHECK ADD  CONSTRAINT [FK_CountryContinent_ContinentID] FOREIGN KEY([ContinentID])
REFERENCES [dbo].[Continent] ([ContinentID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CountryContinent_ContinentID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryContinent]'))
ALTER TABLE [dbo].[CountryContinent] CHECK CONSTRAINT [FK_CountryContinent_ContinentID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CountryContinent_CountryID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryContinent]'))
ALTER TABLE [dbo].[CountryContinent]  WITH CHECK ADD  CONSTRAINT [FK_CountryContinent_CountryID] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Country] ([CountryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CountryContinent_CountryID]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryContinent]'))
ALTER TABLE [dbo].[CountryContinent] CHECK CONSTRAINT [FK_CountryContinent_CountryID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Job_CompanyID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Job]'))
ALTER TABLE [dbo].[Job]  WITH CHECK ADD  CONSTRAINT [FK_Job_CompanyID] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Company] ([CompanyID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Job_CompanyID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Job]'))
ALTER TABLE [dbo].[Job] CHECK CONSTRAINT [FK_Job_CompanyID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_AddressID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_AddressID] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_AddressID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_AddressID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_CountryOfBirthID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_CountryOfBirthID] FOREIGN KEY([CountryOfBirthID])
REFERENCES [dbo].[Country] ([CountryID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_CountryOfBirthID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_CountryOfBirthID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_EmploymentStatusCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_EmploymentStatusCode] FOREIGN KEY([EmploymentStatusCode])
REFERENCES [dbo].[EmploymentStatus] ([EmploymentStatusCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_EmploymentStatusCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_EmploymentStatusCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_FatherID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_FatherID] FOREIGN KEY([FatherID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_FatherID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_FatherID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_MotherID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [FK_Person_MotherID] FOREIGN KEY([MotherID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Person_MotherID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [FK_Person_MotherID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonContact_ContactTypeCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonContact]'))
ALTER TABLE [dbo].[PersonContact]  WITH CHECK ADD  CONSTRAINT [FK_PersonContact_ContactTypeCode] FOREIGN KEY([ContactTypeCode])
REFERENCES [dbo].[ContactType] ([ContactTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonContact_ContactTypeCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonContact]'))
ALTER TABLE [dbo].[PersonContact] CHECK CONSTRAINT [FK_PersonContact_ContactTypeCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonContact_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonContact]'))
ALTER TABLE [dbo].[PersonContact]  WITH CHECK ADD  CONSTRAINT [FK_PersonContact_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonContact_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonContact]'))
ALTER TABLE [dbo].[PersonContact] CHECK CONSTRAINT [FK_PersonContact_PersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonExtra_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonExtra]'))
ALTER TABLE [dbo].[PersonExtra]  WITH CHECK ADD  CONSTRAINT [FK_PersonExtra_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonExtra_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonExtra]'))
ALTER TABLE [dbo].[PersonExtra] CHECK CONSTRAINT [FK_PersonExtra_PersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonJob_JobID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonJob]'))
ALTER TABLE [dbo].[PersonJob]  WITH CHECK ADD  CONSTRAINT [FK_PersonJob_JobID] FOREIGN KEY([JobID])
REFERENCES [dbo].[Job] ([JobID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonJob_JobID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonJob]'))
ALTER TABLE [dbo].[PersonJob] CHECK CONSTRAINT [FK_PersonJob_JobID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonJob_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonJob]'))
ALTER TABLE [dbo].[PersonJob]  WITH CHECK ADD  CONSTRAINT [FK_PersonJob_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonJob_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonJob]'))
ALTER TABLE [dbo].[PersonJob] CHECK CONSTRAINT [FK_PersonJob_PersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonNote_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonNote]'))
ALTER TABLE [dbo].[PersonNote]  WITH CHECK ADD  CONSTRAINT [FK_PersonNote_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonNote_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonNote]'))
ALTER TABLE [dbo].[PersonNote] CHECK CONSTRAINT [FK_PersonNote_PersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation]  WITH CHECK ADD  CONSTRAINT [FK_PersonRelation_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_PersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation] CHECK CONSTRAINT [FK_PersonRelation_PersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_RelatedPersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation]  WITH CHECK ADD  CONSTRAINT [FK_PersonRelation_RelatedPersonID] FOREIGN KEY([RelatedPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_RelatedPersonID]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation] CHECK CONSTRAINT [FK_PersonRelation_RelatedPersonID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_RelationTypeCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation]  WITH CHECK ADD  CONSTRAINT [FK_PersonRelation_RelationTypeCode] FOREIGN KEY([RelationTypeCode])
REFERENCES [dbo].[RelationType] ([RelationTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PersonRelation_RelationTypeCode]') AND parent_object_id = OBJECT_ID(N'[dbo].[PersonRelation]'))
ALTER TABLE [dbo].[PersonRelation] CHECK CONSTRAINT [FK_PersonRelation_RelationTypeCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_FirstOwner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [FK_Pet_FirstOwner] FOREIGN KEY([FirstOwnerID])
REFERENCES [dbo].[Person] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_FirstOwner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [FK_Pet_FirstOwner]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_Owner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [FK_Pet_Owner] FOREIGN KEY([OwnerFirstName], [OwnerLastName], [OwnerDateOfBirth], [OwnerCountryOfBirth])
REFERENCES [dbo].[Person] ([FirstName], [LastName], [DateOfBirth], [CountryOfBirthID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_Owner]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [FK_Pet_Owner]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_PetType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet]  WITH CHECK ADD  CONSTRAINT [FK_Pet_PetType] FOREIGN KEY([PetTypeCode])
REFERENCES [dbo].[PetType] ([PetTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Pet_PetType]') AND parent_object_id = OBJECT_ID(N'[dbo].[Pet]'))
ALTER TABLE [dbo].[Pet] CHECK CONSTRAINT [FK_Pet_PetType]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Person_Sex]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person]  WITH CHECK ADD  CONSTRAINT [CK_Person_Sex] CHECK  (([Sex]='F' OR [Sex]='M'))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Person_Sex]') AND parent_object_id = OBJECT_ID(N'[dbo].[Person]'))
ALTER TABLE [dbo].[Person] CHECK CONSTRAINT [CK_Person_Sex]
GO
