CREATE TABLE [dbo].[SpMetadata]
(
	[SpMetadataId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[EntityId] NVARCHAR(255) NOT NULL UNIQUE,
	[ValidUntil] DATETIME NOT NULL,
	[AuthnRequestsSigned] BIT NOT NULL,
	[WantAssertionsSigned] BIT NOT NULL,
	[ProtocolSupportEnumeration] NVARCHAR(255) NOT NULL,
	[NameIdFormats] NVARCHAR(MAX) NOT NULL,
	[AcsBinding] NVARCHAR(255) NOT NULL,
	[AcsLocation] NVARCHAR(MAX) NOT NULL,
    [CreatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [CreatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_NAME(),
    [UpdatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [UpdatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_SNAME()
)
