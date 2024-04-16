using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;

namespace Umbraco.Cms.Core.Notifications;
/// <summary>
/// A notification that is used to trigger the ContentTypeService when the Delete method is called in the API.
/// </summary>
public class ContentTypeDeletingNotification : DeletingNotification<IContentType>
{
    public ContentTypeDeletingNotification(IContentType target, EventMessages messages)
        : base(target, messages)
    {
    }

    public ContentTypeDeletingNotification(IEnumerable<IContentType> target, EventMessages messages)
        : base(
        target,
        messages)
    {
    }
}
