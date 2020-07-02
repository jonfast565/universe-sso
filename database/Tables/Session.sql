CREATE TABLE [dbo].[Session]
(
    [SessionId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [ProviderId] INT NOT NULL REFERENCES [Provider]([ProviderId]),
    [SessionToken] NVARCHAR(MAX) NOT NULL DEFAULT HASHBYTES('MD5', CONVERT(NVARCHAR, GETDATE())),
    [SessionData] NVARCHAR(MAX) NOT NULL,
        [CreatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [CreatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_NAME(),
    [UpdatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [UpdatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_SNAME()
    CONSTRAINT [CH_SessionData] CHECK (ISJSON([SessionData]) > 0)
)
