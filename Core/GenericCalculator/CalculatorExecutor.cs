using System;
using System.Composition.Hosting;

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
        /// <param name="configuration">
        /// The Managed Extensibility Framework container configuration set up to include
        /// the operations exported by the calling project.
        /// </param>
        public static void ExecuteCalculator(ContainerConfiguration configuration)
        {
            configuration.WithAssembly(typeof(CalculatorExecutor).Assembly);
            using(CompositionHost container = configuration.CreateContainer())
            {
                PerformOperations(container, 751, 129);
                PerformOperations(container, 3883.91d, 191.83d);
            };
            Console.ReadLine();
        }

        private static void PerformOperations<T>(CompositionHost container, T a, T b)
        {
            container
                .GetExport<Calculator<T>>()
                .PerformOperations(a, b);
        }
    }
}
