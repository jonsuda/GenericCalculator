using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Reflection.Context;

namespace GenericCalculator
{
    /// <summary>
    /// A custom reflection context implementation that ensures that all
    /// <see cref="IOperation{T}" /> interfaces implementations by classes decorated with
    /// <see cref="OperationAttribute" /> get exported.
    /// </summary>
    public class CustomAttributeReflectionContext : CustomReflectionContext
    {
        /// <summary>
        /// Gets the set of custom attributes for the specified member.
        /// </summary>
        /// <param name="member">
        /// The member whose custom attributes will be returned.
        /// </param>
        /// <param name="declaredAttributes">
        /// A collection of the member's attributes in its current context.
        /// </param>
        /// <returns>
        /// This implementation returns all attributes returned by the base implementation.
        /// In addition, for each class decorated with <see cref="OperationAttribute" />,
        /// it adds an <see cref="ExportAttribute" /> instance for each instance of
        /// <see cref="IOperation{T}" /> the class implements.
        /// </returns>
        protected override IEnumerable<object> GetCustomAttributes(
            MemberInfo member, IEnumerable<object> declaredAttributes)
        {
            var attributes = new List<object>(
                base.GetCustomAttributes(member, declaredAttributes));
            if(member is Type type)
            {
                if(type.GetCustomAttribute<OperationAttribute>() != null)
                {
                    attributes.AddRange(
                        this.GetExportAttributes(type));
                }
            }
            return attributes;
        }

        private IEnumerable<object> GetExportAttributes(Type operationType)
        {
            return
                operationType
                .GetInterfaces()
                .Where(this.IsOperationInterface)
                .Select(interfaceType => new ExportAttribute(interfaceType));
        }

        private bool IsOperationInterface(Type interfaceType)
        {
            return interfaceType.IsGenericType
                && interfaceType.GetGenericTypeDefinition() == typeof(IOperation<>);
        }
    }
}
