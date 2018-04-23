using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace TemplateProject.UI2.Controllers.API
{
    public abstract partial class BaseApiController : ApiController
    {
        /// <summary>
        /// Protected IUnitOfWork _unitOfWork;
        /// </summary>
        protected BaseApiController()
        {
            //_unitOfWork = unitOfWork
        }
    }
}