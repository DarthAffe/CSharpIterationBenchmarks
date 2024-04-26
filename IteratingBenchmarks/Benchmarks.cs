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

    [Benchmark]
    public void ForNoIn() => ColorSort.SortRedForNoIn(_iterationColors);

    [Benchmark]
    public void ForeachNoIn() => ColorSort.SortRedForeachNoIn(_iterationColors);

    [Benchmark]
    public void UnsafeAddNoIn() => ColorSort.SortRedUnsafeAddNoIn(_iterationColors);

    [Benchmark]
    public void PtrNoIn() => ColorSort.SortRedPtrNoIn(_iterationColors);

    [Benchmark]
    public void FullPtrNoIn() => ColorSort.SortRedFullPtrNoIn(_iterationColors);


    [Benchmark]
    public void ForReverseIncrements() => ColorSort.SortRedForReverseIncrements(_iterationColors);

    [Benchmark]
    public void ForeachReverseIncrements() => ColorSort.SortRedForeachReverseIncrements(_iterationColors);

    [Benchmark]
    public void UnsafeAddReverseIncrements() => ColorSort.SortRedUnsafeAddReverseIncrements(_iterationColors);

    [Benchmark]
    public void PtrReverseIncrements() => ColorSort.SortRedPtrReverseIncrements(_iterationColors);

    [Benchmark]
    public void ForCopyLocalReverseIncrements() => ColorSort.SortRedForCopyLocalReverseIncrements(_iterationColors);

    [Benchmark]
    public void ForeachCopyLocalReverseIncrements() => ColorSort.SortRedForeachCopyLocalReverseIncrements(_iterationColors);

    [Benchmark]
    public void UnsafeAddCopyLocalReverseIncrements() => ColorSort.SortRedUnsafeAddCopyLocalReverseIncrements(_iterationColors);

    [Benchmark]
    public void PtrCopyLocalReverseIncrements() => ColorSort.SortRedPtrCopyLocalReverseIncrements(_iterationColors);

    [Benchmark]
    public void ForNoInReverseIncrements() => ColorSort.SortRedForNoInReverseIncrements(_iterationColors);

    [Benchmark]
    public void ForeachNoInReverseIncrements() => ColorSort.SortRedForeachNoInReverseIncrements(_iterationColors);

    [Benchmark]
    public void UnsafeAddNoInReverseIncrements() => ColorSort.SortRedUnsafeAddNoInReverseIncrements(_iterationColors);

    [Benchmark]
    public void PtrNoInReverseIncrements() => ColorSort.SortRedPtrNoInReverseIncrements(_iterationColors);
}
