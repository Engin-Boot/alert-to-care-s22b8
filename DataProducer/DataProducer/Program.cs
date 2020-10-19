

using System;

namespace DataProducer
{
    class Program
    {
        static void Main()
        {
            try
            {
                while (true)
                {
                    DataReader dataReader = new DataReader();
                    dataReader.ReadDataAndPost();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
