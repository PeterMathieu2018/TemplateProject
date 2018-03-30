using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Specialized;
using System.Web;
using System.Collections;
using System.Security.Principal;
using DonorPortal.IntegrationTest.Fakes;

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

        //Methods to mock controller with overloads for fake session object, request params, and query string.
        public static void GetMockControllerContext(this Controller controller)
        {
            var contextMock = new Mock<ControllerContext>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var session = new Mock<HttpSessionStateBase>();

            mockHttpContext.Setup(ctx => ctx.Session).Returns(session.Object);
            mockHttpContext.Setup(ctx => ctx.User)
                .Returns(new GenericPrincipal(new GenericIdentity("rami"), new string[0]));

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection
            {
                {"X-Requested-With", "XMLHttpRequest"}
            });
            request.SetupGet(i => i.UrlReferrer).Returns(new Uri(""));
            request.Setup(i => i.Url).Returns(new Uri(""));
            request.Setup(i => i.Browser.IsMobileDevice).Returns(false);
            request.Setup(i => i.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection
            {
                { "SERVER_NAME", "localhost" },
                { "SCRIPT_NAME", "localhost" },
                { "SERVER_PORT", "80" },
                { "HTTPS", "" },
                { "REMOTE_ADDR", "10.1.9.28" },
                { "REMOTE_HOST", "10.1.9.28" }
            });
            request.Setup(i => i.UserAgent).Returns("Browser");

            var browser = new HttpBrowserCapabilities
            {
                Capabilities = new Hashtable { { string.Empty, "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36" } }
            };
            request.Setup(i => i.Browser).Returns(new HttpBrowserCapabilitiesWrapper(browser));
            request.Setup(i => i.Browser.MajorVersion).Returns(100);
            request.Setup(i => i.Browser.Browser).Returns("Chrome");

            mockHttpContext.SetupGet(i => i.Request).Returns(request.Object);

            contextMock.Setup(ctx => ctx.HttpContext).Returns(mockHttpContext.Object);

            controller.ControllerContext = contextMock.Object;
        }
        public static void GetMockControllerContextFakeSession(this Controller controller)
        {
            var contextMock = new Mock<ControllerContext>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var session = new FakeHttpSession();

            mockHttpContext.Setup(ctx => ctx.Session).Returns(session);

            //add username
            mockHttpContext.Setup(ctx => ctx.User)
                .Returns(new GenericPrincipal(new GenericIdentity(""), new string[0]));

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection
            {
                    {"X-Requested-With", "XMLHttpRequest"}
            });

            request.SetupGet(i => i.UrlReferrer).Returns(new Uri(""));
            request.Setup(i => i.Url).Returns(new Uri(""));
            request.Setup(i => i.Browser.IsMobileDevice).Returns(false);
            request.Setup(i => i.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection{
                { "SERVER_NAME", "localhost" },
                { "SCRIPT_NAME", "localhost" },
                { "SERVER_PORT", "80" },
                { "HTTPS", "" },
                { "REMOTE_ADDR", "10.1.9.28" },
                { "REMOTE_HOST", "10.1.9.28" }
              });
            request.Setup(i => i.UserAgent).Returns("Browser");

            var browser = new HttpBrowserCapabilities
            {
                Capabilities = new Hashtable { { string.Empty, "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36" } }
            };

            request.Setup(i => i.Browser).Returns(new HttpBrowserCapabilitiesWrapper(browser));
            request.Setup(i => i.Browser.MajorVersion).Returns(100);
            request.Setup(i => i.Browser.Browser).Returns("Chrome");

            mockHttpContext.SetupGet(i => i.Request).Returns(request.Object);

            contextMock.Setup(ctx => ctx.HttpContext).Returns(mockHttpContext.Object);

            controller.ControllerContext = contextMock.Object;
        }
        public static void GetMockControllerContext(this Controller controller, NameValueCollection paramDictionary)
        {
            var contextMock = new Mock<ControllerContext>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var session = new FakeHttpSession();

            mockHttpContext.Setup(ctx => ctx.Session).Returns(session);

            mockHttpContext.Setup(ctx => ctx.User)
                .Returns(new GenericPrincipal(new GenericIdentity(""), new string[0]));

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(
                new System.Net.WebHeaderCollection {
        {"X-Requested-With", "XMLHttpRequest"}
                });



            request.SetupGet(i => i.UrlReferrer).Returns(new Uri(""));
            request.Setup(i => i.Url).Returns(new Uri(""));
            request.Setup(i => i.Browser.IsMobileDevice).Returns(false);
            request.Setup(i => i.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection{
                { "SERVER_NAME", "localhost" },
                { "SCRIPT_NAME", "localhost" },
                { "SERVER_PORT", "80" },
                { "HTTPS", "" },
                { "REMOTE_ADDR", "10.1.9.28" },
                { "REMOTE_HOST", "10.1.9.28" }
            });
            var browser = new HttpBrowserCapabilities
            {
                Capabilities = new Hashtable { { string.Empty, "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36" } }
            };

            request.Setup(i => i.Browser).Returns(new HttpBrowserCapabilitiesWrapper(browser));
            request.Setup(i => i.Browser.MajorVersion).Returns(100);
            request.Setup(i => i.UserAgent).Returns("Browser");
            request.Setup(i => i.Browser.Browser).Returns("Chrome");

            request.SetupGet(i => i.Params).Returns(paramDictionary);

            mockHttpContext.SetupGet(i => i.Request).Returns(request.Object);

            contextMock.Setup(ctx => ctx.HttpContext).Returns(mockHttpContext.Object);

            controller.ControllerContext = contextMock.Object;
        }
        public static void GetMockControllerContext(this Controller controller, NameValueCollection paramDictionary, NameValueCollection queryString)
        {
            var contextMock = new Mock<ControllerContext>();
            var mockHttpContext = new Mock<HttpContextBase>();
            var session = new FakeHttpSession();

            mockHttpContext.Setup(ctx => ctx.Session).Returns(session);

            mockHttpContext.Setup(ctx => ctx.User)
                .Returns(new GenericPrincipal(new GenericIdentity(""), new string[0]));

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(new System.Net.WebHeaderCollection
            {
                {"X-Requested-With", "XMLHttpRequest"}
            });

            request.SetupGet(i => i.UrlReferrer).Returns(new Uri(""));
            request.Setup(i => i.Url).Returns(new Uri(""));
            request.Setup(i => i.Browser.IsMobileDevice).Returns(false);
            request.Setup(i => i.ServerVariables).Returns(new System.Collections.Specialized.NameValueCollection
            {
                { "SERVER_NAME", "localhost" },
                { "SCRIPT_NAME", "localhost" },
                { "SERVER_PORT", "80" },
                { "HTTPS", "" },
                { "REMOTE_ADDR", "10.1.9.28" },
                { "REMOTE_HOST", "10.1.9.28" }
            });
            var browser = new HttpBrowserCapabilities
            {
                Capabilities = new Hashtable { { string.Empty, "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36" } }
            };

            request.Setup(i => i.Browser).Returns(new HttpBrowserCapabilitiesWrapper(browser));
            request.Setup(i => i.Browser.MajorVersion).Returns(100);
            request.Setup(i => i.Browser.Browser).Returns("Chrome");
            request.Setup(i => i.UserAgent).Returns("Browser");

            request.SetupGet(i => i.Form).Returns(queryString);
            request.SetupGet(i => i.QueryString).Returns(queryString);
            request.SetupGet(i => i.Params).Returns(paramDictionary);

            mockHttpContext.SetupGet(i => i.Request).Returns(request.Object);

            contextMock.Setup(ctx => ctx.HttpContext).Returns(mockHttpContext.Object);

            controller.ControllerContext = contextMock.Object;
        }
    }
}
