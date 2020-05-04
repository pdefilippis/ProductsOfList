using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Infrastructure
{
    public interface ILoteInfrastructure
    {
        Member.Output.Lote Save(Member.Input.Lote lote);
        Member.Output.Lote Update(Member.Input.Lote lote);
        Member.Output.Lote Create(Member.Input.Lote lote);
        void Delete(int id);
        Member.Output.Lote GetById(int id);
        ICollection<Member.Output.Lote> Get();
        ICollection<Member.Output.Lote> GetAll();
        void ChangeStatus(int id);
    }
}
