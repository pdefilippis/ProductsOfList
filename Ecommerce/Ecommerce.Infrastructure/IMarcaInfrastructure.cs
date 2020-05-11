using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Infrastructure
{
    public interface IMarcaInfrastructure
    {
        ICollection<Member.Output.Marca> Get();
    }
}
