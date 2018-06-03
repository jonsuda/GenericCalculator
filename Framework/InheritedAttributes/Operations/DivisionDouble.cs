namespace GenericCalculator.Operations
{
    [Operation(typeof(double), "Division", '/')]
    public class DivisionDouble : IOperation<double>
    {
        public double Perform(double a, double b)
        {
            return a / b;
        }
    }
}
