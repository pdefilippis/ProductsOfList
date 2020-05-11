using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Core
{
    public interface IMarcaManager
    {
        ICollection<Member.Output.Marca> Get();
    }
}
