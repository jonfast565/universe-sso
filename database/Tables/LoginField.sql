CREATE TABLE [dbo].[LoginField]
(
    [LoginFieldId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [LoginProviderId] INT NOT NULL REFERENCES [LoginProvider]([LoginProviderId]),
    [FieldName] NVARCHAR(255) NOT NULL,
    [FieldType] NVARCHAR(255) NOT NULL,
    [OptionalFieldValues] NVARCHAR(MAX) NULL,
    [CreatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [CreatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_NAME(),
    [UpdatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [UpdatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_SNAME()
)
