using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzManWinUI
{
	public enum StructureViewEnum : short
	{
		RoleTask=1,
		Role=2, 
	}

	public enum AuthorizationViewEnum : short
	{
		RoleTask=1,
		Role=2
	}

	public enum TreeNodeSizeEnum : short
	{
		Small = 0,
		Large = 1
	}

	public enum ListViewTypeEnum : short
	{
		Details = 0,
		List = 1,
		LargeIcons = 2,
		SmallIcons = 3,
		Tile = 4
	}
}
