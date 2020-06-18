using Ecommerce.Core.Services;
using Ecommerce.Test;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Core = Ecommerce.Core;
using Infra = Ecommerce.Infrastructure;
using Member = Ecommerce.Common.DataMembers;

namespace Ecommerce.Test
{
    class NotificacionesTest
    {
        IUnityContainer container;

        [SetUp]
        public void Setup()
        {
            container = UnityConfig.Container;
            MapperConfig.Initialize();
        }

        [Test]
        public void Noticia_GetByUser_Assert()
        {
            var notificacionesManager = container.Resolve<Core.INotificacionesManager>();
            var mockNotificaciones = new Mock<Infra.INotificacionesInfrastructure>();

            mockNotificaciones.Setup(x => x.GetByUser(It.IsAny<string>()));


            notificacionesManager.GetByUser("2");

            Assert.IsTrue(true);
        }

        [Test]
        public void Noticia_RecordReading_Assert()
        {
            var notificacionesManager = container.Resolve<Core.INotificacionesManager>();
            var mockNotificaciones = new Mock<Infra.INotificacionesInfrastructure>();

            mockNotificaciones.Setup(x => x.RecordReading(It.IsAny<string>()));

            notificacionesManager.RecordReading("2");

            Assert.IsTrue(true);
        }

        [Test]
        public void Noticia_Email_Assert()
        {
            var notificacion = new Notifications();
            notificacion.SendRecoverPassword("pdefilippis@gmail.com", "77777");
        }
    }
}
