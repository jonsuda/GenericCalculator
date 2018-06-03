using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace GenericCalculator
{
    public static class Program
    {
        public static void Main()
        {
            CalculatorExecutor.ExecuteCalculator(CreateCatalog());
        }

        /// <summary>
        /// Creates a <see cref="ComposablePartCatalog" /> instance, loading all of the
        /// project's exports into it.
        /// </summary>
        /// <returns>
        /// A <see cref="ComposablePartCatalog" /> instance containing all of the
        /// project's exports.
        /// </returns>
        private static ComposablePartCatalog CreateCatalog()
        {
            return new AssemblyCatalog(
                typeof(Program).Assembly,
                new CustomAttributeReflectionContext());
        }
    }
}
