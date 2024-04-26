using System.Diagnostics;

namespace IteratingBenchmarks;

[DebuggerDisplay("[{Red}, {Green}, {Blue}]")]
public readonly struct Color(byte r, byte g, byte b)
{
    #region Properties & Fields

    public readonly byte Red = r;
    public readonly byte Green = g;
    public readonly byte Blue = b;

    #endregion

    #region Methods

    public override string ToString() => $"[{Red}, {Green}, {Blue}]";

    #endregion
}