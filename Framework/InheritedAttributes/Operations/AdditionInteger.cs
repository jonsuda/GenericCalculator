namespace GenericCalculator.Operations
{
    [Operation(typeof(int), "Addition", '+')]
    public class AdditionInteger : IOperation<int>
    {
        public int Perform(int a, int b)
        {
            return a + b;
        }
    }
}
