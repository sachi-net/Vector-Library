using VectorLibrary.Exceptions;
using VectorLibrary.VectorOperations;

namespace VectorLibrary
{
    /// <summary>
    /// Define common properties and functions on vectors in any dimension.
    /// </summary>
    public interface IVector : ICommonVectorOperations
    {
        /// <summary>
        /// Calculate the magnitude of this vector.
        /// </summary>
        /// <returns>Magnitude of the vector as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        public decimal GetMagnitude();

        /// <summary>
        /// Calculate the vector dimension.
        /// </summary>
        /// <returns>Dimension of the vector as integer.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        public int GetDimension();

        /// <summary>
        /// Calculate the unit vector along this vector.
        /// </summary>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <returns>Unit vector as IVector</returns>
        public abstract IVector GetUnitVector();

        /// <summary>
        /// Format this vector as row matrix notation.
        /// </summary>
        /// <param name="precision">Decimal precision of the vector components.</param>
        /// <returns>Row matrix form of the vector as string.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        public string Print(int precision = 2);

        /// <summary>
        /// Convert this vector to an array.
        /// </summary>
        /// <returns>Vector components as decimal array.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        public decimal[] ToArray();
    }
}
