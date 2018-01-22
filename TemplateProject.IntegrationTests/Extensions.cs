using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TemplateProject.IntegrationTests
{
    public static class Extensions
    {
        public static void AssertViewWasReturned(this ActionResult result, string viewName, string defaultViewName)
        {
            if (result.GetType() == typeof(ViewResultBase))
            {
                Assert.IsInstanceOfType(result, typeof(ViewResultBase));
                var viewResult = (ViewResultBase)result;

                var actualViewName = viewResult.ViewName;

                if (actualViewName == "")
                    actualViewName = defaultViewName;

                Assert.AreEqual(viewName, actualViewName,
                    string.Format("Expected a View named{0}, got a View named {1}", viewName, actualViewName));
            }
            else if (result.GetType() == typeof(ViewResult))
            {
                Assert.IsInstanceOfType(result, typeof(ViewResult));
                var viewResult = (ViewResult)result;

                var actualViewName = viewResult.ViewName;

                if (actualViewName == "")
                    actualViewName = defaultViewName;

                Assert.AreEqual(viewName, actualViewName,
                string.Format("Expected a View named{0}, got a View named {1}", viewName, actualViewName));
            }
            else if (result.GetType() == typeof(RedirectToRouteResult))
            {
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                var viewResult = (RedirectToRouteResult)result;

                var actualViewName = viewResult.RouteValues.FirstOrDefault(i => i.Key == "action").Value;

                Assert.AreEqual(viewName, actualViewName,
                    string.Format("Expected a View named{0}, got a View named {1}", viewName, actualViewName));
            }
        }

        public static void AssertViewWasReturned(this Task<ActionResult> result, string viewName, string defaultViewName)
        {
            if (result.GetType() == typeof(ViewResultBase))
            {
                Assert.IsInstanceOfType(result, typeof(ViewResultBase));
                var viewResult = (ViewResultBase)result.Result;

                var actualViewName = viewResult.ViewName;

                if (actualViewName == "")
                    actualViewName = defaultViewName;

                Assert.AreEqual(viewName, actualViewName,
                    string.Format("Expected a View named{0}, got a View named {1}", viewName, actualViewName));
            }
            else if (result.GetType() == typeof(ViewResult))
            {
                Assert.IsInstanceOfType(result, typeof(ViewResult));
                var viewResult = (ViewResult)result.Result;

                var actualViewName = viewResult.ViewName;

                if (actualViewName == "")
                    actualViewName = defaultViewName;

                Assert.AreEqual(viewName, actualViewName,
                string.Format("Expected a View named{0}, got a View named {1}", viewName, actualViewName));
            }
            else if (result.GetType() == typeof(RedirectToRouteResult))
            {
                Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
                var viewResult = (RedirectToRouteResult)result.Result;

                var actualViewName = viewResult.RouteValues.FirstOrDefault(i => i.Key == "action").Value;

                Assert.AreEqual(viewName, actualViewName,
                    string.Format("Expected a View named{0}, got a View named {1}", viewName, actualViewName));
            }
        }

        public static Mock<DbSet<T>> GetQueryableMockDbSet<T>(this IEnumerable<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbSet;
        }
    }
}
