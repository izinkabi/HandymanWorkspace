CREATE PROCEDURE [Delivery].[spEmployeesLookUpByWorkShop]
	@workShopId int = 0
AS
Begin
	SELECT 
            e.[emp_workShopid],e.[emp_date_employed],e.[emp_profile_id], e.[emp_role],e.[emp_profile_id],
           
            p.[Names],p.[Surname],p.[DateOfBirth],p.[Gender],p.[PhoneNumber],p.[AddressId]
    FROM 
          [Delivery].[employee] e, 
          [dbo].[Profile] p
  
    WHERE 
           [p].[userid] = [e].[emp_profile_id] AND
           [e].[emp_workShopid] = @workShopId 
END

