﻿using Umbraco.Cms.Api.Management.ViewModels.Tree;
using Umbraco.Cms.Core;

namespace Umbraco.Cms.Api.Management.ViewModels.MediaType.Item;

public class MediaTypeTreeItemResponseModel : FolderTreeItemResponseModel
{
    public string Icon { get; set; } = string.Empty;
    public override string Type => Constants.UdiEntityType.MediaType;
}
