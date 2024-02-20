CREATE TABLE [dbo].[cliente] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]          NVARCHAR (50) NOT NULL,
    [Apellido]        NVARCHAR (50) NOT NULL,
    [FechaNacimiento] DATETIME2 (7) NULL,
    [Cuit]            NVARCHAR (15) NOT NULL,
    [Telefono]        INT           NOT NULL,
    [Mail]            NVARCHAR (50) NOT NULL UNIQUE,
    CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UNIQUE_Cliente_Mail] UNIQUE NONCLUSTERED ([Mail] ASC),
    CONSTRAINT [UNIQUE_Cliente_Cuit] UNIQUE NONCLUSTERED ([Cuit] ASC)
);

