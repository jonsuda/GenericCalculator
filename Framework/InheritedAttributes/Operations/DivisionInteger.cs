namespace GenericCalculator.Operations
{
    [Operation(typeof(int), "Division", '/')]
    public class DivisionInteger : IOperation<int>
    {
        public int Perform(int a, int b)
        {
            return a / b;
        }
    }
}
