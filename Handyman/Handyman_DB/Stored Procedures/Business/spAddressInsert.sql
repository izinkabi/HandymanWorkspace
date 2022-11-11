CREATE PROCEDURE [Delivery].[spAddressInsert]
	
    @add_street NVARCHAR(50), 
    @add_suburb NVARCHAR(50), 
    @add_city NVARCHAR(50), 
    @add_zip NVARCHAR(50), 
    @add_latitude FLOAT , 
    @add_longitude FLOAT, 
    @add_country NVARCHAR(50), 
    @add_state NVARCHAR(50) 

AS
BEGIN
	INSERT INTO [Delivery].[address] 
    (
        [ add_street] ,add_suburb , add_city, 
        add_zip, add_latitude, add_longitude, 
        add_country, add_state
    )
    VALUES 
    (
        @add_street ,@add_suburb , @add_city, 
        @add_zip, @add_latitude, @add_longitude, 
        @add_country, @add_state
    )

DECLARE @Id INT = SCOPE_IDENTITY();
    SELECT [add_Id] 
    FROM   [Delivery].[address] 
    WHERE  [add_Id] = @Id
END