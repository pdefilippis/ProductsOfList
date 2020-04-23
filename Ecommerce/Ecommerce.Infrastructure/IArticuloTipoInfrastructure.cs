using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Infrastructure
{
    public interface IArticuloTipoInfrastructure
    {
        Member.Output.ArticuloTipo GetById(int id);
        Member.Output.ArticuloTipo GetByCodigo(string codigo);
        ICollection<Member.Output.ArticuloTipo> Get();

    }
}
