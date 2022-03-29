using VectorLibrary.Exceptions;

namespace VectorLibrary.VectorOperations
{
    /// <summary>
    /// Provides arithmetic operations only on 3-dimensional vectors.
    /// </summary>
    public interface I3DVectorOperations
    {
        /// <summary>
        /// Perform the vector product with this vector and provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be performed the vector product.</param>
        /// <returns>Vector product of the vectors as Vector3D.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        /// <exception cref="InvalidVectorOperationException">Throws when provided IVector is not a 3-dimensional vector.</exception>
        public Vector3D CrossProduct(IVector vector);
    }
}
