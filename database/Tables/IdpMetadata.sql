CREATE TABLE [dbo].[IdpMetadata]
(
	[IdpMetadataId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[EntityId] NVARCHAR(255) NOT NULL,
	[WantAuthnRequestsSigned] BIT NOT NULL,
	[ProtocolSupportEnumeration] NVARCHAR(255) NOT NULL,
	[SigningCertificate] VARBINARY(MAX) NOT NULL,
	[EncryptionCertificate] VARBINARY(MAX) NOT NULL,
	[NameIdFormats] NVARCHAR(MAX) NOT NULL,
	[SsoBinding] NVARCHAR(255) NOT NULL,
	[SsoLocation] NVARCHAR(255) NOT NULL,
	    [CreatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [CreatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_NAME(),
    [UpdatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [UpdatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_SNAME()
)
