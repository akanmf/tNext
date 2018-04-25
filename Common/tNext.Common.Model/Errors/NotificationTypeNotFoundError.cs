using tNext.Common.Model.Abstracts;

namespace tNext.Common.Model.Errors
{
    public class NotificationTypeNotFoundError : tNextErrorBase
    {
        public NotificationTypeNotFoundError(string notificationType)
            : base("NOTIFICATION_TYPE_NOT_FOUND", $"{notificationType} bulunamadı", "Hatalı bildirim tipi.")
        {

        }
    }
}
