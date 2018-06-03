namespace GenericCalculator
{
    /// <summary>
    /// Represents the metadata associated with a binary operation (i.e., a
    /// <see cref="IOperation{T}" /> implementation.
    /// </summary>
    public interface IOperationMetadata
    {
        /// <summary>
        /// Gets the name of the operation (e.g., addition).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the operation's symbol (e.g., '+' in addition).
        /// </summary>
        char Symbol { get; }
    }
}
