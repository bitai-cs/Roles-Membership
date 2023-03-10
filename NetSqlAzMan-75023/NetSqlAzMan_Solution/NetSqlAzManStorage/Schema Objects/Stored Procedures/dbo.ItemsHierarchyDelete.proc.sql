CREATE PROCEDURE [dbo].[netsqlazman_ItemsHierarchyDelete]
(
	@ItemId int,
	@MemberOfItemId int,
	@ApplicationId int
)
AS
IF EXISTS(SELECT ItemId FROM dbo.[netsqlazman_Items]() WHERE ItemId = @ItemId) AND dbo.[netsqlazman_CheckApplicationPermissions](@ApplicationId, 2) = 1
	DELETE FROM [dbo].[netsqlazman_ItemsHierarchyTable] WHERE [ItemId] = @ItemId AND [MemberOfItemId] = @MemberOfItemId
ELSE
	RAISERROR ('Application permission denied.', 16, 1)


