CREATE PROCEDURE [dbo].[usp_feature_get_all]
	@start_date_utc DATETIME2(7),
	@end_date_utc DATETIME2(7),
	@page_number INT,
	@page_size INT
AS
	SELECT 
		[f].[id],
		[f].[feature_type] AS [Type],
		[f].[property_id] AS [PropertyId],
		[f].[property_title] AS [PropertyTitle],
		[f].[property_description] AS [PropertyDescription],
		[f].[property_date_utc] AS [PropertyDateUtc],
		[f].[category_id] AS [CategroyId],
		[f].[category_title] AS [CategoryTitle],
		[f].[geometry_type] AS [GeometryType],
		[f].[latitude] AS [Latitude],
		[f].[longitude] AS [Longitude]
	FROM
		[dbo].[feature] 
	AS 
		[f]
	WHERE 
		[f].[property_date_utc] BETWEEN @start_date_utc AND @end_date_utc
	ORDER BY 
		[f].[property_date_utc]
	OFFSET 
		(@page_number - 1) * @page_size ROWS
	FETCH NEXT 
		@page_size 
	ROWS 
		ONLY;