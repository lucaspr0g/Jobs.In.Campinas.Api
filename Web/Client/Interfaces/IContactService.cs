using Web.Client.Entities.Contact;

namespace Web.Client.Interfaces
{
    public interface IContactService
    {
        Task<(bool, string)> Send(ContactModel model);
    }
}