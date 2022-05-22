using System;
using System.Linq;
using System.Text;
using VectorLibrary.Exceptions;
using VectorLibrary.MessageTemplates;
using VectorLibrary.VectorEnums;

namespace VectorLibrary
{
    /// <summary>
    /// Defines vector with properties and common vector operations.
    /// </summary>
    public abstract class Vector : IVector
    {
        /// <summary>
        /// Inner vector array
        /// </summary>
        protected decimal[] _vector;

        /// <summary>
        /// Convert this vector to an array.
        /// </summary>
        /// <returns>Vector components as decimal array.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        public decimal[] ToArray()
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            return _vector;
        }

        /// <summary>
        /// Calculate the magnitude of this vector.
        /// </summary>
        /// <returns>Magnitude of the vector as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        public decimal GetMagnitude()
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            decimal magnitude = 0m;

            foreach (var n in _vector)
                magnitude += n * n;

            return (decimal)Math.Sqrt((double)magnitude);
        }

        /// <summary>
        /// Calculate the vector dimension.
        /// </summary>
        /// <returns>Dimension of the vector as integer.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        public int GetDimension()
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            return _vector.Where(c => c != 0).Count();
        }

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
        public string Print(int precision = 2)
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            StringBuilder vector = new("[");
            for (int i = 0; i < _vector.Length; i++)
            {
                vector.Append($" {decimal.Round(_vector[i], precision)}");
                if (i != _vector.Length - 1)
                    vector.Append(",");
            }
            vector.Append(" ]");
            return vector.ToString();
        }

        /// <summary>
        /// Addition of this vector with provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be pereformed the addition.</param>
        /// <returns>Addition of the vectors as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public abstract IVector AddTo(IVector vector);

        /// <summary>
        /// Subtraction of this vector from provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be substracted from.</param>
        /// <returns>Substraction of the vectors as IVector</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public abstract IVector SubtractFrom(IVector vector);

        /// <summary>
        /// Perform the scalar product with this vector and provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be performed the scalar product.</param>
        /// <returns>Scalar product of the vectors as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public decimal DotProduct(IVector vector)
        {
            if (_vector is null || vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (_vector.Length != vector.ToArray().Length)
            {
                throw new VectorSpaceNotMatchException(Message.VECTOR_SPACE_NOT_MATCH);
            }

            decimal dotProduct = 0m;

            for (int i = 0; i < _vector.Length; i++)
                dotProduct += _vector[i] * vector.ToArray()[i];

            return dotProduct;
        }

        /// <summary>
        /// Perform scalar multiplication with this vector.
        /// </summary>
        /// <param name="scalar">The scalar to be performed the multiplication.</param>
        /// <returns>Scaled vector as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        public abstract IVector MultiplyByScalar(decimal scalar);

        /// <summary>
        /// Compute the angle between this vector and provided IVector.
        /// </summary>
        /// <param name="vector">The vector to be calculated the angle with.</param>
        /// <param name="angleUnit">Unit of the angle [Radian/Degree].</param>
        /// <returns>The angle between the vectors as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public decimal AngleWith(IVector vector, AngleUnit angleUnit = AngleUnit.Radian)
        {
            if (_vector is null || vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (_vector.Length != vector.ToArray().Length)
            {
                throw new VectorSpaceNotMatchException(Message.VECTOR_SPACE_NOT_MATCH);
            }

            var _magnitude = GetMagnitude();
            var magnitude = vector.GetMagnitude();
            var scalarProduct = DotProduct(vector);
            decimal cosAngle = scalarProduct / (_magnitude * magnitude);
            decimal angle = 0m;

            switch (angleUnit)
            {
                case AngleUnit.Radian:
                    angle = Convert.ToDecimal(Math.Acos((double)cosAngle));
                    break;
                case AngleUnit.Degree:
                    angle = Convert.ToDecimal(Math.Acos((double)cosAngle) * 180 / Math.PI);
                    break;
            }

            return angle;
        }

        /// <summary>
        /// Scalar projection of this vector on to provided IVector.
        /// </summary>
        /// <param name="vector">The vector to be projected on to.</param>
        /// <returns>Scalar projection of the vector as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public decimal ScalarProjectionOn(IVector vector)
        {
            if (_vector is null || vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (_vector.Length != vector.ToArray().Length)
            {
                throw new VectorSpaceNotMatchException(Message.VECTOR_SPACE_NOT_MATCH);
            }

            var projection = DotProduct(vector) / vector.GetMagnitude();

            return projection;
        }

        /// <summary>
        /// Vector projection of this vector on to provided IVector.
        /// </summary>
        /// <param name="vector">The vector to be projected on to.</param>
        /// <returns>Vector projection of the vector as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public abstract IVector VectorProjectionOn(IVector vector);
    }
}
