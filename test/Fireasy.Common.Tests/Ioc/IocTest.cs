﻿using Fireasy.Common.Extensions;
using Fireasy.Common.Ioc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Fireasy.Common.Aop;

namespace Fireasy.Common.Tests.Ioc
{
    public interface IMainService
    {
        string Name { get; set; }
    }

    public class MainService : IMainService
    {
        public string Name { get; set; }
    }

    public class MainServiceSecond : IMainService
    {
        public string Name { get; set; }
    }

    public interface IAClass
    {
        bool HasB { get; }
    }

    public interface IBClass
    {
    }

    public class AClass : IAClass
    {
        private readonly IBClass b;

        public AClass(IBClass b)
        {
            this.b = b;
        }

        /// <summary>
        /// 判断是否包含 <see cref="IBClass"/> 对象。
        /// </summary>
        public bool HasB
        {
            get
            {
                return b != null;
            }
        }
    }

    public class BClass : IBClass
    {
    }
    public interface ICClass
    {
        IDClass D { get; set; }
    }

    public interface IDClass
    {
    }

    public class CClass : ICClass
    {
        [IgnoreInjectProperty]
        public IDClass D { get; set; }
    }

    public class DClass : IDClass
    {
    }

    [TestClass]
    public class IocTest
    {

        [TestMethod]
        public void Test()
        {
            var container = ContainerUnity.GetContainer();

            container
                .Register<IMainService, MainService>()
                .Register(typeof(IMainService), typeof(MainServiceSecond));

            var obj = container.Resolve<IMainService>();

            Console.WriteLine(obj.GetType().Name);
        }

        [TestMethod]
        public void TestSigleton()
        {
            var container = ContainerUnity.GetContainer();
            container.RegisterSingleton<IMainService>(() => new MainService());

            //第一次反转
            var obj1 = container.Resolve<IMainService>();

            //再次反转，还是之前的对象
            var obj2 = container.Resolve<IMainService>();

            Assert.IsTrue(obj1 == obj2);
        }

        [TestMethod]
        public void TestInstance()
        {
            var container = ContainerUnity.GetContainer();
            container.Register<IMainService>(() => new MainService());

            //第一次反转
            var obj1 = container.Resolve<IMainService>();

            //再次反转，得到新的对象
            var obj2 = container.Resolve<IMainService>();

            Assert.IsTrue(obj1 != obj2);
        }

        [TestMethod]
        public void TestInitializer()
        {
            var container = ContainerUnity.GetContainer();

            container.Register<IMainService, MainService>();
            container.RegisterInitializer<IMainService>(s => s.Name = "fireasy");

            var obj = container.Resolve<IMainService>();
            Assert.AreEqual("fireasy", obj.Name);
        }

        [TestMethod]
        public void TestAssembly()
        {
            var container = ContainerUnity.GetContainer();
            container.RegisterAssembly(typeof(IocTest).Assembly);

            var obj = container.Resolve<IMainService>();

            Console.WriteLine(obj.GetType().Name);
        }

        [TestMethod]
        public void TestAssemblyName()
        {
            var container = ContainerUnity.GetContainer();
            container.RegisterAssembly("Fireasy.Common.Tests");

            var obj = container.Resolve<IMainService>();

            Console.WriteLine(obj.GetType().Name);
        }

        [TestMethod]
        public void TestConstructorInjection()
        {
            var container = ContainerUnity.GetContainer();

            container.Register<IAClass, AClass>();
            container.Register<IBClass, BClass>();

            var a = container.Resolve<IAClass>();

            Assert.IsTrue(a.HasB);
        }

        [TestMethod]
        public void TestPropertyInjection()
        {
            var container = ContainerUnity.GetContainer();

            container.Register<ICClass, CClass>();
            container.Register<IDClass, DClass>();

            var c = container.Resolve<ICClass>();

            Assert.IsNotNull(c.D);
        }

        [TestMethod]
        public void TestIgnoreInjectProperty()
        {
            var container = ContainerUnity.GetContainer();

            container.Register<ICClass, CClass>();
            container.Register<IDClass, DClass>();

            var c = container.Resolve<ICClass>();

            Assert.IsNull(c.D);
        }

        [TestMethod]
        public void TestConfig()
        {
            //查找config目录下的 *.ioc.config 文件
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
            var container = ContainerUnity.GetContainer(path, "*.ioc.xml");
            var service = container.Resolve<IMainService>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void TestLoadConfig()
        {
            var container = ContainerUnity.GetContainer();

            //查找config目录下的 *.ioc.config 文件
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config");
            container.Config(path, "*.ioc.xml");

            //如果是加载指定的文件
            //container.Config(Path.Combine(path, "sample.ioc.xml"));

            var service = container.Resolve<IMainService>();
            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void TestContainers()
        {
            var c1 = ContainerUnity.GetContainer("c1");
            var c2 = ContainerUnity.GetContainer("c2");

            var b1 = c1.Resolve<IBClass>();
            var b2 = c2.Resolve<IBClass>();
            var b3 = c1.Resolve<IBClass>();

            Assert.IsNotNull(b1);
            Assert.IsNotNull(b2);

            Assert.IsTrue(b1 == b3); //同一容器反转的两个对象
            Assert.IsFalse(b1 == b2); //不同容器的两个对象
        }

        [IgnoreRegister]
        public interface IAopBase
        {
            void Test();
        }

        [Intercept(typeof(AopInterceptor))]
        public class AopImpl : IAopBase, IAopSupport
        {
            public AopImpl(IAClass a)
            {
                Console.WriteLine(a);
            }

            public virtual void Test()
            {
            }
        }

        public class AopInterceptor : Aop.IInterceptor
        {
            public void Initialize(InterceptContext context)
            {
            }

            public void Intercept(InterceptCallInfo info)
            {
                Console.WriteLine(info.InterceptType);
            }
        }

        [TestMethod]
        public void TestAop()
        {
            var container = ContainerUnity.GetContainer();

            container
                .Register<IAopBase, AopImpl>()
                .Register<IAClass, AClass>();
            //.Register<IBClass, BClass>();

            var obj = container.Resolve<IAopBase>();
            obj.Test();
        }
    }
}
