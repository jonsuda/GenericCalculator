using System.Composition;

namespace GenericCalculator.Operations
{
    [Export(typeof(IOperation<int>))]
    [ExportMetadata("Name", "Division")]
    [ExportMetadata("Symbol", '/')]
    public class DivisionInteger : IOperation<int>
    {
        public int Perform(int a, int b)
        {
            return a / b;
        }
    }
}
