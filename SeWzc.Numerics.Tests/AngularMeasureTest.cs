﻿using System;
using JetBrains.Annotations;
using Xunit;

namespace SeWzc.Numerics.Tests;

[TestSubject(typeof(AngularMeasure))]
public class AngularMeasureTest
{
    #region 静态变量

    public static readonly TheoryData<AngularMeasure> AngleData = new([
        AngularMeasure.Zero,
        AngularMeasure.HalfPi,
        AngularMeasure.Pi,
        AngularMeasure.HalfPi + AngularMeasure.Pi,
        AngularMeasure.Tau,
        AngularMeasure.FromRadian(1),
        AngularMeasure.FromDegree(45),
        AngularMeasure.FromDegree(1),
        AngularMeasure.FromDegree(100),
    ]);

    public static readonly TheoryData<AngularMeasure, AngularMeasure, AngularMeasure> AngleAddTestData = new()
    {
        { AngularMeasure.Degree90, AngularMeasure.Degree90, AngularMeasure.Degree180 },
        { AngularMeasure.Degree90, AngularMeasure.Degree180, AngularMeasure.Degree270 },
        { AngularMeasure.Degree180, AngularMeasure.Degree180, AngularMeasure.Degree360 },
        { AngularMeasure.Degree270, AngularMeasure.Degree90, AngularMeasure.Degree360 },
        { AngularMeasure.HalfPi, AngularMeasure.Pi, AngularMeasure.OneAndHalfPi },
    };

    #endregion

    #region 成员方法

    [Theory(DisplayName = "测试从角度创建角。")]
    [MemberData(nameof(AngleData))]
    public void FromDegreeTest(AngularMeasure angle)
    {
        Assert.Equal(angle, AngularMeasure.FromDegree(angle.Degree), NumericsEqualHelper.IsAlmostEqual);
    }

    [Theory(DisplayName = "测试从弧度创建角。")]
    [MemberData(nameof(AngleData))]
    public void FromRadianTest(AngularMeasure angle)
    {
        Assert.Equal(angle, AngularMeasure.FromRadian(angle.Radian), NumericsEqualHelper.IsAlmostEqual);
    }

    [Theory(DisplayName = "测试角的加法。")]
    [MemberData(nameof(AngleAddTestData))]
    public void AddTest(AngularMeasure angle1, AngularMeasure angle2, AngularMeasure expected)
    {
        Assert.Equal(expected, angle1 + angle2, NumericsEqualHelper.IsAlmostEqual);
    }

    [Theory(DisplayName = "测试角的减法。")]
    [MemberData(nameof(AngleAddTestData))]
    public void SubtractTest(AngularMeasure expected, AngularMeasure angle1, AngularMeasure angle2)
    {
        Assert.Equal(expected, angle2 - angle1, NumericsEqualHelper.IsAlmostEqual);
    }

    [Fact(DisplayName = "测试角的乘法。")]
    public void MultiplyTest()
    {
        Assert.Equal(AngularMeasure.Pi, AngularMeasure.HalfPi * 2, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(AngularMeasure.Tau, AngularMeasure.Pi * 2, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(AngularMeasure.Tau, AngularMeasure.HalfPi * 4, NumericsEqualHelper.IsAlmostEqual);

        Assert.Equal(AngularMeasure.Pi, 2 * AngularMeasure.HalfPi, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(AngularMeasure.Tau, 2 * AngularMeasure.Pi, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(AngularMeasure.Tau, 4 * AngularMeasure.HalfPi, NumericsEqualHelper.IsAlmostEqual);
    }

    [Fact(DisplayName = "测试角的数除。")]
    public void DivideNumTest()
    {
        Assert.Equal(AngularMeasure.HalfPi, AngularMeasure.Pi / 2, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(AngularMeasure.HalfPi, AngularMeasure.Tau / 4, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(AngularMeasure.Pi, AngularMeasure.Tau / 2, NumericsEqualHelper.IsAlmostEqual);
    }

    [Fact(DisplayName = "测试角的除法。")]
    public void DivideTest()
    {
        Assert.Equal(2, AngularMeasure.Pi / AngularMeasure.HalfPi, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(4, AngularMeasure.Tau / AngularMeasure.HalfPi, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(2, AngularMeasure.Tau / AngularMeasure.Pi, NumericsEqualHelper.IsAlmostEqual);
    }

    [Fact(DisplayName = "测试角的比较。")]
    public void CompareTest()
    {
        Assert.True(AngularMeasure.Zero < AngularMeasure.HalfPi);
        Assert.True(AngularMeasure.HalfPi < AngularMeasure.Pi);
        Assert.True(AngularMeasure.Pi < AngularMeasure.HalfPi + AngularMeasure.Pi);
        Assert.True(AngularMeasure.HalfPi + AngularMeasure.Pi < AngularMeasure.Tau);

        Assert.True(AngularMeasure.Zero <= AngularMeasure.HalfPi);
        Assert.True(AngularMeasure.HalfPi <= AngularMeasure.Pi);
        Assert.True(AngularMeasure.Pi <= AngularMeasure.HalfPi + AngularMeasure.Pi);
        Assert.True(AngularMeasure.HalfPi + AngularMeasure.Pi <= AngularMeasure.Tau);

        Assert.True(AngularMeasure.HalfPi > AngularMeasure.Zero);
        Assert.True(AngularMeasure.Pi > AngularMeasure.HalfPi);
        Assert.True(AngularMeasure.HalfPi + AngularMeasure.Pi > AngularMeasure.Pi);
        Assert.True(AngularMeasure.Tau > AngularMeasure.HalfPi + AngularMeasure.Pi);

        Assert.True(AngularMeasure.HalfPi >= AngularMeasure.Zero);
        Assert.True(AngularMeasure.Pi >= AngularMeasure.HalfPi);
        Assert.True(AngularMeasure.HalfPi + AngularMeasure.Pi >= AngularMeasure.Pi);
        Assert.True(AngularMeasure.Tau >= AngularMeasure.HalfPi + AngularMeasure.Pi);
    }

    [Theory(DisplayName = "测试角的转换。")]
    [MemberData(nameof(AngleData))]
    public void ConvertTest(AngularMeasure angle)
    {
        Assert.Equal(angle, AngularMeasure.FromDegree(angle.Degree), NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(angle, AngularMeasure.FromRadian(angle.Radian), NumericsEqualHelper.IsAlmostEqual);
    }

    [Theory(DisplayName = "测试三角函数。")]
    [MemberData(nameof(AngleData))]
    public void TrigonometricFunctions(AngularMeasure angle)
    {
        Assert.Equal(Math.Sin(angle.Radian), angle.Sin(), NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(Math.Cos(angle.Radian), angle.Cos(), NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(Math.Tan(angle.Radian), angle.Tan(), NumericsEqualHelper.IsAlmostEqual);

        Assert.Equal(1, angle.Sin().Square() + angle.Cos().Square(), NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(angle.Sin() / angle.Cos(), angle.Tan(), NumericsEqualHelper.IsAlmostEqual);
    }

    [Theory(DisplayName = "测试角的单位向量。")]
    [MemberData(nameof(AngleData))]
    public void UnitVectorTest(AngularMeasure angle)
    {
        var unitVector = angle.UnitVector;

        Assert.Equal(angle, unitVector.Angle.Normalized, NumericsEqualHelper.IsAlmostEqual);
        Assert.Equal(1.0, unitVector.Length, NumericsEqualHelper.IsAlmostEqual);
    }

    [Theory(DisplayName = "测试角的规范化。")]
    [InlineData(0, 0)]
    [InlineData(Math.Tau, 0)]
    [InlineData(Math.Tau / 2, Math.Tau / 2)]
    [InlineData(-Math.Tau / 4, Math.Tau * 3 / 4)]
    [InlineData(Math.Tau * 3 / 2, Math.Tau / 2)]
    public void NormalizedTest(double radian, double expected)
    {
        var angle = AngularMeasure.FromRadian(radian);
        Assert.Equal(expected, angle.Normalized.Radian, NumericsEqualHelper.IsAlmostEqual);
    }

    #endregion
}