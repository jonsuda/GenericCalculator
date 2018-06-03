using System.ComponentModel.Composition;

namespace GenericCalculator.Operations
{
    [Export(typeof(IOperation<int>))]
    [Export(typeof(IOperation<double>))]
    [ExportMetadata("Name", "Subtraction")]
    [ExportMetadata("Symbol", '-')]
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
