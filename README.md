# Vector-Library
Vector-Library consists of vector properties and common vector algebraic functions to perform various arithmetic operations on any dimensional vectors and scalars. This is a reusable library that any .NET 5.0 or above projects can utilize as an application dependency.

## Vector-Master Demo
Try vector-master demo application built on top of this library at,  
[vector-master.azurewebsites.net](https://vector-master.azurewebsites.net/).

## Prerequisites
Currently, Vector-Library dependency is available for local installation only.  
Vector-Library requires [.NET 5.0](https://dotnet.microsoft.com/en-us/download/dotnet/5.0) (v5.#.#) or [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) (v6.#.#).

## Get Started
Add `VectorLibrary.dll` to project dependencies.  
Refer the content from the code by adding `using VectorLibrary;` at top-level statements or global usings.

## Implementation Overview
VectorLibray is formed with following class-object structure.

### ICommonVectorOperation interface
This provide common vector operations which are applied to both 3-dimensional and n-dimensional (higher than 3-dimensional) vectors.

### I3DVectorOperations interface
This provides vector operations which only 3-dimensional vectors can perform.

### IVector interface
This element reference the both 3-dimensional and n-dimensional vector types.
```C#
public interface IVector : ICommonVectorOperations
```

### Vector class
This is an abstract element which provide common functionable operations to both 3-dimensional and n-dimensional vectors.
```C#
public abstract class Vector : IVector
```

### Vector3D class
This is the main class which represents 3-dimensional vector in action.
```C#
public class Vector3D : Vector, I3DVectorOperations
```

### VectorND class
This is the main class which represents n-dimensional ((higher than 3-dimensional)) vector in action.
```C#
public class VectorND : Vector
```

Following tables indicate the implementation structure of VectorLibray for both 3-dimensional `Vector3D` and n-dimensional `VectorND` vectors.

### Properties
|Name|Type|Availability|Description|
|---|---|---|---|
|I|`decimal`|`Vector3D` only|Get the i component of the vector|
|J|`decimal`|`Vector3D` only|Get the j component of the vector|
|K|`decimal`|`Vector3D` only|Get the k component of the vector|

### Constructors
For `Vector3D`
|Name|Description|
|---|---|
|`Vector3D(decimal i, decimal j, decimal k)`|Initializes Vector3D instance|

For `VectorND`
|Name|Description|
|---|---|
|`VectorND(params decimal[] vector)`|Initializes VectorND instance|

### Enums
`AngleUnit` enumeration defines the unit of angle when calculating the angle between two vectors.
|Option|Description|
|---|---|
|`Degree`|Unit of angle with Radians (θ°)|
|`Radian`|Unit of angle with Degrees (θ rad)|

`DivisionMode` enumeration defines the ratio division method to follow when performing vector division.
|Option|Description|
|---|---|
|`Internal`|Internal ratio division|
|`External`|External ratio division|

### Methods
|Name|Return Type|Availability|Description|
|---|---|---|---|
|`AddTo(IVector)`|`IVector`|`Vector3D` and `VectorND`|Addition of this vector with provided IVector|
|`AngleWith(IVector, [AngleUnit])`|`IVector`|`Vector3D` and `VectorND`|Compute the angle between this vector and provided IVector|
|`CrossProduct(IVector)`|`Vector3D`|`Vector3D` only|Perform the vector product with this vector and provided IVector|
|`DistanceWith(IVector)`|`IVector`|`Vector3D` only|Calculate the Euclidean distance between this vector and provided vector|
|`DivideInto(decimal, [DivisionMode])`|`IVector`|`Vector3D` and `VectorND`|Divide this vector into the given ratio internally or externally|
|`DotProduct(IVector)`|`decimal`|`Vector3D` and `VectorND`|Perform the scalar product with this vector and provided IVector|
|`GetDimension()`|`int`|`Vector3D` and `VectorND`|Calculate the vector dimension|
|`GetMagnitude()`|`decimal`|`Vector3D` and `VectorND`|Calculate the magnitude of this vector|
|`GetUnitVector()`|`IVector`|`Vector3D` and `VectorND`|Calculate the unit vector along this vector|
|`MultiplyByScalar(decimal)`|`IVector`|`Vector3D` and `VectorND`|Perform scalar multiplication with this vector|
|`Negate()`|`IVector`|`Vector3D` and `VectorND`|Negate this vector|
|`Print([int])`|`string`|`Vector3D` and `VectorND`|Format this vector as row matrix notation|
|`PrintF([int])`|`string`|`Vector3D` only|Format this vector as cartesian positional vector notation with i, j, k unit vectors|
|`ScalarProjectionOn(IVector)`|`decimal`|`Vector3D` and `VectorND`|Scalar projection of this vector on to provided IVector|
|`SubtractFrom(IVector)`|`IVector`|`Vector3D` and `VectorND`|Subtraction of this vector from provided IVector|
|`ToArray()`|`decimal[]`|`Vector3D` and `VectorND`|Convert this vector to an array|
|`ToVector3D()`|`Vector3D`|`VectorND` only|Convert this vector to Vector3D|
|`ToVectorND()`|`VectorND`|`Vector3D` only|Convert this vector to VectorND|
|`VectorProjectionOn(IVector)`|`IVector`|`Vector3D` and `VectorND`|Vector projection of this vector on to provided IVector|

## Usage
Following section describe the general usage of `VectorLibrary` to perform vector operations.

### Initialize New Vector  
Define new 3-dimensional vector `u = 2i + 3j - k`
```C#
Vector3D u = new(2, 3, -1);
```

Initialize New n-dimensional Vector  
Define new n-dimensional vector `u = [1, 3, -5, 6, -2]`
```C#
VectorND u = new(new decimal[] { 1, 3, -5, 6, -2 });
```
Or simply
```C#
VectorND u = new(1, 3, -5, 6, -2);
```

### Read Vector
Any defined vector can be read in two ways as either a string formatted text or an array. If the vector is `null`, the function throws `VectorNotInitializedException`.
Using `Print` function, a vector can be read as a `string`.
```C#
Console.WriteLine(u.Print());
```
Using `PrintF` function, a 3-dimensional vector can be read as a `string`.
```C#
Console.WriteLine(u.PrintF());
```
The `Print` and `PrintF` function takes optional parameter `int precision` to round-off decimal figures when reading. The default precision is `2` decimals.  

Bothe of the vector types can be converted to `decimal[]` array using `ToArray` function.
```C#
decimal[] array = u.ToArray();
```

### Vector Properties
Any vector has three main alias properties as Dimension, Magnitude and UnitVector. These properties can be calculate as follows.
```C#
int dimension = u.GetDimension();
decimal magnitude = u.GetMagntude();
IVector unitVector = u.GetUnitVector();
```

### Vector Conversion
Convert a 3-dimensional vector `u` to an n-dimensional vector `v`.
```C#
VectorND v = u.ToVector3D();
```
Convert a 3-dimensional vector `v` to an n-dimensional vector `u`. However, the vector `v`'s order must be 3 to perform this conversion, otherwise the function throws `InvalidCastException`.
```C#
Vector3D v = u.ToVectorND();
```

### Negate Vector
Negating results the negative vector of current vector `u` and `v` as follows. 
```C#
Vector3D u = new(1, -3, 2);
IVector negativeOfU = u.Negate();

VectorND v = new(-2, 1, 4, -3);
IVector negativeOfV = v.Negate();
```

### Distance between Vectors
The Euclidean distance of two vectors `u` to `v` and `u` to `w` can be calculated as follows. The function throws `VectorNotInitializedException` if vector `u`, `v` or `w` is `null` and throws `InvalidVectorOperationException` when provided any non 3-dimensional vector.
```C#
Vector3D u = new(1, 5, 3);
IVector v = new Vector3D(2, -3, 4);
IVector w = new VectorND(4, 1, -7);

decimal distanceFromUtoV = u.DistanceWith(v);
decimal distanceFromUtoW = u.DistanceWith(w);
```

## Vector Operations
IVector object can perform following operations.

### Addition
Add two same-dimensional vectors `u` and `v`. The function throws `VectorNotInitializedException` if vector `u` or `v` is `null` and throws `VectorSpaceNotMatchException` if the dimension of both vectors do not match.
```C#
IVector result = u.AddTo(v);
```

### Subtraction
Subtract two same-dimensional vectors `u` from `v`. The function throws `VectorNotInitializedException` if vector `u` or `v` is `null` and throws `VectorSpaceNotMatchException` if the dimension of both vectors do not match.
```C#
IVector result = u.SubtractFrom(v);
```

### Scalar Multiplication
Multiply vector `u` by scalar value `k`. The function throws `VectorNotInitializedException` if vector `u` is `null`.
```C#
IVector result = u.MultiplyByScalar(k);
```

### Ratio Division
A vector can be divided into a given ratio `r` internally or externally. Note that the ratio will be taken as `r:1` while performing this action. The function throws `VectorNotInitializedException` if vector current is `null`, throws `InvalidVectorOperationException` if the ratio is negative and throws `ZeroExternalDivisionException` if attempt to perform 1:1 external division.
```C#
IVector u = new Vector3D(1, 5, -2);
// Divide u into 3:1 ratio internally
IVector result1 = u.DivideInto(3, DivisionMode.Internal);
// Divide u into 3:4 ratio externally
IVector result2 = u.DivideInto(0.75, DivisionMode.External);

IVector v = new VectorND(2, -3, -1, 5);
// Divide v into 1:1 ratio internally (exactly into two halves)
IVector result3 = v.DivideInto(1, DivisionMode.Internal);
// Divide v into 5:2 ratio externally
IVector result4 = v.DivideInto(2.5, DivisionMode.External);
```

### Dot Product (Scalar Product)
Calculate dot product of two same-dimensional vectors `u` from `v`. The function throws `VectorNotInitializedException` if vector `u` or `v` is `null` and throws `VectorSpaceNotMatchException` if the dimension of both vectors do not match.
```C#
decimal result = u.DotProduct(v);
```

### Cross Product (Vector Product)
Calculate cross product of two 3-dimensional vectors `u` from `v`. The function throws `VectorNotInitializedException` if vector `u` or `v` is `null`, throws `VectorSpaceNotMatchException` if the dimension of both vectors do not match and throws `InvalidVectorOperationException` when provided any non 3-dimensional vector.
`VectorSpaceNotMatchException` if the dimension of both vectors do not match.
```C#
Vector3D result = u.CrossProduct(v);
```

### Angle within Vectors
Calculate the angle between two same-dimensional vectors `u` from `v`. The function throws `VectorNotInitializedException` if vector `u` or `v` is `null` and throws `VectorSpaceNotMatchException` if the dimension of both vectors do not match.
The unit of angle can be defined in the function argument `AngleUnit` enum. The default unit is radian.
```C#
decimal angleInDegree = u.AngleWith(v, AngleUnit.Degree);
decimal angleInRadian = u.AngleWith(v, AngleUnit.Radian);
```

### Projection
Calculate the scalar projection of two same-dimensional vectors `u` on to `v`. The function throws `VectorNotInitializedException` if vector `u` or `v` is `null` and throws `VectorSpaceNotMatchException` if the dimension of both vectors do not match.
```C#
decimal projection = u.ScalarProjectionOn(v);
```
Calculate the vector projection of two same-dimensional vectors `u` on to `v`. The function throws `VectorNotInitializedException` if vector `u` or `v` is `null` and throws `VectorSpaceNotMatchException` if the dimension of both vectors do not match.
```C#
IVector projection = u.VectorProjectionOn(v);
```
