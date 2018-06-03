using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Registration;
using System.Reflection;

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
                CreateRegistrationBuilder());
        }

        /// <summary>
        /// Creates the <see cref="RegistrationBuilder" /> builder instance to be used in
        /// conjunction with the project's <see cref="AssemblyCatalog" /> instance.
        /// </summary>
        /// <returns>
        /// The <see cref="RegistrationBuilder" /> builder instance to be used in
        /// conjunction with the project's <see cref="AssemblyCatalog" /> instance.
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
        private static RegistrationBuilder CreateRegistrationBuilder()
        {
            var registrationBuilder = new RegistrationBuilder();
            Type currentType = null;
            var processedTypes = new HashSet<Type>();
            registrationBuilder
                .ForTypesMatching(IsOperationType)
                .ExportInterfaces(IsOperationInterface, ExportInterface);
            return registrationBuilder;

            bool IsOperationType(Type type)
            {
                currentType = type;
                return GetOperationAttribute(type) != null;
            };

            void ExportInterface(Type interfaceType, ExportBuilder exportBuilder)
            {
                exportBuilder.AsContractType(interfaceType);
                if(processedTypes.Add(currentType))
                {
                    OperationAttribute attribute = GetOperationAttribute(currentType);
                    exportBuilder.AddMetadata(nameof(attribute.Name), attribute.Name);
                    exportBuilder.AddMetadata(nameof(attribute.Symbol), attribute.Symbol);
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
