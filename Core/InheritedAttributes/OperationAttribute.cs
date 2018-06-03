using System;
using System.Composition;

namespace GenericCalculator
{
    /// <summary>
    /// Used to denote classes that represent binary operations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    [MetadataAttribute]
    public class OperationAttribute : ExportAttribute
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="operandType">The operand type.</param>
        /// <param name="name">The name of the operation.</param>
        /// <param name="symbol">The symbol used to represent the operation.</param>
        public OperationAttribute(Type operandType, string name, char symbol)
            : base(typeof(IOperation<>).MakeGenericType(operandType))
        {
            this.Name = name;
            this.Symbol = symbol;
        }

        /// <summary>
        /// Gets the name of the operation.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the symbol used to represent the operation.
        /// </summary>
        public char Symbol { get; }
    }
}
