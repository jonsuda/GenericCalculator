using System.Collections.Generic;

namespace GenericCalculator
{
    /// <summary>
    /// Represents the metadata associated with a binary operation (i.e., an
    /// <see cref="IOperation{T}" /> implementation.
    /// </summary>
    public class OperationMetadata
    {
        /// <summary>
        /// Initializes a new instance of the class and populates its property values from
        /// the specified metadata dictionary.
        /// </summary>
        /// <param name="metadata">The dictionary of metadata values.</param>
        public OperationMetadata(IDictionary<string, object> metadata)
        {
            metadata.TryGetValue(nameof(this.Name), out object name);
            metadata.TryGetValue(nameof(this.Symbol), out object symbol);
            this.Name = (string) name;
            this.Symbol = symbol is char ? (char) symbol : default(char);
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
