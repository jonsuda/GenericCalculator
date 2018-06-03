using System;
using System.ComponentModel.Composition;

namespace GenericCalculator
{
    /// <summary>
    /// Used to denote classes that represent binary operations for multiple operand types;
    /// the <see cref="OperationAttribute" /> is to be used for one operand type and to
    /// specify the metadata (for all operand types); an
    /// <see cref="AdditionalOperationAttribute"/> instance is to be applied for each
    /// additional operand type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class AdditionalOperationAttribute : ExportAttribute
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="operandType">The operand type.</param>
        public AdditionalOperationAttribute(Type operandType)
            : base(typeof(IOperation<>).MakeGenericType(operandType))
        { }
    }
}
