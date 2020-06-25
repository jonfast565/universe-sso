CREATE TABLE [dbo].[AuthenticationStrategy]
(
    [AuthenticationStrategyId] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [StrategyName] NVARCHAR(255) NOT NULL,
    [CreatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [CreatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_NAME(),
    [UpdatedDatetime] DATETIME NOT NULL
        DEFAULT GETDATE(),
    [UpdatedBy] NVARCHAR(255) NOT NULL
        DEFAULT SUSER_SNAME()
)
