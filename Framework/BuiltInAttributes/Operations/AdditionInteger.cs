using System.ComponentModel.Composition;

namespace GenericCalculator.Operations
{
    [Export(typeof(IOperation<int>))]
    [ExportMetadata("Name", "Addition")]
    [ExportMetadata("Symbol", '+')]
    public class AdditionInteger : IOperation<int>
    {
        public int Perform(int a, int b)
        {
            return a + b;
        }
    }
}
