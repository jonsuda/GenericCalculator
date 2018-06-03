using System;
using System.Collections.Generic;
using System.Composition;
using System.Reflection;

namespace GenericCalculator
{
    /// <summary>
    /// Retrieves metadata for individual operations.
    /// </summary>
    [Export(typeof(IMetadataProvider))]
    public class MetadataProvider : IMetadataProvider
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
        public OperationMetadata GetMetadata(Type operationType)
        {
            OperationAttribute attribute =
                operationType.GetCustomAttribute<OperationAttribute>();
            return new OperationMetadata(
                new Dictionary<string, object>()
                {
                    [nameof(attribute.Name)] = attribute.Name,
                    [nameof(attribute.Symbol)] = attribute.Symbol
                });
        }
    }
}
