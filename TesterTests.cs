using Microsoft.VisualStudio.TestTools.UnitTesting;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;

namespace DB.Tests
{
    [TestClass()]
    public class TesterTests
    {
        [TestMethod()]
        public void TestMethodTest()
        {

            TestDatabaseEntities context = new TestDatabaseEntities();
            context.Entidade = new Mock<DbSet<Entidade>>().Object;

            List<Entidade> entidades = new List<Entidade>()
            {
                new Entidade() { Id = 1, Propriedade ="1" },
                new Entidade() { Id = 2, Propriedade = "2" }
            };

            IQueryable<Entidade> queryableEntidade = entidades.AsQueryable();

            var mockDbSet = MockDbSet<Entidade>(
                new Entidade() { Id = 1, Propriedade = "1" },
                new Entidade() { Id = 2, Propriedade = "2" }
            );

            context.Entidade = mockDbSet;

           // var tester = new Tester();

            var result = Tester.TestMethod(context);
            
        }

        public static DbSet<T> MockDbSet<T>(params T[] fakesource) where T: class
        {
            var queryable = fakesource.AsQueryable();
            var dbset = new Mock<DbSet<T>>();

            dbset.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbset.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbset.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbset.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbset.Object;
        }
    }
}