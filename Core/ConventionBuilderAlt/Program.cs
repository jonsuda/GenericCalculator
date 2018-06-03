using System;
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
        /// Creates a <see cref="ContainerConfiguration" /> instance set up to load all of
        /// the project's exports.
        /// </summary>
        /// <returns>
        /// A <see cref="ContainerConfiguration" /> instance set up to load all of the
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
        /// This project (unlike the CustomAttributes project) represents an alternative
        /// implementation that doesn't rely on MEF-managed metadata; an implementation of
        /// the <see cref="IMetadataProvider" /> interface is used instead.
        /// </remarks>
        private static ConventionBuilder CreateConventionBuilder()
        {
            var conventionBuilder = new ConventionBuilder();
            conventionBuilder
                .ForTypesMatching(IsOperationType)
                .ExportInterfaces(IsOperationInterface);
            return conventionBuilder;
        }

        /// <summary>
        /// Determines whether the specified type represents an operation; a type
        /// represents an operation if it has an <see cref="OperationAttribute" /> instance
        /// applied to it.
        /// </summary>
        /// <param name="type">The type being tested.</param>
        /// <returns>
        /// True, if the type represents an operation; otherwise, false.
        /// </returns>
        private static bool IsOperationType(Type type)
        {
            return type.GetCustomAttribute<OperationAttribute>() != null;
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
