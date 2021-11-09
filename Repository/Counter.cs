namespace Repository
{
    public interface ICounter
    {
        void Increment();
        int GetCount();
    }
    
    public class Counter : ICounter
    {
        private int _count = 0;

        public void Increment()
        {
            _count += 1;
        }

        public int GetCount()
        {
            return _count;
        }
    }
}