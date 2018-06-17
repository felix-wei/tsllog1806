using System;
using System.Web;
using System.Web.Security;
using Wilson.ORMapper;

namespace C2
{
    public class Membership : MembershipProvider
    {
        private static readonly string KEY_minRequiredPasswordLength = "minRequiredPasswordLength";
        private static readonly string KEY_minRequiredNonAlphanumericCharacters = "minRequiredNonalphanumericCharacters";

        public override MembershipPasswordFormat PasswordFormat { get { return MembershipPasswordFormat.Hashed; } }
        public override bool RequiresUniqueEmail { get { return true; } }
        public override bool EnablePasswordReset { get { return true; } }
        public override bool EnablePasswordRetrieval { get { return false; } } // Unsupported with Hashed
        public override bool RequiresQuestionAndAnswer { get { return false; } } // Unsupported Feature
        public override int MinRequiredPasswordLength { get { return this.minRequiredPasswordLength; } }
        public override int MinRequiredNonAlphanumericCharacters { get { return this.minRequiredNonAlphanumericCharacters; } }
        public override string PasswordStrengthRegularExpression { get { return null; } } // Unsupported Feature
        public override int MaxInvalidPasswordAttempts { get { return 0; } } // Unsupported Feature
        public override int PasswordAttemptWindow { get { return 0; } } // Unsupported Feature

        private int minRequiredPasswordLength = 8;
        private int minRequiredNonAlphanumericCharacters = 1;

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
            if (!string.IsNullOrEmpty(config[KEY_minRequiredPasswordLength]))
            {
                try
                {
                    this.minRequiredPasswordLength = int.Parse(config[KEY_minRequiredPasswordLength]);
                }
                catch (Exception exception)
                {
                    throw new ArgumentException("Invalid Configuration for WilsonProviders Membership", KEY_minRequiredPasswordLength, exception);
                }
                config.Remove(KEY_minRequiredPasswordLength);
            }
            if (!string.IsNullOrEmpty(config[KEY_minRequiredNonAlphanumericCharacters]))
            {
                try
                {
                    this.minRequiredNonAlphanumericCharacters = int.Parse(config[KEY_minRequiredNonAlphanumericCharacters]);
                }
                catch (Exception exception)
                {
                    throw new ArgumentException("Invalid Configuration for WilsonProviders Membership", KEY_minRequiredNonAlphanumericCharacters, exception);
                }
                config.Remove(KEY_minRequiredNonAlphanumericCharacters);
            }
        }

        public override string ApplicationName
        {
            get { return "ERM ASP.Net Providers"; }
            set { throw new NotSupportedException("The method or operation is not supported."); }
        }


        public override bool ValidateUser(string userName, string password)
        {
            OPathQuery query = new OPathQuery(typeof(C2.User), "Name='" + userName + "'", "Name");
            C2.User user = C2.Manager.ORManager.GetObject(query) as C2.User;
            if (user == null) return false;
            Encryption.EncryptClass encrypt = new Encryption.EncryptClass();
            return (SafeValue.SafeString(encrypt.DESEnCode(userName, password), "") == user.Pwd);
            // return (user.Pwd == password);
        }

        public override bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (!this.ValidPassword(newPassword)) return false;

            {
                OPathQuery query = new OPathQuery(typeof(C2.User), "Name='" + userName + "'", "Name");
                C2.User user = C2.Manager.ORManager.GetObject(query) as C2.User;
                if (user == null) return false;
                if (user.Pwd == oldPassword)
                {
                    user.Pwd = newPassword;
                    C2.Manager.ORManager.StartTracking(user, InitialState.Updated);
                    C2.Manager.ORManager.PersistChanges(user);
                    return true;
                }
            }
            return false;
        }

        public override string ResetPassword(string userName, string answer)
        {
            string password = "password";
            System.Web.Security.Membership.GeneratePassword(
            this.MinRequiredPasswordLength, this.MinRequiredNonAlphanumericCharacters);

            OPathQuery query = new OPathQuery(typeof(C2.User), "Name='" + userName + "'", "Name");
            C2.User user = C2.Manager.ORManager.GetObject<C2.User>(query);
            if (user == null) return null;

            user.Pwd = password;
            C2.Manager.ORManager.StartTracking(user, InitialState.Updated);
            C2.Manager.ORManager.PersistChanges(user);
            return password;
        }

        // Needed for Password Charge and Password Reset
        public override MembershipUser GetUser(string userName, bool userIsOnline)
        {
            OPathQuery query = new OPathQuery(typeof(C2.User), "Name='" + userName + "'", "Name");
            C2.User user = C2.Manager.ORManager.GetObject<C2.User>(query);
            if (user == null) return null;
            return new MembershipUser(this.Name, user.Name, user.SequenceId, user.Email, null, null, true, false,
                    DateTime.MinValue, DateTime.Today, DateTime.Today, DateTime.MinValue, DateTime.MinValue);
        }

        private bool ValidPassword(string password)
        {
            if (password.Length < 3) return false;
            return true;
            //int nonAlphas = 0;
            //for (int index = 0; index < password.Length; index++) {
            //    if (!char.IsLetterOrDigit(password, index)) nonAlphas++;
            //}
            //return (nonAlphas >= this.MinRequiredNonAlphanumericCharacters);
        }

        #region Unsupported Features

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override string GetPassword(string userName, string answer)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        protected override byte[] DecryptPassword(byte[] encodedPassword)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        protected override byte[] EncryptPassword(byte[] password)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override bool ChangePasswordQuestionAndAnswer(string userName, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override string GetUserNameByEmail(string userEmail)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override MembershipUser CreateUser(string userName, string password, string userEmail, string passwordQuestion,
                string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {

            C2.User user = new C2.User();

            user.Name = userName;
            user.Role = "GUEST";
            user.Pwd = password;
            user.Email = userEmail;
            status = MembershipCreateStatus.Success;
            C2.Manager.ORManager.StartTracking(user, InitialState.Inserted);
            C2.Manager.ORManager.PersistChanges(user);


            return new MembershipUser(this.Name, user.Name, user.SequenceId, user.Email, null, null, true, false,
                    DateTime.MinValue, DateTime.Today, DateTime.Today, DateTime.MinValue, DateTime.MinValue);

            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override bool DeleteUser(string userName, bool deleteAllRelatedData)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        public override MembershipUserCollection FindUsersByName(string userNameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException("The method or operation is not supported.");
        }

        #endregion
    }
}