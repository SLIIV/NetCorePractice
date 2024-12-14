using System.Text;
using Microsoft.AspNetCore.DataProtection;
namespace NetCorePractice
{
    public class Encryptor
    {
        private readonly IDataProtectionProvider _dataProtectorProvider = DataProtectionProvider.Create("NetCorePractice");

        public string Enctypt(string text)
        {
            IDataProtectorFactory _dataProtectorFactory = new DataProtectorFactory(_dataProtectorProvider);
            IDataProtector _dataProtector = _dataProtectorFactory.CreateProtector();
            if(text == null) throw new ArgumentNullException("text");

            var data = Encoding.Unicode.GetBytes(text);
            byte[] enctypted = _dataProtector.Protect(data);
            return Convert.ToBase64String(enctypted);
        }
    }
}
