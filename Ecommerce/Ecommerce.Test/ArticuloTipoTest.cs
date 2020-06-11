using Ecommerce.Test;
using Ecommerce.Test.ModelFactory.Output;
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
    public class ArticuloTipoTest
    {
        IUnityContainer container;

        [SetUp]
        public void Setup()
        {
            container = UnityConfig.Container;
            MapperConfig.Initialize();
        }

        [Test]
        public void ArticuloTipo_Get_Assert()
        {
            var mock = new Mock<Infra.IArticuloTipoInfrastructure>();
            var articuloTipoManager = new Core.Managers.ArticuloTipo(mock.Object);

            var items = ArticuloTipoOutputFactory.GetList();

            mock.Setup(x => x.Get()).
                Returns(items);

            var result = articuloTipoManager.Get();

            Assert.AreEqual(items.First().Codigo, result.First().Codigo);
            Assert.AreEqual(items.First().Descripcion, result.First().Descripcion);
            Assert.AreEqual(items.First().Id, result.First().Id);
        }

        [Test]
        public void ArticuloTipo_GetByCodigo_Assert()
        {
            var mock = new Mock<Infra.IArticuloTipoInfrastructure>();
            var articuloTipoManager = new Core.Managers.ArticuloTipo(mock.Object);

            var item = ArticuloTipoOutputFactory.Get();

            mock.Setup(x => x.GetByCodigo(item.Codigo)).
                Returns(item);

            var result = articuloTipoManager.GetByCodigo(item.Codigo);

            Assert.AreEqual(item.Codigo, result.Codigo);
            Assert.AreEqual(item.Descripcion, result.Descripcion);
            Assert.AreEqual(item.Id, result.Id);
        }

        [Test]
        public void ArticuloTipo_GetById_Assert()
        {
            var mock = new Mock<Infra.IArticuloTipoInfrastructure>();
            var articuloTipoManager = new Core.Managers.ArticuloTipo(mock.Object);

            var item = ArticuloTipoOutputFactory.Get();

            mock.Setup(x => x.GetById(item.Id)).
                Returns(item);

            var result = articuloTipoManager.GetById(item.Id);

            Assert.AreEqual(item.Codigo, result.Codigo);
            Assert.AreEqual(item.Descripcion, result.Descripcion);
            Assert.AreEqual(item.Id, result.Id);
        }
    }
}
