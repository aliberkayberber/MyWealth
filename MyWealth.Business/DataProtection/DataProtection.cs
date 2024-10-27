using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWealth.Business.DataProtection
{
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector _protection;

        public DataProtection(IDataProtectionProvider provider)
        {
            _protection = provider.CreateProtector("Security");
        }
        public string Protect(string text)
        {
            return _protection.Protect(text);
        }

        public string UnProtect(string ProtectedText)
        {
            return _protection.Unprotect(ProtectedText);
        }
    }
}
