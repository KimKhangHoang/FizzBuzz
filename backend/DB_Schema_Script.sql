-- Create GameRules table
CREATE TABLE [dbo].[GameRules](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Name] [nvarchar](max) NOT NULL,
    [Author] [nvarchar](max) NOT NULL,
    [Range] [int] NOT NULL,
    CONSTRAINT [PK_GameRules] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

-- Create Rules table
CREATE TABLE [dbo].[Rules](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Divisor] [int] NOT NULL,
    [Replacement] [nvarchar](max) NOT NULL,
    [GameRuleId] [int] NOT NULL,
    CONSTRAINT [PK_Rules] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO