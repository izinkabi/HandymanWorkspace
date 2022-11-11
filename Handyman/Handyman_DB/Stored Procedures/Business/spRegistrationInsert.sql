CREATE PROCEDURE [Delivery].[spRegistrationInsert]
	
    @reg_name NVARCHAR (100), 
    @reg_regnumber NVARCHAR (50) , 
    @reg_taxnumber NVARCHAR(50), 
    @reg_businesstype INT = 0
AS
BEGIN
	INSERT INTO [Delivery].[registration] (reg_name,reg_regnumber,reg_taxnumber,reg_businesstype)
    VALUES                                (@reg_name,@reg_regnumber,@reg_taxnumber,@reg_businesstype)

    DECLARE @Id INT = SCOPE_IDENTITY();

    SELECT [reg_Id] 
    FROM   [Delivery].[registration] 
    WHERE  [reg_Id] = @Id
END
