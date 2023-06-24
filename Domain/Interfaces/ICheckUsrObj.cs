
namespace Domain.Interfaces
{
    public interface ICheckUsrObj
    {
        
        Task<bool> CheckUserNameExistAsync(string usrFullName);
        Task<bool> CheckEmailExistAsync(string usrEmail);
     

    }
}
