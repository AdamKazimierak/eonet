CREATE PROCEDURE [dbo].[usp_Feature_Create]
	@feature_type NVARCHAR(100),
	@property_id NVARCHAR(100),
	@property_title NVARCHAR(100),
	@property_description NVARCHAR(100) NULL,
	@property_date_utc DATETIME2(7),
	@category_id NVARCHAR(100),
	@category_title NVARCHAR(100),
	@geometry_type NVARCHAR(100),
	@latitude FLOAT,
	@longitude FLOAT
AS
	INSERT INTO [dbo].[feature]
		([feature_type], [property_id], [property_title], [property_description], [property_date_utc], [category_id], [category_title], [geometry_type], [latitude], [longitude])
	OUTPUT
		[Inserted].[Id]
	SELECT
		@feature_type,
		@property_id,
		@property_title,
		@property_description,
		@property_date_utc,
		@category_id,
		@category_title,
		@geometry_type,
		@latitude,
		@longitude
	WHERE NOT EXISTS 
	(
		SELECT 
			1 
		FROM 
			[dbo].[feature] AS [f]
		WHERE 
			[f].[property_id] = @property_id
	)
RETURN 0