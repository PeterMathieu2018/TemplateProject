
using System;
using ;
using ;

namespace
{
    public interface IUnitOfWork : IUnitOfWork
    {

        //Change this to the context type
        IContext Context();
        void Complete();

    }
}
