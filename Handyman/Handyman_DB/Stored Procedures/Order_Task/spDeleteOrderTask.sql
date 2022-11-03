﻿CREATE PROCEDURE [Request].[spDeleteOrderTask]
	@OrderId int = 0,
    @consumerId VARCHAR(MAX)

AS
BEGIN
DECLARE @listOfIDs table (id int);

--Delete bridge table records
    DELETE  FROM [Request].[order_task]
    WHERE [order_id] = @OrderId 

--Delete the order,
	DELETE  FROM [Request].[order]
    WHERE [ord_id] = @OrderId AND [ord_consumer_id] = @consumerId

--Delete the task,
--Get the id's of the tasks to delete

    insert INTO @listOfIDs(id) 
    (SELECT [task_id] 
    FROM [Request].[task])

    DELETE  FROM [Request].[task]
    WHERE [task_id] = (Select [id] from @listOfIDs)

END