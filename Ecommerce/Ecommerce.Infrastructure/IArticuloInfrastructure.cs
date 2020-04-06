using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Infrastructure
{
    public interface IArticuloInfrastructure
    {
        Member.Output.Articulo Save(Member.Input.Articulo articulo);
        Member.Output.Articulo Update(Member.Input.Articulo articulo);
        Member.Output.Articulo Create(Member.Input.Articulo articulo);
        Member.Output.Articulo GetById(int id);
        Member.Output.Articulo GetByLote(int lote);
        Member.Output.Articulo Get();
        void Delete(int id);

    }
}
