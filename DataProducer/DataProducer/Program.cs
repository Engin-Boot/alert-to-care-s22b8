

namespace DataProducer
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                DataReader dataReader = new DataReader();
                dataReader.ReadDataAndPost();
            }
        }
    }
}
