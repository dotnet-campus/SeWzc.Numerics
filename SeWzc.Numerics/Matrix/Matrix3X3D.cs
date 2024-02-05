﻿namespace SeWzc.Numerics.Matrix;

public readonly partial struct Matrix3X3D : ISquareMatrix<Matrix3X3D, Vector3D, double>
{
    /// <inheritdoc />
    public double Determinant => M11 * (M22 * M33 - M23 * M32) - M12 * (M21 * M33 - M23 * M31) + M13 * (M21 * M32 - M22 * M31);

    /// <inheritdoc />
    public Matrix3X3D Inverse()
    {
        var det = Determinant;

        if (det == 0)
            throw new MatrixNonInvertibleException(det);

        if (det.IsAlmostZero(Row1.SquaredLength + Row2.SquaredLength + Row3.SquaredLength))
            throw new MatrixNonInvertibleException(det);

        var invDet = 1 / det;
        var invM11 = (M22 * M33 - M23 * M32) * invDet;
        var invM12 = -(M12 * M33 - M13 * M32) * invDet;
        var invM13 = (M12 * M23 - M13 * M22) * invDet;
        var invM21 = -(M21 * M33 - M23 * M31) * invDet;
        var invM22 = (M11 * M33 - M13 * M31) * invDet;
        var invM23 = -(M11 * M23 - M13 * M21) * invDet;
        var invM31 = (M21 * M32 - M22 * M31) * invDet;
        var invM32 = -(M11 * M32 - M12 * M31) * invDet;
        var invM33 = (M11 * M22 - M12 * M21) * invDet;
        return new Matrix3X3D(invM11, invM12, invM13, invM21, invM22, invM23, invM31, invM32, invM33);
    }
}