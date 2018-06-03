namespace GenericCalculator.Operations
{
    [Operation(typeof(double), "Addition", '+')]
    public class AdditionDouble : IOperation<double>
    {
        public double Perform(double a, double b)
        {
            return a + b;
        }
    }
}
