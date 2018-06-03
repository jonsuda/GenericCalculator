using System;
using System.Collections.Generic;
using System.Composition.Convention;
using System.Composition.Hosting;
using System.Reflection;

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
                    CreateConventionBuilder());
        }

        /// <summary>
        /// Creates the <see cref="ConventionBuilder" /> builder instance to be used in
        /// conjunction with the project's <see cref="ContainerConfiguration" /> instance.
        /// </summary>
        /// <returns>
        /// The <see cref="ConventionBuilder" /> builder instance to be used in
        /// conjunction with the project's <see cref="ContainerConfiguration" /> instance.
        /// </returns>
        /// <remarks>
        /// In a nutshell, the instance returned by this method represents the following
        /// export rules: "For each type defined within the current assembly that has the
        /// <see cref="OperationAttribute" /> applied to it, export all
        /// <see cref="IOperation{T}" /> interfaces that it implements; a class may
        /// implement the interface multiple times - for different generic arguments
        /// (operand types)."
        /// 
        /// In addition, the metadata (i.e., name and symbol) expressed by individual
        /// classes' <see cref="OperationAttribute" /> instances is added accordingly.
        /// </remarks>
        private static ConventionBuilder CreateConventionBuilder()
        {
            var conventionBuilder = new ConventionBuilder();
            Type currentType = null;
            var processedTypes = new HashSet<Type>();
            conventionBuilder
                .ForTypesMatching(IsOperationType)
                .ExportInterfaces(IsOperationInterface, ExportInterface);
            return conventionBuilder;

            bool IsOperationType(Type type)
            {
                currentType = type;
                return GetOperationAttribute(type) != null;
            };

            void ExportInterface(
                Type interfaceType,
                ExportConventionBuilder exportConventionBuilder)
            {
                exportConventionBuilder.AsContractType(interfaceType);
                if(processedTypes.Add(currentType))
                {
                    OperationAttribute attribute = GetOperationAttribute(currentType);
                    exportConventionBuilder.AddMetadata(
                        nameof(attribute.Name), attribute.Name);
                    exportConventionBuilder.AddMetadata(
                        nameof(attribute.Symbol), attribute.Symbol);
                }
            };
        }

        /// <summary>
        /// Gets the <see cref="OperationAttribute" /> instance applied to the specified
        /// type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// The <see cref="OperationAttribute" /> instance applied to the specified type,
        /// if one is applied; otherwise, a null reference.
        /// </returns>
        private static OperationAttribute GetOperationAttribute(Type type)
        {
            return type.GetCustomAttribute<OperationAttribute>();
        }

        /// <summary>
        /// Determines whether the specified interface type is the
        /// <see cref="IOperation{T}" /> interface.
        /// </summary>
        /// <param name="interfaceType">The interface type being tested.</param>
        /// <returns>
        /// True, if the specified interface type is the <see cref="IOperation{T}" />
        /// interface; otherwise, false.
        /// </returns>
        private static bool IsOperationInterface(Type interfaceType)
        {
            return interfaceType.IsGenericType
                && interfaceType.GetGenericTypeDefinition() == typeof(IOperation<>);
        }
    }
}
