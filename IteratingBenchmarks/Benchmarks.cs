using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace IteratingBenchmarks;

[LongRunJob(RuntimeMoniker.Net80)]
[HtmlExporter]
[MemoryDiagnoser]
public class Benchmarks
{
    private readonly Color[] _randomData;
    private Color[] _iterationColors = Array.Empty<Color>();

    public Benchmarks()
    {
        Random random = new(12345); // It shouldn't make a difference here, but fixed seed for reproducibility

        _randomData = new Color[1024 * 1024];
        for (int i = 0; i < _randomData.Length; i++)
            _randomData[i] = new Color((byte)random.Next(byte.MaxValue), (byte)random.Next(byte.MaxValue), (byte)random.Next(byte.MaxValue));
    }

    [IterationSetup]
    public void IterationSetup()
    {
        _iterationColors = new Color[_randomData.Length];
        Array.Copy(_randomData, _iterationColors, _iterationColors.Length);
    }

    // ----------------------------------------------------------------------------
    
    [Benchmark]
    public void For() => ColorSort.SortRedFor(_iterationColors);

    [Benchmark]
    public void Foreach() => ColorSort.SortRedForeach(_iterationColors);

    [Benchmark]
    public void UnsafeAdd() => ColorSort.SortRedUnsafeAdd(_iterationColors);

    [Benchmark]
    public void Ptr() => ColorSort.SortRedPtr(_iterationColors);

    [Benchmark]
    public void FullPtr() => ColorSort.SortRedFullPtr(_iterationColors);

    // ----------------------------------------------------------------------------

    [Benchmark]
    public void ForCopyLocal() => ColorSort.SortRedForCopyLocal(_iterationColors);

    [Benchmark]
    public void ForeachCopyLocal() => ColorSort.SortRedForeachCopyLocal(_iterationColors);

    [Benchmark]
    public void UnsafeAddCopyLocal() => ColorSort.SortRedUnsafeAddCopyLocal(_iterationColors);

    [Benchmark]
    public void PtrCopyLocal() => ColorSort.SortRedPtrCopyLocal(_iterationColors);

    [Benchmark]
    public void FullPtrCopyLocal() => ColorSort.SortRedFullPtrCopyLocal(_iterationColors);

    // ----------------------------------------------------------------------------
    
    [Benchmark]
    public void ForIn() => ColorSort.SortRedForIn(_iterationColors);

    [Benchmark]
    public void ForeachIn() => ColorSort.SortRedForeachIn(_iterationColors);

    [Benchmark]
    public void UnsafeAddIn() => ColorSort.SortRedUnsafeAddIn(_iterationColors);

    [Benchmark]
    public void PtrIn() => ColorSort.SortRedPtrIn(_iterationColors);

    [Benchmark]
    public void FullPtrIn() => ColorSort.SortRedFullPtrIn(_iterationColors);

    // ----------------------------------------------------------------------------

    [Benchmark]
    public void ForInCopyLocal() => ColorSort.SortRedForInCopyLocal(_iterationColors);

    [Benchmark]
    public void ForeachInCopyLocal() => ColorSort.SortRedForeachInCopyLocal(_iterationColors);

    [Benchmark]
    public void UnsafeAddIdCopyLocal() => ColorSort.SortRedUnsafeAddInCopyLocal(_iterationColors);

    [Benchmark]
    public void PtrInCopyLocal() => ColorSort.SortRedPtrInCopyLocal(_iterationColors);

    [Benchmark]
    public void FullPtrInCopyLocal() => ColorSort.SortRedFullPtrInCopyLocal(_iterationColors);
}
