using System;
using System.Collections.Generic;

namespace Repetitions
{
    internal sealed class AuthToken
    {
        public string UserName { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public DateTime ExpirationDateTime { get; set; }
    }
}