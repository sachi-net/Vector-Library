using System;
using System.Linq;
using System.Text;
using VectorLibrary.Exceptions;
using VectorLibrary.MessageTemplates;
using VectorLibrary.VectorOperations;

namespace VectorLibrary
{
    /// <summary>
    /// Defines 3-dimensional vector with properties and all related vector operations.
    /// </summary>
    public class Vector3D : Vector, I3DVectorOperations
    {
        private string[] _cartesianUnitVectors = { "i", "j", "k" };

        /// <summary>
        /// Get the i component of the vector.
        /// </summary>
        public decimal I { get; }

        /// <summary>
        /// Get the j component of the vector.
        /// </summary>
        public decimal J { get; }

        /// <summary>
        /// Get the k component of the vector.
        /// </summary>
        public decimal K { get; }

        /// <summary>
        /// Initializes Vector3D instance.
        /// </summary>
        /// <param name="i">i-component of the vector</param>
        /// <param name="j">j-component of the vector</param>
        /// <param name="k">k-component of the vector</param>
        public Vector3D(decimal i, decimal j, decimal k)
        {
            I = i;
            J = j;
            K = k;
            _vector = new decimal[] { i, j, k };
        }


        /// <summary>
        /// Calculate the unit vector along this vector.
        /// </summary>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        /// <returns>Unit vector as Vector3D.</returns>
        public override Vector3D GetUnitVector()
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            var vectorComponents = new decimal[3];
            var magnitude = GetMagnitude();

            for (int i = 0; i < vectorComponents.Length; i++)
            {
                vectorComponents[i] = _vector[i] / magnitude;
            }

            return new Vector3D(vectorComponents[0], vectorComponents[1], vectorComponents[2]);
        }

        /// <summary>
        /// Convert this vector to VectorND.
        /// </summary>
        /// <returns>Vector as VectorND</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        public VectorND ToVectorND()
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            return new VectorND(new decimal[] { I, J, K });
        }

        /// <summary>
        /// Format this vector as cartesian positional vector notation with i, j, k unit vectors.
        /// </summary>
        /// <param name="precision">Decimal precision of the vector components.</param>
        /// <returns>Positional vector form as string.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when this vector is not initialized.</exception>
        public string PrintF(int precision = 2)
        {
            if (_vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            StringBuilder vector = new();
            bool isFirstComponent = true;

            for(int i = 0; i < _vector.Length; i++)
            {
                if (_vector[i] == 0)
                    continue;

                string c = string.Empty;

                if(isFirstComponent)
                {
                    c = _vector[i] == 1 ? _cartesianUnitVectors[i] 
                        : _vector[i] == -1 ? $"-{_cartesianUnitVectors[i]}"
                        : $"{decimal.Round(_vector[i], precision)}{_cartesianUnitVectors[i]}";
                    vector.Append(c);
                    isFirstComponent = false;
                    continue;
                }

                if(_vector[i] > 0)
                {
                    c = _vector[i] == 1 ? $"+{_cartesianUnitVectors[i]}" :
                        $"+{decimal.Round(_vector[i], precision)}{_cartesianUnitVectors[i]}";
                }
                else
                {
                    c = _vector[i] == -1 ? $"-{_cartesianUnitVectors[i]}" :
                        $"{decimal.Round(_vector[i], precision)}{_cartesianUnitVectors[i]}";
                }
                vector.Append($"{c}");
            }
            return vector.ToString().Replace("+", " + ").Replace("-", " - ").Trim();
        }

        /// <summary>
        /// Addition of this vector with provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be pereformed the addition.</param>
        /// <returns>Addition of the vectors as IVector. This can be explicitly convertible to Vector3D type.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        public override IVector AddTo(IVector vector)
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
                result[i] = _vector[i] + vector.ToArray()[i];

            return new Vector3D(result[0], result[1], result[2]);
        }

        /// <summary>
        /// Subtraction of this vector from provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be substracted from.</param>
        /// <returns>Substraction of the vectors as IVector. This can be explicitly convertible to Vector3D type.</returns>
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

            return new Vector3D(result[0], result[1], result[2]);
        }

        /// <summary>
        /// Perform scalar multiplication with this vector.
        /// </summary>
        /// <param name="scalar">The scalar to be performed the multiplication.</param>
        /// <returns>Scaled vector as IVector. This can be explicitly convertible to Vector3D type.</returns>
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

            return new Vector3D(result[0], result[1], result[2]);
        }

        /// <summary>
        /// Vector projection of this vector on to provided IVector.
        /// </summary>
        /// <param name="vector">The vector to be projected on to.</param>
        /// <returns>Vector projection of the vector as IVector. This can be explicitly convertible to Vector3D type.</returns>
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
            Vector3D v = vector.GetType() == typeof(Vector3D) ? vector as Vector3D : 
                new Vector3D(comps[0], comps[1], comps[2]);

            return v.MultiplyByScalar(scalarFactor) as Vector3D;
        }

        /// <summary>
        /// Perform the vector product with this vector and provided IVector.
        /// </summary>
        /// <param name="vector">The IVector object to be performed the vector product.</param>
        /// <returns>Vector product of the vectors as Vector3D.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="VectorSpaceNotMatchException">Throws when the vectors have different dimentional spaces.</exception>
        /// <exception cref="InvalidVectorOperationException">Throws when provided IVector is not a 3-dimensional vector.</exception>
        public Vector3D CrossProduct(IVector vector)
        {
            if (_vector is null || vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (vector.ToArray().Length != 3)
            {
                throw new InvalidVectorOperationException(Message.OPERATION_NOT_DEFINED_FOR_ND);
            }

            var comps = vector.ToArray();
            Vector3D v = vector.GetType() == typeof(Vector3D) ? vector as Vector3D :
                new Vector3D(comps[0], comps[1], comps[2]);

            decimal i = (J * v.K) - (K * v.J);
            decimal j = (K * v.I) - (I * v.K);
            decimal k = (I * v.J) - (J * v.I);

            return new Vector3D(i, j, k);
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

            if (_vector.Length != 3)
            {
                throw new VectorSpaceNotMatchException(Message.VECTOR_SPACE_NOT_MATCH);
            }

            var comps = _vector.ToArray().Select(x => -1 * x).ToArray();
            return new Vector3D(comps[0], comps[1], comps[2]);
        }

        /// <summary>
        /// Calculate the Euclidean distance between this vector and provided vector.
        /// </summary>
        /// <param name="vector">The vector to calculate the distance from.</param>
        /// <returns>The distance between the vectors as decimal.</returns>
        /// <exception cref="VectorNotInitializedException">Throws when the vectors are not initialized.</exception>
        /// <exception cref="InvalidVectorOperationException">Throws when provided IVector is not a 3-dimensional vector.</exception>
        public decimal DistanceWith(IVector vector)
        {
            if (_vector is null || vector is null)
            {
                throw new VectorNotInitializedException(Message.VECTOR_NOT_INITIALIZED);
            }

            if (vector.ToArray().Length != 3)
            {
                throw new InvalidVectorOperationException(Message.OPERATION_NOT_DEFINED_FOR_ND);
            }

            var comps = vector.ToArray();
            Vector3D v = vector.GetType() == typeof(Vector3D) ? vector as Vector3D :
                new Vector3D(comps[0], comps[1], comps[2]);

            decimal dx = I - v.I;
            decimal dy = J - v.J;
            decimal dz = K - v.K;

            double rs = Math.Pow((double)dx, 2) + Math.Pow((double)dy, 2) + Math.Pow((double)dz, 2);
            return (decimal)Math.Sqrt(rs);
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

            Vector3D zeroVector = new(0, 0, 0);
            IVector result = null;

            if (divisionMode is DivisionMode.Internal)
                result = zeroVector.AddTo(MultiplyByScalar(ratio)).MultiplyByScalar(1 / (ratio + 1));

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
