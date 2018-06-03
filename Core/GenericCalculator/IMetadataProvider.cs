using System;

namespace GenericCalculator
{
    /// <summary>
    /// Retrieves metadata for individual operations.
    /// </summary>
    public interface IMetadataProvider
    {
        /// <summary>
        /// Retrieves metadata for the operation(s) represented by the specified type
        /// (class).
        /// </summary>
        /// <param name="operationType">
        /// The type (class) representing the operation(s) to retrieve metadata for.
        /// </param>
        /// <returns>
        /// The metadata for the operation(s) represented by the specified type (class).
        /// </returns>
        OperationMetadata GetMetadata(Type operationType);
    }
}
