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
using Ecommerce.Test.ModelFactory.Output;

namespace Tests
{
    public class ArticuloTests
    {
        IUnityContainer container;

        [SetUp]
        public void Setup()
        {
            container = UnityConfig.Container;
            MapperConfig.Initialize();
        }

        [Test]
        public void Articulo_GetById_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);
            var target = ArticuloOutputFactory.Get();

            mock.Setup(x => x.GetById(target.Id))
                .Returns((int value) => target);

            var item = articuloManager.GetById(target.Id);

            Assert.AreEqual(target.Activo, item.Activo);
            Assert.AreEqual(target.Descripcion, item.Descripcion);
            Assert.AreEqual(target.Id, item.Id);

            Assert.AreEqual(target.Lote, item.Lote);
            Assert.AreEqual(target.Marca, item.Marca);
            Assert.AreEqual(target.NumeroSerie, item.NumeroSerie);
            Assert.AreEqual(target.Precio, item.Precio);
            Assert.AreEqual(target.Tipo, item.Tipo);
            Assert.AreEqual(target.UsuarioAdjudicado, item.UsuarioAdjudicado);
            Assert.AreEqual(target.UsuariosInteresados, item.UsuariosInteresados);

        }

        [Test]
        public void Articulo_GetById_Fail()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = ArticuloOutputFactory.Get();

            mock.Setup(x => x.GetById(target.Id))
                .Returns(target);

            var art = articuloManager.GetById(target.Id - 1);

            Assert.Null(art);
        }

        [Test]
        public void Articulo_Save_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = ArticuloOutputFactory.Get();
            var articulo = new Member.Input.Articulo
            {
                Descripcion = target.Descripcion,
                IdLote = target.Lote.Id,
                IdTipo = target.Tipo.Id,
                Marca = target.Marca,
                NroSerie = target.NumeroSerie,
                Precio = target.Precio
            };

            mock.Setup(x => x.Get())
                .Returns(ArticuloOutputFactory.GetList());

            mock.Setup(x => x.Save(articulo))
                .Returns(target);

            var result = articuloManager.Save(articulo);

            Assert.AreEqual(target.Descripcion, articulo.Descripcion);
            Assert.AreEqual(target.Lote.Id, articulo.IdLote);
            Assert.AreEqual(target.Tipo.Id, articulo.IdTipo);
            Assert.AreEqual(target.Marca, articulo.Marca);
            Assert.AreEqual(target.NumeroSerie, articulo.NroSerie);
            Assert.AreEqual(target.Precio, articulo.Precio);
        }

        [Test]
        public void Articulo_Get_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = ArticuloOutputFactory.GetList();

            mock.Setup(x => x.Get())
                .Returns(target);

            var result = articuloManager.Get();

            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.First().Activo, target.First().Activo);
            Assert.AreEqual(result.First().Descripcion, target.First().Descripcion);
            Assert.AreEqual(result.First().Id, target.First().Id);
            Assert.AreEqual(result.First().Lote, target.First().Lote);
            Assert.AreEqual(result.First().Marca, target.First().Marca);
            Assert.AreEqual(result.First().NumeroSerie, target.First().NumeroSerie);
            Assert.AreEqual(result.First().Precio, target.First().Precio);
            Assert.AreEqual(result.First().Tipo, target.First().Tipo);
            Assert.AreEqual(result.First().UsuarioAdjudicado, target.First().UsuarioAdjudicado);
            Assert.AreEqual(result.First().UsuariosInteresados, target.First().UsuariosInteresados);
            Assert.AreEqual(result.First().UsuariosInteresados, target.First().UsuariosInteresados);
        }

        [Test]
        public void Articulo_GetAll_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = ArticuloOutputFactory.GetList();

            mock.Setup(x => x.GetAll())
                .Returns(target);

            var result = articuloManager.GetAll();

            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.First().Activo, target.First().Activo);
            Assert.AreEqual(result.First().Descripcion, target.First().Descripcion);
            Assert.AreEqual(result.First().Id, target.First().Id);
            Assert.AreEqual(result.First().Lote, target.First().Lote);
            Assert.AreEqual(result.First().Marca, target.First().Marca);
            Assert.AreEqual(result.First().NumeroSerie, target.First().NumeroSerie);
            Assert.AreEqual(result.First().Precio, target.First().Precio);
            Assert.AreEqual(result.First().Tipo, target.First().Tipo);
            Assert.AreEqual(result.First().UsuarioAdjudicado, target.First().UsuarioAdjudicado);
            Assert.AreEqual(result.First().UsuariosInteresados, target.First().UsuariosInteresados);
            Assert.AreEqual(result.First().UsuariosInteresados, target.First().UsuariosInteresados);
        }

        [Test]
        public void Articulo_GetLote_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = ArticuloOutputFactory.GetList();

            mock.Setup(x => x.GetByLote(target.First().Id))
                .Returns(target);

            var result = articuloManager.GetLote(target.First().Id);

            Assert.IsTrue(result.Any());
            Assert.AreEqual(result.First().Activo, target.First().Activo);
            Assert.AreEqual(result.First().Descripcion, target.First().Descripcion);
            Assert.AreEqual(result.First().Id, target.First().Id);
            Assert.AreEqual(result.First().Lote, target.First().Lote);
            Assert.AreEqual(result.First().Marca, target.First().Marca);
            Assert.AreEqual(result.First().NumeroSerie, target.First().NumeroSerie);
            Assert.AreEqual(result.First().Precio, target.First().Precio);
            Assert.AreEqual(result.First().Tipo, target.First().Tipo);
            Assert.AreEqual(result.First().UsuarioAdjudicado, target.First().UsuarioAdjudicado);
            Assert.AreEqual(result.First().UsuariosInteresados, target.First().UsuariosInteresados);
            Assert.AreEqual(result.First().UsuariosInteresados, target.First().UsuariosInteresados);
        }

        [Test]
        public void Articulo_PostularArticulo_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = new Member.Input.ArticuloPostulacion
            {
                IdArticulo = 32,
                IdUsuario = 17
            };

            mock.Setup(x => x.Postular(target));

            mock.Setup(x => x.ExistsPostulacion(target))
                .Returns(false);

            var result = articuloManager.PostularArticulo(target);

            Assert.IsTrue(result);
        }

        [Test]
        public void Articulo_DeclinarPostulacionArticulo_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = new Member.Input.ArticuloPostulacion
            {
                IdArticulo = 32,
                IdUsuario = 17
            };

            mock.Setup(x => x.DeclinarPostulacion(target));

            mock.Setup(x => x.ExistsPostulacion(target))
                .Returns(false);

            var result = articuloManager.PostularArticulo(target);

            Assert.IsTrue(result);
        }

        [Test]
        public void Articulo_Enable_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();

            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = ArticuloOutputFactory.Get();
            target.Activo = false;

            mock.Setup(x => x.ChangeStatus(target.Id));

            mock.Setup(x => x.GetById(target.Id))
                .Returns(target);

            var result = articuloManager.Enable(target.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Articulo_Disable_Assert()
        {
            var mock = new Mock<Infra.IArticuloInfrastructure>();
            var articuloManager = new Core.Managers.Articulo(mock.Object);

            var target = ArticuloOutputFactory.Get();

            mock.Setup(x => x.ChangeStatus(target.Id));

            mock.Setup(x => x.GetById(target.Id))
                .Returns(target);

            var result = articuloManager.Disable(target.Id);

            Assert.IsTrue(result);
        }
    }
}