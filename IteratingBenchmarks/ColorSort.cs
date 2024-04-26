using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IteratingBenchmarks;

public class ColorSort
{
    public static void SortRedForeach(in Span<Color> colors)
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
        {
            int index = color.Red;
            int bucketIndex = currentBucketIndex[index];
            currentBucketIndex[index]++;
            buckets[bucketIndex] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedFor(in Span<Color> colors)
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
            int index = color.Red;
            int bucketIndex = currentBucketIndex[index];
            currentBucketIndex[index]++;
            buckets[bucketIndex] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static void SortRedUnsafeAdd(in Span<Color> colors)
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
            int index = color.Red;
            int bucketIndex = currentBucketIndex[index];
            currentBucketIndex[index]++;
            buckets[bucketIndex] = color;
        }

        buckets.CopyTo(colors);

        ArrayPool<Color>.Shared.Return(bucketsArray);
    }

    public static unsafe void SortRedPtr(in Span<Color> colors)
    {
        fixed (Color* ptr = colors)
        {
            Color* end = ptr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            for (Color* color = ptr; color < end; ++color)
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

            for (Color* color = ptr; color < end; ++color)
            {
                int index = (*color).Red;
                int bucketIndex = currentBucketIndex[index];
                currentBucketIndex[index]++;
                buckets[bucketIndex] = (*color);
            }

            buckets.CopyTo(colors);

            ArrayPool<Color>.Shared.Return(bucketsArray);
        }
    }

    public static unsafe void SortRedFullPtr(in Span<Color> colors)
    {
        fixed (Color* colorsPtr = colors)
        {
            Color* colorsEnd = colorsPtr + colors.Length;

            Span<int> counts = stackalloc int[256];
            counts.Clear();

            fixed (int* countsPtr = counts)
            {
                int* countsEnd = countsPtr + counts.Length;

                for (Color* color = colorsPtr; color < colorsEnd; ++color)
                    ++(*(countsPtr + (*color).Red));

                Color[] bucketsArray = ArrayPool<Color>.Shared.Rent(colors.Length);
                Span<Color> buckets = bucketsArray.AsSpan()[..colors.Length];

                fixed (Color* bucketsPtr = buckets)
                {
                    Span<int> currentBucketIndex = stackalloc int[256];
                    fixed (int* currentBucketIndexPtr = currentBucketIndex)
                    {
                        int offset = 0;
                        int* currentBucketIndx = currentBucketIndexPtr;
                        for (int* count = countsPtr; count < countsEnd; ++count, ++currentBucketIndx)
                        {
                            (*currentBucketIndx) = offset;
                            offset += (*count);
                        }

                        for (Color* color = colorsPtr; color < colorsEnd; ++color)
                        {
                            int index = (*color).Red;
                            int* bucketIndexPtr = currentBucketIndexPtr + index;
                            Color* bucketPtr = bucketsPtr + (*bucketIndexPtr);
                            ++(*bucketIndexPtr);
                            *bucketPtr = *color;
                        }

                        buckets.CopyTo(colors);

                        ArrayPool<Color>.Shared.Return(bucketsArray);
                    }
                }
            }
        }
    }
}