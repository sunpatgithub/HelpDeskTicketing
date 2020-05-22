using HR.WebApi.Model;
using System.Collections.Generic;

namespace HR.WebApi.Interfaces
{
    public interface IUserService<T>
    {
        IEnumerable<T> GetAll(int RecordLimit);

        T GetUserById(int id);

        T GetUserByLoginId(string login_Id);

        void CreateUser(T entity);

        void UpdateUser(T entity);

        bool UserExist(T entity);

        IEnumerable<T> FindUserById(int id);
        IEnumerable<T> FindUserByLogInId(string login_Id);

        void ChangePassword(int id, string oldPassword, string newPassword); //used from IUser_Password

        void ForgotPassword(string login_Id);

        void AdminChangePassword(string login_Id, string password);

        bool ResetPassword(string login_Id, string password);

        void ActivateUser(int id); //ToogleStatus

        void InactivateUser(int id); //ToogleStatus      

        IEnumerable<T> FindPaginated(int pageIndex, int pageSize, string searchValue);

        User_Response SignIn(User_Request user_Request);

        void SignOut(int id);
    }
}
