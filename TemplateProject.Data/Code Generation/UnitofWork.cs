using ;
using ;

namespace
{
    public class UnitOfWork : IUnitOfWork
    {
        private DefaultModel _context;

        //public UnitOfWork(DefaultModel context)
        //{
        // _context = context;
        //}

        //Delete this default constructor if using an IoC container
        public UnitOfWork()
        {
            _context = new DefaultModel();
        }


        public IContext Context()
        {
            return _context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

    }
}
