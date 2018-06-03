using System.Composition.Hosting;

namespace GenericCalculator
{
    public static class Program
    {
        public static void Main()
        {
            CalculatorExecutor.ExecuteCalculator(CreateContainerConfiguration());
        }

        /// <summary>
        /// Creates a <see cref="ContainerConfiguration" /> instance configured to load all
        /// of the project's exports.
        /// </summary>
        /// <returns>
        /// A <see cref="ContainerConfiguration" /> instance configured to load all of the
        /// project's exports.
        /// </returns>
        private static ContainerConfiguration CreateContainerConfiguration()
        {
            return
                new ContainerConfiguration()
                .WithAssembly(
                    typeof(Program).Assembly,
                    new CustomAttributeAttributedModelProvider());
        }
    }
}
