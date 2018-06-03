using System;

namespace GenericCalculator
{
    /// <summary>
    /// Used to denote classes that represent binary operations.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class OperationAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="name">The name of the operation.</param>
        /// <param name="symbol">The symbol used to represent the operation.</param>
        public OperationAttribute(string name, char symbol)
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
