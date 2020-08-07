
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/13/2020 09:23:38
-- Generated from EDMX file: F:\TruongMamNon\Models\TruongMamNonModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [TruongMamNon];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetRole];
GO
IF OBJECT_ID(N'[dbo].[FK_AspNetUserRoles_AspNetUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_AspNetUserRoles_AspNetUser];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_DiemDanhGiaoVien_GiaoViens]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiemDanhGiaoViens] DROP CONSTRAINT [FK_DiemDanhGiaoVien_GiaoViens];
GO
IF OBJECT_ID(N'[dbo].[FK_DiemDanhHocSinh_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiemDanhHocSinhs] DROP CONSTRAINT [FK_DiemDanhHocSinh_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_DiemDanhHocSinh_HocSinhs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DiemDanhHocSinhs] DROP CONSTRAINT [FK_DiemDanhHocSinh_HocSinhs];
GO
IF OBJECT_ID(N'[dbo].[FK_GiaoVien_Lops_GiaoViens]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GiaoVien_Lops] DROP CONSTRAINT [FK_GiaoVien_Lops_GiaoViens];
GO
IF OBJECT_ID(N'[dbo].[FK_GiaoViens_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GiaoViens] DROP CONSTRAINT [FK_GiaoViens_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_PhuHuynh_AspNetUsers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhuHuynhs] DROP CONSTRAINT [FK_PhuHuynh_AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[FK_PhuHuynh_HocSinh_HocSinhs]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhuHuynh_HocSinh] DROP CONSTRAINT [FK_PhuHuynh_HocSinh_HocSinhs];
GO
IF OBJECT_ID(N'[dbo].[FK_PhuHuynh_HocSinh_PhuHuynh]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhuHuynh_HocSinh] DROP CONSTRAINT [FK_PhuHuynh_HocSinh_PhuHuynh];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[C__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[C__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[DiemDanhGiaoViens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DiemDanhGiaoViens];
GO
IF OBJECT_ID(N'[dbo].[DiemDanhHocSinhs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DiemDanhHocSinhs];
GO
IF OBJECT_ID(N'[dbo].[GiaoVien_Lops]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GiaoVien_Lops];
GO
IF OBJECT_ID(N'[dbo].[GiaoViens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GiaoViens];
GO
IF OBJECT_ID(N'[dbo].[HocSinhs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HocSinhs];
GO
IF OBJECT_ID(N'[dbo].[PhuHuynh_HocSinh]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhuHuynh_HocSinh];
GO
IF OBJECT_ID(N'[dbo].[PhuHuynhs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhuHuynhs];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL,
    [img] nvarchar(256)  NULL
);
GO

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'DiemDanhGiaoViens'
CREATE TABLE [dbo].[DiemDanhGiaoViens] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_GiaoVien] int  NULL,
    [ThoiGian] datetime  NOT NULL
);
GO

-- Creating table 'DiemDanhHocSinhs'
CREATE TABLE [dbo].[DiemDanhHocSinhs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_HocSinh] int  NULL,
    [ThoiGian] datetime  NOT NULL,
    [NguoiDuaDon] nvarchar(200)  NULL,
    [DenVe] nvarchar(10)  NULL,
    [Id_NguoiDiemDanh] nvarchar(128)  NULL
);
GO

-- Creating table 'GiaoVien_Lops'
CREATE TABLE [dbo].[GiaoVien_Lops] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_GiaoVien] int  NOT NULL,
    [LopHoc] nvarchar(20)  NULL,
    [TrangThai] nvarchar(20)  NULL
);
GO

-- Creating table 'GiaoViens'
CREATE TABLE [dbo].[GiaoViens] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HoTen] nvarchar(255)  NULL,
    [img] nvarchar(255)  NULL,
    [QueQuan] nvarchar(255)  NULL,
    [DanToc] nvarchar(20)  NULL,
    [TonGiao] nvarchar(20)  NULL,
    [HKTT] nvarchar(255)  NULL,
    [SDT] nchar(10)  NULL,
    [ChuyenNganh] nvarchar(100)  NULL,
    [TrinhDo] nvarchar(20)  NULL,
    [TrinhTrangCongTac] nvarchar(20)  NULL,
    [HonNhan] nvarchar(20)  NULL,
    [SucKhoe] nvarchar(20)  NULL,
    [NgayTao] datetime  NOT NULL,
    [Id_User] nvarchar(128)  NOT NULL,
    [NgaySinh] datetime  NOT NULL,
    [GioiTinh] nvarchar(20)  NULL
);
GO

-- Creating table 'HocSinhs'
CREATE TABLE [dbo].[HocSinhs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HoTen] nvarchar(255)  NULL,
    [BiDanh] nvarchar(255)  NULL,
    [DanToc] nvarchar(255)  NULL,
    [NgaySinh] datetime  NOT NULL,
    [img] nvarchar(255)  NULL,
    [NgayTao] datetime  NOT NULL,
    [GioiTinh] nvarchar(10)  NULL,
    [LopHoc] nvarchar(10)  NULL
);
GO

-- Creating table 'PhuHuynh_HocSinh'
CREATE TABLE [dbo].[PhuHuynh_HocSinh] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_PhuHuynh] int  NOT NULL,
    [Id_HocSinh] int  NULL,
    [QuanHe] nvarchar(30)  NULL
);
GO

-- Creating table 'PhuHuynhs'
CREATE TABLE [dbo].[PhuHuynhs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HoTen] nvarchar(255)  NULL,
    [SDT] nchar(10)  NULL,
    [NamSinh] nchar(10)  NULL,
    [NoiCongTac] nvarchar(255)  NULL,
    [NgayTao] datetime  NOT NULL,
    [Id_User] nvarchar(128)  NOT NULL,
    [img] nvarchar(128)  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'DiemDanhGiaoViens'
ALTER TABLE [dbo].[DiemDanhGiaoViens]
ADD CONSTRAINT [PK_DiemDanhGiaoViens]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DiemDanhHocSinhs'
ALTER TABLE [dbo].[DiemDanhHocSinhs]
ADD CONSTRAINT [PK_DiemDanhHocSinhs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GiaoVien_Lops'
ALTER TABLE [dbo].[GiaoVien_Lops]
ADD CONSTRAINT [PK_GiaoVien_Lops]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GiaoViens'
ALTER TABLE [dbo].[GiaoViens]
ADD CONSTRAINT [PK_GiaoViens]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HocSinhs'
ALTER TABLE [dbo].[HocSinhs]
ADD CONSTRAINT [PK_HocSinhs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhuHuynh_HocSinh'
ALTER TABLE [dbo].[PhuHuynh_HocSinh]
ADD CONSTRAINT [PK_PhuHuynh_HocSinh]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PhuHuynhs'
ALTER TABLE [dbo].[PhuHuynhs]
ADD CONSTRAINT [PK_PhuHuynhs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [Id_NguoiDiemDanh] in table 'DiemDanhHocSinhs'
ALTER TABLE [dbo].[DiemDanhHocSinhs]
ADD CONSTRAINT [FK_DiemDanhHocSinh_AspNetUsers]
    FOREIGN KEY ([Id_NguoiDiemDanh])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiemDanhHocSinh_AspNetUsers'
CREATE INDEX [IX_FK_DiemDanhHocSinh_AspNetUsers]
ON [dbo].[DiemDanhHocSinhs]
    ([Id_NguoiDiemDanh]);
GO

-- Creating foreign key on [Id_User] in table 'GiaoViens'
ALTER TABLE [dbo].[GiaoViens]
ADD CONSTRAINT [FK_GiaoViens_AspNetUsers]
    FOREIGN KEY ([Id_User])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GiaoViens_AspNetUsers'
CREATE INDEX [IX_FK_GiaoViens_AspNetUsers]
ON [dbo].[GiaoViens]
    ([Id_User]);
GO

-- Creating foreign key on [Id_User] in table 'PhuHuynhs'
ALTER TABLE [dbo].[PhuHuynhs]
ADD CONSTRAINT [FK_PhuHuynh_AspNetUsers]
    FOREIGN KEY ([Id_User])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PhuHuynh_AspNetUsers'
CREATE INDEX [IX_FK_PhuHuynh_AspNetUsers]
ON [dbo].[PhuHuynhs]
    ([Id_User]);
GO

-- Creating foreign key on [Id_GiaoVien] in table 'DiemDanhGiaoViens'
ALTER TABLE [dbo].[DiemDanhGiaoViens]
ADD CONSTRAINT [FK_DiemDanhGiaoVien_GiaoViens]
    FOREIGN KEY ([Id_GiaoVien])
    REFERENCES [dbo].[GiaoViens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiemDanhGiaoVien_GiaoViens'
CREATE INDEX [IX_FK_DiemDanhGiaoVien_GiaoViens]
ON [dbo].[DiemDanhGiaoViens]
    ([Id_GiaoVien]);
GO

-- Creating foreign key on [Id_HocSinh] in table 'DiemDanhHocSinhs'
ALTER TABLE [dbo].[DiemDanhHocSinhs]
ADD CONSTRAINT [FK_DiemDanhHocSinh_HocSinhs]
    FOREIGN KEY ([Id_HocSinh])
    REFERENCES [dbo].[HocSinhs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DiemDanhHocSinh_HocSinhs'
CREATE INDEX [IX_FK_DiemDanhHocSinh_HocSinhs]
ON [dbo].[DiemDanhHocSinhs]
    ([Id_HocSinh]);
GO

-- Creating foreign key on [Id_GiaoVien] in table 'GiaoVien_Lops'
ALTER TABLE [dbo].[GiaoVien_Lops]
ADD CONSTRAINT [FK_GiaoVien_Lops_GiaoViens]
    FOREIGN KEY ([Id_GiaoVien])
    REFERENCES [dbo].[GiaoViens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GiaoVien_Lops_GiaoViens'
CREATE INDEX [IX_FK_GiaoVien_Lops_GiaoViens]
ON [dbo].[GiaoVien_Lops]
    ([Id_GiaoVien]);
GO

-- Creating foreign key on [Id_HocSinh] in table 'PhuHuynh_HocSinh'
ALTER TABLE [dbo].[PhuHuynh_HocSinh]
ADD CONSTRAINT [FK_PhuHuynh_HocSinh_HocSinhs]
    FOREIGN KEY ([Id_HocSinh])
    REFERENCES [dbo].[HocSinhs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PhuHuynh_HocSinh_HocSinhs'
CREATE INDEX [IX_FK_PhuHuynh_HocSinh_HocSinhs]
ON [dbo].[PhuHuynh_HocSinh]
    ([Id_HocSinh]);
GO

-- Creating foreign key on [Id_PhuHuynh] in table 'PhuHuynh_HocSinh'
ALTER TABLE [dbo].[PhuHuynh_HocSinh]
ADD CONSTRAINT [FK_PhuHuynh_HocSinh_PhuHuynh]
    FOREIGN KEY ([Id_PhuHuynh])
    REFERENCES [dbo].[PhuHuynhs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PhuHuynh_HocSinh_PhuHuynh'
CREATE INDEX [IX_FK_PhuHuynh_HocSinh_PhuHuynh]
ON [dbo].[PhuHuynh_HocSinh]
    ([Id_PhuHuynh]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------