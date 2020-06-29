CREATE TABLE [dbo].[Field]
(
    [FieldId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [ProviderId] INT NOT NULL REFERENCES [Provider]([ProviderId]),
    [PageType] NVARCHAR(255) NOT NULL,
    [FieldName] NVARCHAR(255) NOT NULL,
    [FieldType] NVARCHAR(255) NOT NULL,
    [Required] BIT NOT NULL,
    [Pattern] NVARCHAR(MAX) NULL,
    [OptionalFieldValues] NVARCHAR(MAX) NULL,
    [Order] INT NOT NULL,
    [CreatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [CreatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_NAME(),
    [UpdatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [UpdatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_SNAME()
)
