namespace GenericCalculator
{
    /// <summary>
    /// Represents the metadata associated with a binary operation (i.e., a
    /// <see cref="IOperation{T}" /> implementation.
    /// </summary>
    /// <remarks>
    /// This is a convenience implementation of the <see cref="IOperationMetadata" />
    /// interface to be used by individual <see cref="IMetadataProvider" />
    /// implementations.
    /// </remarks>
    public class OperationMetadata : IOperationMetadata
    {
        /// <summary>
        /// Initializes a new instance of the class and sets the values of its properties.
        /// </summary>
        /// <param name="name">The name of the operation (e.g., addition).</param>
        /// <param name="symbol">The operation's symbol (e.g., '+' in addition).</param>
        public OperationMetadata(string name, char symbol)
        {
            this.Name = name;
            this.Symbol = symbol;
        }

        /// <summary>
        /// Gets the name of the operation (e.g., addition).
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the operation's symbol (e.g., '+' in addition).
        /// </summary>
        public char Symbol { get; }
    }
}
