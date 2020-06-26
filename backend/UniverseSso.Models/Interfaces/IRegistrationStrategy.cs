using System;
using System.Collections.Generic;
using System.Text;
using UniverseSso.Models.Implementation;

namespace UniverseSso.Models.Interfaces
{
    public interface IRegistrationStrategy
    {
        RegistrationReasons Register(IDictionary<string, string> registrationFieldsDictionary);
    }
}
