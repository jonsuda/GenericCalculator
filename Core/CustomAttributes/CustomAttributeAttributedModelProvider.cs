using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Convention;
using System.Linq;
using System.Reflection;

namespace GenericCalculator
{
    /// <summary>
    /// A custom attributed model provider implementation that ensures that all
    /// <see cref="IOperation{T}" /> interfaces implementations by classes decorated with
    /// <see cref="OperationAttribute" /> get exported.
    /// </summary>
    public class CustomAttributeAttributedModelProvider : AttributedModelProvider
    {
        /// <summary>
        /// Returns the list of attributes applied to the specified member of the specified
        /// type.
        /// </summary>
        /// <param name="reflectedType">The type.</param>
        /// <param name="member">The member to inspect.</param>
        /// <returns>
        /// A collection of the attributes applied to the specified member.
        /// </returns>
        /// <remarks>
        /// For each class decorated with <see cref="OperationAttribute" />, the method
        /// returns an <see cref="ExportAttribute" /> instance for each instance of
        /// <see cref="IOperation{T}" /> the class implements, as well as one instance of
        /// <see cref="ExportMetadataAttribute" /> for the operation name and one for the
        /// symbol. The name and the symbol get extracted from the operation attribute.
        /// </remarks>
        public override IEnumerable<Attribute>
            GetCustomAttributes(Type reflectedType, MemberInfo member)
        {
            IEnumerable<Attribute> attributes = Enumerable.Empty<Attribute>();
            if(member is Type type)
            {
                OperationAttribute operationAttribute =
                    type.GetCustomAttribute<OperationAttribute>();
                if(operationAttribute != null)
                {
                    attributes = this.GetExportAttributes(type, operationAttribute);
                }
            }
            return attributes;
        }

        /// <summary>
        /// Returns the list of attributes applied to the specified parameter of the
        /// specified type.
        /// </summary>
        /// <param name="reflectedType">The type.</param>
        /// <param name="parameter">The parameter to inspect.</param>
        /// <returns>
        /// A collection of the attributes applied to the specified parameter; this
        /// implementation always returns an empty collection.
        /// </returns>
        public override IEnumerable<Attribute>
            GetCustomAttributes(Type reflectedType, ParameterInfo parameter)
        {
            return Enumerable.Empty<Attribute>();
        }

        private IEnumerable<Attribute> GetExportAttributes(
            Type operationType, OperationAttribute operationAttribute)
        {
            var attributes = new List<Attribute>();
            attributes.AddRange(
                operationType
                .GetInterfaces()
                .Where(this.IsOperationInterface)
                .Select(interfaceType => new ExportAttribute(interfaceType)));
            attributes.Add(new ExportMetadataAttribute(
                nameof(operationAttribute.Name), operationAttribute.Name));
            attributes.Add(new ExportMetadataAttribute(
                nameof(operationAttribute.Symbol), operationAttribute.Symbol));
            return attributes;
        }

        private bool IsOperationInterface(Type interfaceType)
        {
            return interfaceType.IsGenericType
                && interfaceType.GetGenericTypeDefinition() == typeof(IOperation<>);
        }
    }
}
