using VectorLibrary.Exceptions;
using VectorLibrary.VectorEnums;

namespace VectorLibrary.VectorOperations
{
    /// <summary>
    /// Provides common arithmetic operations on vectors in any dimension.
    /// </summary>
    public interface ICommonVectorOperations
    {
        /// <summary>
        /// Addition of this vector with provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be pereformed the addition.</param>
        /// <returns>Addition of the vectors as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public IVector AddTo(IVector vector);

        /// <summary>
        /// Subtraction of this vector from provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be substracted from.</param>
        /// <returns>Substraction of the vectors as IVector</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public IVector SubtractFrom(IVector vector);

        /// <summary>
        /// Perform the scalar product with this vector and provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be performed the scalar product.</param>
        /// <returns>Scalar product of the vectors as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public decimal DotProduct(IVector vector);

        /// <summary>
        /// Perform scalar multiplication with this vector.
        /// </summary>
        /// <param name="scalar">The scalar to be performed the multiplication.</param>
        /// <returns>Scaled vector as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        public IVector MultiplyByScalar(decimal scalar);

        /// <summary>
        /// Compute the angle between this vector and provided IVector.
        /// </summary>
        /// <param name="vector">The vector to be calculated the angle with.</param>
        /// <param name="angleUnit">Unit of the angle [Radian/Degree].</param>
        /// <returns>The angle between the vectors as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public decimal AngleWith(IVector vector, AngleUnit angleUnit = AngleUnit.Radian);

        /// <summary>
        /// Scalar projection of this vector on to provided IVector.
        /// </summary>
        /// <param name="vector">The vector to be projected on to.</param>
        /// <returns>Scalar projection of the vector as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public decimal ScalarProjectionOn(IVector vector);

        /// <summary>
        /// Vector projection of this vector on to provided IVector.
        /// </summary>
        /// <param name="vector">The vector to be projected on to.</param>
        /// <returns>Vector projection of the vector as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public IVector VectorProjectionOn(IVector vector);

        /// <summary>
        /// Negate this vector.
        /// </summary>
        /// <returns>Negative vector of this vector as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vectors is not initialized.</exception>
        public IVector Negate();

        /// <summary>
        /// Divide this vector into the given ratio internally or externally.
        /// </summary>
        /// <param name="ratio">The ratio to which this vector is divided into.</param>
        /// <param name="divisionMode">Division is internal or exteral.</param>
        /// <returns>The resultant vector after the ratio division as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        /// <exception cref="InvalidVectorOperationException">Throws when provided when the ratio is negative.</exception>
        /// <exception cref="ZeroExternalDivisionException">Throws when try to perform 1:1 division in external division mode.</exception>
        public IVector DivideInto(decimal ratio, DivisionMode divisionMode = DivisionMode.Internal);
    }
}
