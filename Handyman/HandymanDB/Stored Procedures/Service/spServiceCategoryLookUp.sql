CREATE PROCEDURE [Service].[spServiceCategoryLookUp]
--Stored procedure for looking up 20 service that come with categories
as
Begin	
	SELECT s.Id, s.[Name], s.[ImageUrl],c.[CategoryId], c.[CategoryName], c.[Type]
    FROM [HandymanDB].[Service].[Service] s
	INNER JOIN [HandymanDB].[Service].[Category] c on c.[CategoryId] = s.[CategoryID]
	Order By c.[CategoryId] ASC
end
