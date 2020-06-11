using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using Infra = Ecommerce.Infrastructure;
using Core = Ecommerce.Core;
using Dom = Ecommerce.Domain;
using Unity.Injection;
using FluentValidation;
using DataMembers = Ecommerce.Common.DataMembers;
using Ecommerce.Core.Validations;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Test
{
    public static class UnityConfig
    {
        private static Lazy<IUnityContainer> container =
         new Lazy<IUnityContainer>(() =>
         {
             var container = new UnityContainer();
             RegisterTypes(container);
             return container;
         });

        public static IUnityContainer Container => container.Value;

        public static void RegisterTypes(IUnityContainer container)
        {
            RegisterDomain(container);
            RegisterCore(container);
            RegisterInfrastructure(container);
        }

        private static void RegisterCore(IUnityContainer container)
        {
            container.RegisterType<Core.IArticuloManager, Core.Managers.Articulo>();
            container.RegisterType<Core.IArticuloTipoManager, Core.Managers.ArticuloTipo>();
            container.RegisterType<Core.ILoteManager, Core.Managers.Lote>();
            container.RegisterType<Core.IUsuarioManager, Core.Managers.Usuario>();
            container.RegisterType<Core.INotificacionesManager, Core.Managers.Notificaciones>();

            container.RegisterType<IValidator<DataMembers.Input.Usuario>, UsuarioValidation>();
            container.RegisterType<IValidator<DataMembers.Input.Lote>, LoteValidation>();
            container.RegisterType<IValidator<DataMembers.Input.Articulo>, ArticuloValidation>();
        }

        private static void RegisterInfrastructure(IUnityContainer container)
        {
            container.RegisterType<Infra.IArticuloInfrastructure, Infra.Repository.Articulo>();
            container.RegisterType<Infra.IArticuloTipoInfrastructure, Infra.Repository.ArticuloTipo>();
            container.RegisterType<Infra.ILoteInfrastructure, Infra.Repository.Lote>();
            container.RegisterType<Infra.IUsuarioInfrastructure, Infra.Repository.Usuario>();
            container.RegisterType<Infra.INotificacionesInfrastructure, Infra.Repository.Notificaciones>();
            container.RegisterType<Infra.Mappers.ITransformMapper, Infra.Mappers.TransformMapper>();
        }

        private static void RegisterDomain(IUnityContainer container)
        {
            container.RegisterType<Dom.IConnectionContext, Dom.ConnectionContext>(new InjectionConstructor("Data Source=.\\SQLEXPRESS;database=ProductsManager;user=usrpm;password=usrpm"));
        }

    }
}
