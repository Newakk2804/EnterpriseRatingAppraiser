
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/30/2023 02:26:11
-- Generated from EDMX file: E:\c#\копия диплома\EnterpriseRatingAppraiser\EF\EnterpriseRatingAppraiser.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [EnterpriseRatingAppraiserContext];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TypeOfEconomicActivityCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanySet] DROP CONSTRAINT [FK_TypeOfEconomicActivityCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_OwnershipFormCompany]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanySet] DROP CONSTRAINT [FK_OwnershipFormCompany];
GO
IF OBJECT_ID(N'[dbo].[FK_CompanyCompanyPerfomance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyPerfomanceSet] DROP CONSTRAINT [FK_CompanyCompanyPerfomance];
GO
IF OBJECT_ID(N'[dbo].[FK_CriterionCompanyPerfomance]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CompanyPerfomanceSet] DROP CONSTRAINT [FK_CriterionCompanyPerfomance];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CompanySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanySet];
GO
IF OBJECT_ID(N'[dbo].[CompanyPerfomanceSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CompanyPerfomanceSet];
GO
IF OBJECT_ID(N'[dbo].[CriterionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CriterionSet];
GO
IF OBJECT_ID(N'[dbo].[RatingSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RatingSet];
GO
IF OBJECT_ID(N'[dbo].[UserSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSet];
GO
IF OBJECT_ID(N'[dbo].[TypeOfEconomicActivitySet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TypeOfEconomicActivitySet];
GO
IF OBJECT_ID(N'[dbo].[OwnershipFormSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OwnershipFormSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CompanySet'
CREATE TABLE [dbo].[CompanySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameCompany] nvarchar(max)  NOT NULL,
    [DateOfFoundation] datetime2(7)  NOT NULL,
    [CompanyDescription] nvarchar(max)  NOT NULL,
    [IdTypeOfEconomicActivity] int  NOT NULL,
    [IdOwnershipForm] int  NOT NULL
);
GO

-- Creating table 'CompanyPerfomanceSet'
CREATE TABLE [dbo].[CompanyPerfomanceSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdCompany] int  NOT NULL,
    [IdCriterion] int  NOT NULL,
    [Month] nvarchar(max)  NOT NULL,
    [Year] int  NOT NULL,
    [Value] decimal(19,2)  NOT NULL
);
GO

-- Creating table 'CriterionSet'
CREATE TABLE [dbo].[CriterionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NameCriterion] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'RatingSet'
CREATE TABLE [dbo].[RatingSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ValueFrom] int  NOT NULL,
    [ValueTo] int  NOT NULL,
    [Value] decimal(19,1)  NOT NULL
);
GO

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TypeOfEconomicActivitySet'
CREATE TABLE [dbo].[TypeOfEconomicActivitySet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'OwnershipFormSet'
CREATE TABLE [dbo].[OwnershipFormSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CompanySet'
ALTER TABLE [dbo].[CompanySet]
ADD CONSTRAINT [PK_CompanySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CompanyPerfomanceSet'
ALTER TABLE [dbo].[CompanyPerfomanceSet]
ADD CONSTRAINT [PK_CompanyPerfomanceSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CriterionSet'
ALTER TABLE [dbo].[CriterionSet]
ADD CONSTRAINT [PK_CriterionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RatingSet'
ALTER TABLE [dbo].[RatingSet]
ADD CONSTRAINT [PK_RatingSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TypeOfEconomicActivitySet'
ALTER TABLE [dbo].[TypeOfEconomicActivitySet]
ADD CONSTRAINT [PK_TypeOfEconomicActivitySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OwnershipFormSet'
ALTER TABLE [dbo].[OwnershipFormSet]
ADD CONSTRAINT [PK_OwnershipFormSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdTypeOfEconomicActivity] in table 'CompanySet'
ALTER TABLE [dbo].[CompanySet]
ADD CONSTRAINT [FK_TypeOfEconomicActivityCompany]
    FOREIGN KEY ([IdTypeOfEconomicActivity])
    REFERENCES [dbo].[TypeOfEconomicActivitySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TypeOfEconomicActivityCompany'
CREATE INDEX [IX_FK_TypeOfEconomicActivityCompany]
ON [dbo].[CompanySet]
    ([IdTypeOfEconomicActivity]);
GO

-- Creating foreign key on [IdOwnershipForm] in table 'CompanySet'
ALTER TABLE [dbo].[CompanySet]
ADD CONSTRAINT [FK_OwnershipFormCompany]
    FOREIGN KEY ([IdOwnershipForm])
    REFERENCES [dbo].[OwnershipFormSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnershipFormCompany'
CREATE INDEX [IX_FK_OwnershipFormCompany]
ON [dbo].[CompanySet]
    ([IdOwnershipForm]);
GO

-- Creating foreign key on [IdCompany] in table 'CompanyPerfomanceSet'
ALTER TABLE [dbo].[CompanyPerfomanceSet]
ADD CONSTRAINT [FK_CompanyCompanyPerfomance]
    FOREIGN KEY ([IdCompany])
    REFERENCES [dbo].[CompanySet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CompanyCompanyPerfomance'
CREATE INDEX [IX_FK_CompanyCompanyPerfomance]
ON [dbo].[CompanyPerfomanceSet]
    ([IdCompany]);
GO

-- Creating foreign key on [IdCriterion] in table 'CompanyPerfomanceSet'
ALTER TABLE [dbo].[CompanyPerfomanceSet]
ADD CONSTRAINT [FK_CriterionCompanyPerfomance]
    FOREIGN KEY ([IdCriterion])
    REFERENCES [dbo].[CriterionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CriterionCompanyPerfomance'
CREATE INDEX [IX_FK_CriterionCompanyPerfomance]
ON [dbo].[CompanyPerfomanceSet]
    ([IdCriterion]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------