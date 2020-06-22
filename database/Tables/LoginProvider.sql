CREATE TABLE [dbo].[LoginProvider]
(
    [LoginProviderId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [ProviderName] NVARCHAR(255) NOT NULL,
    [ProviderLogo] VARBINARY(MAX) NULL,
    [ProviderBackground] VARBINARY(MAX) NULL,
    [Enabled] BIT NOT NULL,
    [CreatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [CreatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_NAME(),
    [UpdatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [UpdatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_SNAME(),
    CONSTRAINT UQ_ProviderName UNIQUE (ProviderName)
)
