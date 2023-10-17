﻿using Umbraco.Cms.Api.Management.ViewModels.TextFiles;

namespace Umbraco.Cms.Api.Management.ViewModels.Stylesheet;

public class StylesheetResponseModel : TextFileResponseModelBase
{
    public override string Type => Constants.ResourceObjectTypes.Stylesheet;
}
