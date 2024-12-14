using Microsoft.AspNetCore.DataProtection;

namespace NetCorePractice
{
    public interface IDataProtectorFactory
    {
        IDataProtector CreateProtector();
    }
    public class DataProtectorFactory : IDataProtectorFactory
    {
        private readonly IDataProtector _protector;
        
        public DataProtectorFactory(IDataProtectionProvider protector)
        {
            this._protector = protector.CreateProtector("NetCorePractice.Enctypt.v1");
        }
        public IDataProtector CreateProtector()
        {
            return _protector;
        }
    }
}
