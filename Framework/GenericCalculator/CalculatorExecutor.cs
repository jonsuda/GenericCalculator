using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace GenericCalculator
{
    /// <summary>
    /// This class is the entry point for the application.
    /// </summary>
    public static class CalculatorExecutor
    {
        /// <summary>
        /// Executes the calculator (and, in turn, individual operations) for multiple
        /// operand types.
        /// </summary>
        /// <param name="catalog">
        /// The Managed Extensibility Framework catalog containing the exported operations
        /// to execute.
        /// </param>
        public static void ExecuteCalculator(ComposablePartCatalog catalog)
        {
            using(CompositionContainer container = CreateCompositionContainer(catalog))
            {
                PerformOperations(container, 751, 129);
                PerformOperations(container, 3883.91d, 191.83d);
            };
            Console.ReadLine();
        }

        private static CompositionContainer
            CreateCompositionContainer(ComposablePartCatalog catalog)
        {
            CompositionContainer compositionContainer;
            var aggregateCatalog = new AggregateCatalog();
            aggregateCatalog.Catalogs.Add(
                new AssemblyCatalog(typeof(CalculatorExecutor).Assembly));
            aggregateCatalog.Catalogs.Add(catalog);
            compositionContainer = new CompositionContainer(aggregateCatalog);
            compositionContainer.ComposeParts();
            return compositionContainer;
        }

        private static void PerformOperations<T>(CompositionContainer container, T a, T b)
        {
            container
                .GetExportedValue<Calculator<T>>()
                .PerformOperations(a, b);
        }
    }
}
