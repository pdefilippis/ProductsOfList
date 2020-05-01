﻿using System;
using System.Collections.Generic;
using System.Text;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Core
{
    public interface IArticuloManager
    {
        Member.Output.Articulo GetById(int id);
        Member.Output.Articulo Save(Member.Input.Articulo articulo);
        ICollection<Member.Output.Articulo> Get();
        ICollection<Member.Output.Articulo> GetLote(int lote);


    }
}
