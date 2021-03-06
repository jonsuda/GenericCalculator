﻿using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;

namespace GenericCalculator
{
    /// <summary>
    /// The calculator that performs all operations registered (i.e., exported) for a given
    /// operand type.
    /// </summary>
    /// <typeparam name="T">The operand type.</typeparam>
    [Export]
    public class Calculator<T>
    {
        /// <summary>
        /// The array of operations registered for the current operand type.
        /// </summary>
        private readonly OperationWrapper[] operations;

        /// <summary>
        /// The importing constructor invoked by the Managed Extensibility Framework
        /// runtime.
        /// </summary>
        /// <param name="operations">
        /// The collection of operations registered for the current operand type.
        /// </param>
        /// <param name="metadataProvider">
        /// The optional metadata provider to be used to retrieve metadata for individual
        /// operations; if no implementation of this interface is registered, the metadata
        /// provided by the Managed Extensibility Framework runtime will be used instead.
        /// </param>
        [ImportingConstructor]
        public Calculator(
            [ImportMany] IEnumerable<Lazy<IOperation<T>, OperationMetadata>> operations,
            [Import(AllowDefault = true)] IMetadataProvider metadataProvider)
        {
            this.operations = this.GetOperations(operations, metadataProvider);
        }

        /// <summary>
        /// Performs all operations registered for the current operand type for the
        /// specified operands and prints the results to the console.
        /// </summary>
        /// <param name="a">The left operand.</param>
        /// <param name="b">The right operand.</param>
        public void PerformOperations(T a, T b)
        {
            Console.WriteLine(
                $"Performing all operations for {a}, {b} " +
                $"of the {typeof(T).FullName} type.");
            foreach(OperationWrapper operation in this.operations.OrderBy(o => o.Name))
            {
                Console.WriteLine(
                    $"  {a} {operation.Symbol} {b} = " +
                    $"{operation.Operation.Perform(a, b)} " +
                    $"({operation.Name})");
            }
            Console.WriteLine();
        }

        private OperationWrapper[] GetOperations(
            IEnumerable<Lazy<IOperation<T>, OperationMetadata>> operations,
            IMetadataProvider metadataProvider)
        {
            return
                operations
                .Select(o => new OperationWrapper(
                    o.Value,
                    metadataProvider != null
                        ? metadataProvider.GetMetadata(o.Value.GetType())
                        : o.Metadata))
                .ToArray();
        }

        private class OperationWrapper
        {
            public OperationWrapper(
                IOperation<T> operation, OperationMetadata metadata)
            {
                this.Operation = operation;
                this.Name = metadata.Name;
                this.Symbol = metadata.Symbol;
            }

            public IOperation<T> Operation { get; }

            public string Name { get; }

            public char Symbol { get; }
        }
    }
}
