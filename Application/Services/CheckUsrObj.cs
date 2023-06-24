using Domain.Interfaces;
using Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class CheckUsrObj:ICheckUsrObj
    {
        private readonly AuthenticationContext _authContext;
        public CheckUsrObj(AuthenticationContext authenticationContext) 
        {
            _authContext = authenticationContext;
        }
      
        public Task<bool> CheckUserNameExistAsync(string usrName)
           => _authContext.tblUsers.AnyAsync(x => x.UserName == usrName);
        public Task<bool> CheckEmailExistAsync(string usrEmail)
        => _authContext.tblUsers.AnyAsync(x => x.UsrEmail == usrEmail);

       
    }
}
