
using Candal.Cryptography;

namespace Candal.Configuration
{
    public class AppConfigData
    {
        private const string _passPhrase = "(#_XPTO.19:65--02..01)";
        public string GitUrl { get; set; }
        public bool UseSSH { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RootFolder { get; set; }

        public void Encrypt()
        {
            if (Password != null)
                Password = Cipher.Encrypt(Password, _passPhrase);
        }

        public void Decrypt()
        {
            if (Password != null)
                Password = Cipher.Decrypt(Password, _passPhrase);
        }
    }
}
