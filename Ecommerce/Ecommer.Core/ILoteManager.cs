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
        ICollection<Member.Output.Lote> GetAll();
        ICollection<Member.Output.Lote> GetOpen();
        Member.Output.Lote Save(Member.Input.Lote lote);
        ICollection<Member.Output.Usuario> Sorteo(int lote);
        bool Enable(int lote);
        bool Disable(int lote);
        bool Open(int lote);
        bool Close(int lote);
    }
}
