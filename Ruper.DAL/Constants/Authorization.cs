namespace Ruper.DAL.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }
        public const string default_username = "Admin";
        public const string default_firstname = "Admin";
        public const string default_lastname = "Admin";
        public const string default_email = "user@secureapi.com";
        public const string default_password = "Pa$$w0rd.";
        public const Roles default_role = Roles.Administrator;
    }
}
