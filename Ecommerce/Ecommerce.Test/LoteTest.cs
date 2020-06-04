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
    public class LoteTest
    {
        IUnityContainer container;

        [SetUp]
        public void Setup()
        {
            container = UnityConfig.Container;
            MapperConfig.Initialize();
        }

        [Test]
        public void Articulo_Enable_Assert()
        {
            var loteMock = new Mock<Infra.ILoteInfrastructure>();
            var loteManager = new Core.Managers.Lote(loteMock.Object, null, null, null);

            var lote = LoteOutputFactory.Get();
            lote.Activo = false;

            loteMock.Setup(x => x.GetById(lote.Id)).Returns(lote);
            loteMock.Setup(x => x.ChangeStatus(lote.Id));

            var result = loteManager.Enable(lote.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Articulo_Disable_Assert()
        {
            var loteMock = new Mock<Infra.ILoteInfrastructure>();
            var loteManager = new Core.Managers.Lote(loteMock.Object, null, null, null);

            var lote = LoteOutputFactory.Get();

            loteMock.Setup(x => x.GetById(lote.Id)).Returns(lote);
            loteMock.Setup(x => x.ChangeStatus(lote.Id));

            var result = loteManager.Disable(lote.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Articulo_Get_Assert()
        {
            var mock = new Mock<Infra.ILoteInfrastructure>();
            var articuloTipoManager = new Core.Managers.Lote(mock.Object, null, null, null);

            var lotes = LoteOutputFactory.GetList();

            mock.Setup(x => x.Get()).Returns(lotes);

            var items = articuloTipoManager.Get();

            Assert.AreEqual(lotes, items);
        }

        [Test]
        public void Articulo_GetById_Assert()
        {
            var mock = new Mock<Infra.ILoteInfrastructure>();
            var articuloTipoManager = new Core.Managers.Lote(mock.Object, null, null, null);

            var lote = LoteOutputFactory.Get();

            mock.Setup(x => x.GetById(lote.Id)).Returns(lote);

            var item = articuloTipoManager.GetById(lote.Id);

            Assert.AreEqual(lote, item);
        }

        [Test]
        public void Articulo_Save_Assert()
        {
            var mock = new Mock<Infra.ILoteInfrastructure>();
            var loteManager = new Core.Managers.Lote(mock.Object, null, null, null);

            var lote = LoteOutputFactory.Get();

            var inputLote = new Member.Input.Lote
            {
                Descripcion = lote.Descripcion,
                Id = lote.Id,
                Imagen = lote.Imagen,
                NombreImagen = lote.NombreImagen
            };

            mock.Setup(x => x.Save(inputLote)).Returns(lote);
            mock.Setup(x => x.GetByDescripcion(inputLote.Descripcion)).Returns(LoteOutputFactory.GetList());

            var item = loteManager.Save(inputLote);

            Assert.AreEqual(lote, item);

        }

        [Test]
        public void Articulo_Sorteo_Assert()
        {
            var mockLote = new Mock<Infra.ILoteInfrastructure>();
            var mockArticulo = new Mock<Infra.IArticuloInfrastructure>();
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var mockNotificaciones = new Mock<Infra.INotificacionesInfrastructure>();
            var loteoManager = new Core.Managers.Lote(mockLote.Object, mockArticulo.Object, mockUsuario.Object, mockNotificaciones.Object);

            var lote = LoteOutputFactory.Get();

            var articulos = ArticuloOutputFactory.GetList();
            var usuarios = UsuarioOutputFactory.GetList();

            mockArticulo.Setup(x => x.GetByLote(lote.Id)).Returns(articulos);
            mockUsuario.Setup(x => x.GetByArticulo(articulos.FirstOrDefault().Id)).Returns(usuarios);
            
            mockArticulo.Setup(x => x.AdjudicarArticulo(It.IsAny<int>(), It.IsAny<int>()));

            mockNotificaciones.Setup(x => x.Create(It.IsAny<Common.DataMembers.Input.Notificacion>()));
            mockLote.Setup(x => x.ChangeStatus(It.IsAny<int>(),It.IsAny<string>()));

            var result = loteoManager.Sorteo(lote.Id);
            Assert.AreEqual(usuarios, result);
        }

        [Test]
        public void Articulo_GetAll_Assert()
        {
            var mock = new Mock<Infra.ILoteInfrastructure>();
            var loteManager = new Core.Managers.Lote(mock.Object, null, null, null);

            var lotes = LoteOutputFactory.GetList();

            mock.Setup(x => x.GetAll()).Returns(lotes);

            var items = loteManager.GetAll();

            Assert.AreEqual(lotes, items);
        }

        [Test]
        public void Articulo_Open_Assert()
        {
            var mock = new Mock<Infra.ILoteInfrastructure>();
            var loteManager = new Core.Managers.Lote(mock.Object, null, null, null);

            var lote = LoteOutputFactory.Get();

            mock.Setup(x => x.ChangeStatus(lote.Id, It.IsAny<string>()));

            var result = loteManager.Open(lote.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Articulo_Close_Assert()
        {
            var mock = new Mock<Infra.ILoteInfrastructure>();
            var articuloTipoManager = new Core.Managers.Lote(mock.Object, null, null, null);

            var lote = LoteOutputFactory.Get();

            mock.Setup(x => x.ChangeStatus(lote.Id));

            var result = articuloTipoManager.Close(lote.Id);

            Assert.IsTrue(result);
          
        }
    }
}
