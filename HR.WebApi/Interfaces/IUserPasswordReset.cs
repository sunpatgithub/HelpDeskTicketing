namespace HR.WebApi.Interfaces
{
    public interface IUserPasswordReset
    {
        void Insert(int id, string Link, string token_No);

        //string EncryptLink(string link);

        string DecryptLink(string link);

        bool VerifyLink(string login_Id, string token_No);

        void Delete(int id);

        string[] SplitLink(string link); //decrypt link and split login_id & token
    }
}
