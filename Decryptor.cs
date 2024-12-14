using System.Text;
using Microsoft.AspNetCore.DataProtection;
namespace NetCorePractice
{
    public class Dectyptor
    {
        private readonly IDataProtectionProvider _dataProtectorProvider = DataProtectionProvider.Create("NetCorePractice");

        public string Decrypt(string text)
        {
            IDataProtectorFactory _dataProtectorFactory = new DataProtectorFactory(_dataProtectorProvider);
            IDataProtector _dataProtector = _dataProtectorFactory.CreateProtector();
            if (text == null) throw new ArgumentNullException("text");

            byte[] data = Convert.FromBase64String(text);
            byte[] dectypted = _dataProtector.Unprotect(data);
            return Encoding.Unicode.GetString(dectypted);
        }
    }
}
