CREATE TABLE [dbo].[feature]
(
	[id] INT NOT NULL IDENTITY(1,1),
	[feature_type] NVARCHAR(100) NOT NULL,
	[property_id] NVARCHAR(100) NOT NULL,
	[property_title] NVARCHAR(100) NOT NULL,
	[property_description] NVARCHAR(100) NULL,
	[property_date_utc] DATETIME2(7) NOT NULL,
	[category_id] NVARCHAR(100) NOT NULL,
	[category_title] NVARCHAR(100) NOT NULL,
	[geometry_type] NVARCHAR(100) NOT NULL,
	[latitude] FLOAT NOT NULL,
	[longitude] FLOAT NOT NULL,
	PRIMARY KEY CLUSTERED ([id] ASC),
	CONSTRAINT UC_PropertyId UNIQUE ([property_id])
);

GO
CREATE NONCLUSTERED INDEX [IX_Features_Property_Date_Utc]
	ON [dbo].[feature]([property_date_utc] ASC);
