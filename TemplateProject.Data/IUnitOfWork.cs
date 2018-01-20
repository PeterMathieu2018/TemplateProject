using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateProject.Data
{
    public interface IUnitOfWork
    {
        void Complete();
    }
}
