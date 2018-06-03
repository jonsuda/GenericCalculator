namespace GenericCalculator.Operations
{
    [Operation(typeof(int), "Subtraction", '-')]
    [Operation(typeof(double), "Subtraction", '-')]
    public class Subtraction : IOperation<int>, IOperation<double>
    {
        public int Perform(int a, int b)
        {
            return a - b;
        }

        public double Perform(double a, double b)
        {
            return a - b;
        }
    }
}
