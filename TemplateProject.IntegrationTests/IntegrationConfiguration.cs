using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TemplateProject.IntegrationTests
{
    [TestClass]
    public static class IntegrationConfiguration
    {
        //Inject Mock unit of work when do not need to actually update database.
        //public static Mock<UnitOfWork> MockMbtsUnitOfWork;

        [AssemblyInitializeAttribute]
        public static void IntegrationInitialize(TestContext context)
        {
            //var UnitOfWork = new UnitOfWork(new DbContext());


            //var mockContext = new Mock<IDContext>();
            //MockUnitOfWork = new Mock<UnitOfWork>(DbContext.Object);

            //Get queryable mock set from concrete database.
            //var entity =UnitOfWork.Entity.GetAll()..GetQueryableMockDbSet();


            //Set mock context entity to return concrete queryable mock set;
            //mockContext.Setup(i => i.Entity).Returns(entity.Object);

            //Set up each repository in unit of work.
            //var mockRepository =  new Mock<IEntityRepository>();
            //mockRepository.Setup(i => i.Context).Returns(mockContext.Object);
            //MockUnitOfWork.Setup(i => i.Entity).Returns(mockRepository.Object);
            //MockMbtsUnitOfWork.Setup(i => i.Entity.GetAll()).Returns(mockContext.Object.Entity);
            //MockUnitOfWork.Setup(i => i.Entity.FindBy(It.IsAny<Expression<Func<Entity, bool>>>()))
            //.Returns<Expression<Func<Order, bool>>>(i => mockContext.Object.Entity.Where(i));

            Mapper.Initialize(cfg =>
            {
                //Include mappings in global.asax in Web project.
                //example cfg.CreateMap<Entity,EntityDto>();
            });
        }
    }
}
