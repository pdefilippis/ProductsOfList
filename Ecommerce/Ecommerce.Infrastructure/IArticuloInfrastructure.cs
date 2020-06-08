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
        ICollection<Member.Output.Articulo> GetByLote(int lote);
        ICollection<Member.Output.Articulo> Get();
        ICollection<Member.Output.Articulo> GetAll();
        void Delete(int id);
        void Postular(Member.Input.ArticuloPostulacion postulacion);
        void DeclinarPostulacion(Member.Input.ArticuloPostulacion postulacion);
        bool ExistsPostulacion(Member.Input.ArticuloPostulacion postulacion);
        void AdjudicarArticulo(int idArticulo, int idUsuario);
        void ChangeStatus(int id);
        ICollection<Member.Output.Articulo> GetPostulados(int idUsuario);
        ICollection<Member.Output.Articulo> GetByUserInteresado(int idUsuario);
    }
}
