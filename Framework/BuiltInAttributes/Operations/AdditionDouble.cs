using System.ComponentModel.Composition;

namespace GenericCalculator.Operations
{
    [Export(typeof(IOperation<double>))]
    [ExportMetadata("Name", "Addition")]
    [ExportMetadata("Symbol", '+')]
    public class AdditionDouble : IOperation<double>
    {
        public double Perform(double a, double b)
        {
            return a + b;
        }
    }
}
