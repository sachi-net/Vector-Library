using System;
using System.Linq;
using VectorLibrary.Exceptions;
using VectorLibrary.MessageTemplates;

namespace VectorLibrary
{
    /// <summary>
    /// Defines n-dimensional vector with properties and common vector operations.
    /// </summary>
    public class VectorND : Vector
    {
        /// <summary>
        /// Initializes VectorND instance.
        /// </summary>
        /// <param name="vector">Vector as decimal array or set of vector components.</param>
        public VectorND(params decimal[] vector)
        {
            _vector = vector;
        }

        /// <summary>
        /// Calculate the unit vector along this vector.
        /// </summary>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        /// <returns>Unit vector as Vector3D. This can be explicitly convertible to VectorND type.</returns>
        public override IVector GetUnitVector()
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            var vectorComponents = new decimal[GetDimension()];
            var magnitude = GetMagnitude();

            for(int i = 0; i < vectorComponents.Length; i++) {
                vectorComponents[i] = _vector[i] / magnitude;
            }

            return new VectorND(vectorComponents);
        }

        /// <summary>
        /// Convert this vector to Vector3D.
        /// </summary>
        /// <returns>Vector as Vector3D</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        /// <exception cref="InvalidCastException">Throws when the vector dimensional space is not for 3D.</exception>
        public Vector3D ToVector3D()
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (_vector.Length != 3)
            {
                throw new InvalidCastException(Message.INVALID_VECTOR_CAST);
            }

            return new Vector3D(_vector[0], _vector[1], _vector[2]);
        }

        /// <summary>
        /// Addition of this vector with provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be pereformed the addition.</param>
        /// <returns>Addition of the vectors as IVector. This can be explicitly convertible to VectorND type.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public override IVector AddTo(IVector vector)
        {
            if (_vector is null || vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if(_vector.Length != vector.ToArray().Length)
            {
                throw new VectorSpaceNotMatchException(Message.VECTOR_SPACE_NOT_MATCH);
            }

            decimal[] result = new decimal[_vector.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = _vector[i] + vector.ToArray()[i];

            return new VectorND(result);
        }

        /// <summary>
        /// Subtraction of this vector from provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be substracted from.</param>
        /// <returns>Substraction of the vectors as IVector. This can be explicitly convertible to VectorND type.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public override IVector SubtractFrom(IVector vector)
        {
            if (_vector is null || vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (_vector.Length != vector.ToArray().Length)
            {
                throw new VectorSpaceNotMatchException(Message.VECTOR_SPACE_NOT_MATCH);
            }

            decimal[] result = new decimal[_vector.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = vector.ToArray()[i] - _vector[i];

            return new VectorND(result);
        }

        /// <summary>
        /// Perform scalar multiplication with this vector.
        /// </summary>
        /// <param name="scalar">The scalar to be performed the multiplication.</param>
        /// <returns>Scaled vector as IVector. This can be explicitly convertible to VectorND type.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        public override IVector MultiplyByScalar(decimal scalar)
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            decimal[] result = new decimal[_vector.Length];

            for (int i = 0; i < result.Length; i++)
                result[i] = _vector[i] * scalar;

            return new VectorND(result);
        }

        /// <summary>
        /// Vector projection of this vector on to provided IVector.
        /// </summary>
        /// <param name="vector">The vector to be projected on to.</param>
        /// <returns>Vector projection of the vector as IVector. This can be explicitly convertible to VectorND type.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public override IVector VectorProjectionOn(IVector vector)
        {
            if (_vector is null || vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (_vector.Length != vector.ToArray().Length)
            {
                throw new VectorSpaceNotMatchException(Message.VECTOR_SPACE_NOT_MATCH);
            }

            var scalarFactor = ScalarProjectionOn(vector) / vector.GetMagnitude();
            var comps = vector.ToArray();
            VectorND v = vector.GetType() == typeof(VectorND) ? vector as VectorND :
                new VectorND(comps);

            return v.MultiplyByScalar(scalarFactor) as VectorND;
        }

        /// <summary>
        /// Negate this vector.
        /// </summary>
        /// <returns>Negative vector of this vector as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vectors is not initialized.</exception>
        public override IVector Negate()
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            return new VectorND(_vector.ToArray().Select(x => -1 * x).ToArray());
        }

        /// <summary>
        /// Divide this vector into the given ratio internally or externally.
        /// </summary>
        /// <param name="ratio">The ratio to which this vector is divided into.</param>
        /// <param name="divisionMode">Division is internal or exteral.</param>
        /// <returns>The resultant vector after the ratio division as IVector.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        /// <exception cref="InvalidVectorOperationException">Throws when provided when the ratio is negative.</exception>
        /// <exception cref="ZeroExternalDivisionException">Throws when try to perform 1:1 division in external division mode.</exception>
        public override IVector DivideInto(decimal ratio, DivisionMode divisionMode = DivisionMode.Internal)
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (ratio < 0)
            {
                throw new InvalidVectorOperationException(Message.INVALID_DIVISION_RATIO);
            }

            VectorND zeroVector = new(_vector.ToArray().Select(x => 0m).ToArray());
            IVector result = null;

            if (divisionMode is DivisionMode.Internal)
                result = zeroVector.AddTo(MultiplyByScalar(ratio)).MultiplyByScalar(1 / (1 + ratio));

            if (divisionMode is DivisionMode.External)
            {
                if (ratio is 1)
                    throw new ZeroExternalDivisionException(Message.ZERO_EXTERNAL_DIVISION);
                result = MultiplyByScalar(ratio).SubtractFrom(zeroVector).MultiplyByScalar(1 / (1 - ratio));
            }

            return result;
        }
    }
}
