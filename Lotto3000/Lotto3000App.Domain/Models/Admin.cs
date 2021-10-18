using System;
using System.Collections.Generic;
using System.Text;

namespace Lotto3000App.Domain.Models
{
    public class Admin : User
    {
        public override bool IsAdmin { get; } = true;
    }
}
