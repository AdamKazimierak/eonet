CREATE PROCEDURE [dbo].[usp_feature_propertyid_exists]
	@property_id NVARCHAR(100)
AS
	SELECT
		1 
	FROM 
		[dbo].[feature] AS [f]
	WHERE 
		[f].[property_id] = @property_id
