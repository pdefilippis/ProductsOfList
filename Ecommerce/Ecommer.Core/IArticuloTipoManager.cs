using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Core
{
    public interface IArticuloTipoManager
    {
        Member.Output.ArticuloTipo GetById(int id);
        Member.Output.ArticuloTipo GetByCodigo(string codigo);
        ICollection<Member.Output.ArticuloTipo> Get();
    }
}
