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
    class UsuarioTest
    {
        IUnityContainer container;

        [SetUp]
        public void Setup()
        {
            container = UnityConfig.Container;
            MapperConfig.Initialize();
        }

        [Test]
        public void Usuario_Disable_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.Get();

            mockUsuario.Setup(x => x.GetById(target.Id)).Returns(target);
            mockUsuario.Setup(x => x.ChangeStatus(target.Id));

            var result = usuarioManager.Disable(target.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Usuario_Enable_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.Get();
            target.Activo = false;
            mockUsuario.Setup(x => x.GetById(target.Id)).Returns(target);
            mockUsuario.Setup(x => x.ChangeStatus(target.Id));

            var result = usuarioManager.Enable(target.Id);

            Assert.IsTrue(result);
        }

        [Test]
        public void Usuario_Get_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.GetList();

            mockUsuario.Setup(x => x.Get()).Returns(target);

            var items = usuarioManager.Get();

            Assert.IsTrue(items.Any());
            Assert.AreEqual(items.First().Acciones, target.First().Acciones);
            Assert.AreEqual(items.First().Activo, target.First().Activo);
            Assert.AreEqual(items.First().Apellido, target.First().Apellido);
            Assert.AreEqual(items.First().Creacion, target.First().Creacion);
            Assert.AreEqual(items.First().Email, target.First().Email);
            Assert.AreEqual(items.First().EsAdministrador, target.First().EsAdministrador);
            Assert.AreEqual(items.First().Id, target.First().Id);
            Assert.AreEqual(items.First().Nombre, target.First().Nombre);
            Assert.AreEqual(items.First().Password, target.First().Password);
            Assert.AreEqual(items.First().UltimoIngreso, target.First().UltimoIngreso);
            Assert.AreEqual(items.First().UserName, target.First().UserName);
        }

        [Test]
        public void Usuario_GetAll_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.GetList();

            mockUsuario.Setup(x => x.GetAll()).Returns(target);

            var items = usuarioManager.GetAll();

            Assert.IsTrue(items.Any());
            Assert.AreEqual(items.First().Acciones, target.First().Acciones);
            Assert.AreEqual(items.First().Activo, target.First().Activo);
            Assert.AreEqual(items.First().Apellido, target.First().Apellido);
            Assert.AreEqual(items.First().Creacion, target.First().Creacion);
            Assert.AreEqual(items.First().Email, target.First().Email);
            Assert.AreEqual(items.First().EsAdministrador, target.First().EsAdministrador);
            Assert.AreEqual(items.First().Id, target.First().Id);
            Assert.AreEqual(items.First().Nombre, target.First().Nombre);
            Assert.AreEqual(items.First().Password, target.First().Password);
            Assert.AreEqual(items.First().UltimoIngreso, target.First().UltimoIngreso);
            Assert.AreEqual(items.First().UserName, target.First().UserName);
        }

        [Test]
        public void Usuario_GetById_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.Get();

            mockUsuario.Setup(x => x.GetById(target.Id)).Returns(target);

            var item = usuarioManager.GetById(target.Id);


            Assert.AreEqual(item.Acciones, target.Acciones);
            Assert.AreEqual(item.Activo, target.Activo);
            Assert.AreEqual(item.Apellido, target.Apellido);
            Assert.AreEqual(item.Creacion, target.Creacion);
            Assert.AreEqual(item.Email, target.Email);
            Assert.AreEqual(item.EsAdministrador, target.EsAdministrador);
            Assert.AreEqual(item.Id, target.Id);
            Assert.AreEqual(item.Nombre, target.Nombre);
            Assert.AreEqual(item.Password, target.Password);
            Assert.AreEqual(item.UltimoIngreso, target.UltimoIngreso);
            Assert.AreEqual(item.UserName, target.UserName);
        }

        [Test]
        public void Usuario_GetByName_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.Get();

            mockUsuario.Setup(x => x.Get(target.Nombre)).Returns(target);

            var item = usuarioManager.GetByName(target.Nombre);


            Assert.AreEqual(item.Acciones, target.Acciones);
            Assert.AreEqual(item.Activo, target.Activo);
            Assert.AreEqual(item.Apellido, target.Apellido);
            Assert.AreEqual(item.Creacion, target.Creacion);
            Assert.AreEqual(item.Email, target.Email);
            Assert.AreEqual(item.EsAdministrador, target.EsAdministrador);
            Assert.AreEqual(item.Id, target.Id);
            Assert.AreEqual(item.Nombre, target.Nombre);
            Assert.AreEqual(item.Password, target.Password);
            Assert.AreEqual(item.UltimoIngreso, target.UltimoIngreso);
            Assert.AreEqual(item.UserName, target.UserName);
        }

        [Test]
        public void Usuario_Login_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.Get();

            mockUsuario.Setup(x => x.Get(It.IsAny<string>())).Returns(target);
            mockUsuario.Setup(x => x.RegistrarLogin(target.Id));

            var user = usuarioManager.Login(new Common.DataMembers.Input.Usuario
            {
                Password = "123",
                UserName = target.UserName
            });

            Assert.IsNotNull(user);
        }

        [Test]
        public void Usuario_Register_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.Get();

            mockUsuario.Setup(x => x.Create(It.IsAny<Common.DataMembers.Input.Usuario>())).Returns(target);

            var item = usuarioManager.Register(new Common.DataMembers.Input.Usuario()
            {
                Apellido = target.Apellido,
                Email = target.Email,
                UserName = target.UserName,
                Password = target.Password,
                Id = target.Id,
                Nombre = target.Nombre,
                EsAdministrador = target.EsAdministrador
            });

            Assert.AreEqual(item.Acciones, target.Acciones);
            Assert.AreEqual(item.Activo, target.Activo);
            Assert.AreEqual(item.Apellido, target.Apellido);
            Assert.AreEqual(item.Creacion, target.Creacion);
            Assert.AreEqual(item.Email, target.Email);
            Assert.AreEqual(item.EsAdministrador, target.EsAdministrador);
            Assert.AreEqual(item.Id, target.Id);
            Assert.AreEqual(item.Nombre, target.Nombre);
            Assert.AreEqual(item.Password, target.Password);
            Assert.AreEqual(item.UltimoIngreso, target.UltimoIngreso);
            Assert.AreEqual(item.UserName, target.UserName);
        }

        [Test]
        public void Usuario_Save_Assert()
        {
            var mockUsuario = new Mock<Infra.IUsuarioInfrastructure>();
            var usuarioManager = new Core.Managers.Usuario(mockUsuario.Object);

            var target = UsuarioOutputFactory.Get();

            mockUsuario.Setup(x => x.Save(It.IsAny<Common.DataMembers.Input.Usuario>())).Returns(target);

            var item = usuarioManager.Save(new Common.DataMembers.Input.Usuario()
            {
                Apellido = target.Apellido,
                Email = target.Email,
                UserName = target.UserName,
                Password = target.Password,
                Id = target.Id,
                Nombre = target.Nombre,
                EsAdministrador = target.EsAdministrador
            });

            Assert.AreEqual(item.Acciones, target.Acciones);
            Assert.AreEqual(item.Activo, target.Activo);
            Assert.AreEqual(item.Apellido, target.Apellido);
            Assert.AreEqual(item.Creacion, target.Creacion);
            Assert.AreEqual(item.Email, target.Email);
            Assert.AreEqual(item.EsAdministrador, target.EsAdministrador);
            Assert.AreEqual(item.Id, target.Id);
            Assert.AreEqual(item.Nombre, target.Nombre);
            Assert.AreEqual(item.Password, target.Password);
            Assert.AreEqual(item.UltimoIngreso, target.UltimoIngreso);
            Assert.AreEqual(item.UserName, target.UserName);
        }

        
    }
}
