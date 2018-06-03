namespace GenericCalculator
{
    /// <summary>
    /// Represents a binary (two-operand) operation for a given operand type.
    /// </summary>
    /// <typeparam name="T">The operand type.</typeparam>
    public interface IOperation<T>
    {
        /// <summary>
        /// Performs the operation represented by the current instance.
        /// </summary>
        /// <param name="a">The left operand.</param>
        /// <param name="b">The right operand.</param>
        /// <returns>The result of the operation.</returns>
        T Perform(T a, T b);
    }
}
