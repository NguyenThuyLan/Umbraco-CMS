﻿namespace Umbraco.Cms.Api.Management.ViewModels.Dictionary;

public class DictionaryItemResponseModel : DictionaryItemModelBase, INamedEntityPresentationModel
{
    public Guid Id { get; set; }

    public string Type => Constants.ResourceObjectTypes.DictionaryItem;
}
