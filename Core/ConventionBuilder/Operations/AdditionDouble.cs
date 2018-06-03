namespace GenericCalculator.Operations
{
    [Operation("Addition", '+')]
    public class AdditionDouble : IOperation<double>
    {
        public double Perform(double a, double b)
        {
            return a + b;
        }
    }
}
