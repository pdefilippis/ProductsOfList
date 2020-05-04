using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Core
{
    public interface ILoteManager
    {
        Member.Output.Lote GetById(int id);
        ICollection<Member.Output.Lote> Get();
        Member.Output.Lote Save(Member.Input.Lote lote);
        void Sorteo(int lote);
        void Enable(int lote);
        void Disable(int lote);
    }
}
