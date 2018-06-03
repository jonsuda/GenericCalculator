namespace GenericCalculator.Operations
{
    [Operation("Multiplication", 'x')]
    public class Multiplication : IOperation<int>, IOperation<double>
    {
        public int Perform(int a, int b)
        {
            return a * b;
        }

        public double Perform(double a, double b)
        {
            return a * b;
        }
    }
}
