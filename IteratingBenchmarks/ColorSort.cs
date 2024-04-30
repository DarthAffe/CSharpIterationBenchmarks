using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratingBenchmarks;

public static class ColorSort
{
    public static void SortRedForeach(Span<Color> colors)
    {
        Span<int> counts = stackalloc int[256];
        counts.Clear();

        foreach (Color t in colors)
            counts[t.Red]++;

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        foreach (Color color in colors)
            buckets[currentBucketIndex[color.Red]++] = color;

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedFor(Span<Color> colors)
    {
        Span<int> counts = stackalloc int[256];
        counts.Clear();

        for (int i = 0; i < colors.Length; i++)
            counts[colors[i].Red]++;

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = colors[i];
            buckets[currentBucketIndex[color.Red]++] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedUnsafeAdd(Span<Color> colors)
    {
        ref Color colorsReference = ref MemoryMarshal.GetReference(colors);

        Span<int> counts = stackalloc int[256];
        counts.Clear();

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = Unsafe.Add(ref colorsReference, i);
            counts[color.Red]++;
        }

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = Unsafe.Add(ref colorsReference, i);
            buckets[currentBucketIndex[color.Red]++] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static unsafe void SortRedPtr(Span<Color> colors)
    {
        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; color++)
                counts[(*color).Red]++;

            Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
            Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
            Span<int> currentBucketIndex = stackalloc int[256];

            int offset = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                currentBucketIndex[i] = offset;
                offset += counts[i];
            }

            for (Color* color = ptr; color < end; color++)
                buckets[currentBucketIndex[(*color).Red]++] = (*color);

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }

    public static unsafe void SortRedFullPtr(Span<Color> colors)
    {
        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; color++)
                counts[(*color).Red]++;

            Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
            Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
            int* currentBucketIndex = stackalloc int[256];

            int offset = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                currentBucketIndex[i] = offset;
                offset += counts[i];
            }

            for (Color* color = ptr; color < end; color++)
                buckets[currentBucketIndex[(*color).Red]++] = (*color);

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }

    // ----------------------------------------------------------------------------

    public static void SortRedForeachCopyLocal(Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        Span<int> counts = stackalloc int[256];
        counts.Clear();

        foreach (Color t in colors)
            counts[t.Red]++;

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        foreach (Color color in colors)
            buckets[currentBucketIndex[color.Red]++] = color;

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedForCopyLocal(Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        Span<int> counts = stackalloc int[256];
        counts.Clear();

        for (int i = 0; i < colors.Length; i++)
            counts[colors[i].Red]++;

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = colors[i];
            buckets[currentBucketIndex[color.Red]++] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedUnsafeAddCopyLocal(Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        ref Color colorsReference = ref MemoryMarshal.GetReference(colors);

        Span<int> counts = stackalloc int[256];
        counts.Clear();

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = Unsafe.Add(ref colorsReference, i);
            counts[color.Red]++;
        }

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = Unsafe.Add(ref colorsReference, i);
            buckets[currentBucketIndex[color.Red]++] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static unsafe void SortRedPtrCopyLocal(Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; color++)
                counts[(*color).Red]++;

            Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
            Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
            Span<int> currentBucketIndex = stackalloc int[256];

            int offset = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                currentBucketIndex[i] = offset;
                offset += counts[i];
            }

            for (Color* color = ptr; color < end; color++)
                buckets[currentBucketIndex[(*color).Red]++] = (*color);

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }

    public static unsafe void SortRedFullPtrCopyLocal(Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; color++)
                counts[(*color).Red]++;

            Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
            Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
            int* currentBucketIndex = stackalloc int[256];

            int offset = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                currentBucketIndex[i] = offset;
                offset += counts[i];
            }

            for (Color* color = ptr; color < end; color++)
                buckets[currentBucketIndex[(*color).Red]++] = (*color);

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }

    // ----------------------------------------------------------------------------

    public static void SortRedForeachIn(in Span<Color> colors)
    {
        Span<int> counts = stackalloc int[256];
        counts.Clear();

        foreach (Color t in colors)
            counts[t.Red]++;

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        foreach (Color color in colors)
            buckets[currentBucketIndex[color.Red]++] = color;

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedForIn(in Span<Color> colors)
    {
        Span<int> counts = stackalloc int[256];
        counts.Clear();

        for (int i = 0; i < colors.Length; i++)
            counts[colors[i].Red]++;

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = colors[i];
            buckets[currentBucketIndex[color.Red]++] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedUnsafeAddIn(in Span<Color> colors)
    {
        ref Color colorsReference = ref MemoryMarshal.GetReference(colors);

        Span<int> counts = stackalloc int[256];
        counts.Clear();

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = Unsafe.Add(ref colorsReference, i);
            counts[color.Red]++;
        }

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = Unsafe.Add(ref colorsReference, i);
            buckets[currentBucketIndex[color.Red]++] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static unsafe void SortRedPtrIn(in Span<Color> colors)
    {
        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; color++)
                counts[(*color).Red]++;

            Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
            Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
            Span<int> currentBucketIndex = stackalloc int[256];

            int offset = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                currentBucketIndex[i] = offset;
                offset += counts[i];
            }

            for (Color* color = ptr; color < end; color++)
                buckets[currentBucketIndex[(*color).Red]++] = (*color);

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }

    public static unsafe void SortRedFullPtrIn(in Span<Color> colors)
    {
        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; color++)
                counts[(*color).Red]++;

            Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
            Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
            int* currentBucketIndex = stackalloc int[256];

            int offset = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                currentBucketIndex[i] = offset;
                offset += counts[i];
            }

            for (Color* color = ptr; color < end; color++)
                buckets[currentBucketIndex[(*color).Red]++] = (*color);

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }

    // ----------------------------------------------------------------------------

    public static void SortRedForeachInCopyLocal(in Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        Span<int> counts = stackalloc int[256];
        counts.Clear();

        foreach (Color t in colors)
            counts[t.Red]++;

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        foreach (Color color in colors)
            buckets[currentBucketIndex[color.Red]++] = color;

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedForInCopyLocal(in Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        Span<int> counts = stackalloc int[256];
        counts.Clear();

        for (int i = 0; i < colors.Length; i++)
            counts[colors[i].Red]++;

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = colors[i];
            buckets[currentBucketIndex[color.Red]++] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedUnsafeAddInCopyLocal(in Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        ref Color colorsReference = ref MemoryMarshal.GetReference(colors);

        Span<int> counts = stackalloc int[256];
        counts.Clear();

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = Unsafe.Add(ref colorsReference, i);
            counts[color.Red]++;
        }

        Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
        Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
        Span<int> currentBucketIndex = stackalloc int[256];

        int offset = 0;
        for (int i = 0; i < counts.Length; i++)
        {
            currentBucketIndex[i] = offset;
            offset += counts[i];
        }

        for (int i = 0; i < colors.Length; i++)
        {
            Color color = Unsafe.Add(ref colorsReference, i);
            buckets[currentBucketIndex[color.Red]++] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static unsafe void SortRedPtrInCopyLocal(in Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; color++)
                counts[(*color).Red]++;

            Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
            Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
            Span<int> currentBucketIndex = stackalloc int[256];

            int offset = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                currentBucketIndex[i] = offset;
                offset += counts[i];
            }

            for (Color* color = ptr; color < end; color++)
                buckets[currentBucketIndex[(*color).Red]++] = (*color);

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }

    public static unsafe void SortRedFullPtrInCopyLocal(in Span<Color> colorsParam)
    {
        Span<Color> colors = colorsParam;

        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; color++)
                counts[(*color).Red]++;

            Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
            Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];
            int* currentBucketIndex = stackalloc int[256];

            int offset = 0;
            for (int i = 0; i < counts.Length; i++)
            {
                currentBucketIndex[i] = offset;
                offset += counts[i];
            }

            for (Color* color = ptr; color < end; color++)
                buckets[currentBucketIndex[(*color).Red]++] = (*color);

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }
}