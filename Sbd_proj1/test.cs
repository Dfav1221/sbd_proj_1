using Repository;

namespace Sbd_proj1
{
    public class test
    {
        private readonly ICounter _counter;

        public test(ICounter counter)
        {
            _counter = counter;
        }

        public test()
        {
            _counter.Increment();
            
            _counter.Increment();

            _counter.Increment();
            
            System.Console.WriteLine(_counter.GetCount());
        }
    }
}