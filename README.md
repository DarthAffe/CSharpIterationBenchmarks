# C# Iteration-Speed-Comparison

I investigated this some time ago after seeing a video that claimed the usage of `UnsafeAdd` is the fastest way of iterating in c#, which for my usecase never hold true.  

The topic of `for`- vs `foreach`-performance and compiler optimizations for `foreach` where then briefly touched by the latest .NET deep-dive ( https://www.youtube.com/watch?v=W4-NVVNwCWs) which got me interested in looking at it again.   

This repo contains some benchmarks where the same algorithm (used to sort 24 bit colors data by the red channel) is implemented with different ways of iterating the data. (The benchmark code is held as simple as possible with all validations and error-handling removed.)   
I consider the results from this extremely interesting as not only does `foreach` consistently performs best of all managed approaches (only pointers are faster), but the difference between `for` and `foreach` is huge and there is a noticable difference between AMD and Intel CPUs.   

I'd be highly interested in seeing some more result for different CPUs! (Just open an issue with your results if you're interested in this.)

**Update**: `For` + `in Span` seems to be a bad combo. Added some more benchmarks to test the differences between the code generated for the `foreach` and the `for`. But `forach` is still the best (the gap is way smaller though).

## Results

### AMD Ryzen 9 5900x

| Method                              | Mean     | Error     | StdDev    | Median   | Allocated |
|------------------------------------ |---------:|----------:|----------:|---------:|----------:|
| For                                 | 3.314 ms | 0.0286 ms | 0.1432 ms | 3.239 ms |     736 B |
| Foreach                             | 1.814 ms | 0.0253 ms | 0.1267 ms | 1.750 ms |     736 B |
| UnsafeAdd                           | 2.139 ms | 0.0111 ms | 0.0533 ms | 2.122 ms |     736 B |
| Ptr                                 | 1.550 ms | 0.0211 ms | 0.1030 ms | 1.507 ms |     736 B |
| FullPtr                             | 1.904 ms | 0.0089 ms | 0.0420 ms | 1.889 ms |     736 B |
|                                     |          |           |           |          |           |
| ForCopyLocal                        | 1.858 ms | 0.0186 ms | 0.0904 ms | 1.817 ms |     736 B |
| ForeachCopyLocal                    | 1.824 ms | 0.0103 ms | 0.0488 ms | 1.806 ms |     736 B |
| UnsafeAddCopyLocal                  | 1.890 ms | 0.0265 ms | 0.1313 ms | 1.827 ms |     736 B |
| PtrCopyLocal                        | 1.525 ms | 0.0120 ms | 0.0561 ms | 1.503 ms |     736 B |
| FullPtrCopyLocal                    | 1.588 ms | 0.0051 ms | 0.0239 ms | 1.579 ms |     736 B |
|                                     |          |           |           |          |           |
| ForNoIn                             | 1.812 ms | 0.0049 ms | 0.0233 ms | 1.804 ms |     736 B |
| ForeachNoIn                         | 1.863 ms | 0.0285 ms | 0.1362 ms | 1.821 ms |     736 B |
| UnsafeAddNoIn                       | 1.823 ms | 0.0041 ms | 0.0197 ms | 1.817 ms |     736 B |
| PtrNoIn                             | 1.568 ms | 0.0418 ms | 0.2032 ms | 1.506 ms |     736 B |
| FullPtrNoIn                         | 1.586 ms | 0.0052 ms | 0.0249 ms | 1.578 ms |     736 B |
|                                     |          |           |           |          |           |
| ForReverseIncrements                | 3.224 ms | 0.0043 ms | 0.0215 ms | 3.219 ms |     736 B |
| ForeachReverseIncrements            | 1.870 ms | 0.0643 ms | 0.3183 ms | 1.738 ms |     736 B |
| UnsafeAddReverseIncrements          | 2.174 ms | 0.0246 ms | 0.1201 ms | 2.129 ms |     736 B |
| PtrReverseIncrements                | 1.579 ms | 0.0350 ms | 0.1736 ms | 1.509 ms |     736 B |
|                                     |          |           |           |          |           |
| ForCopyLocalReverseIncrements       | 1.806 ms | 0.0034 ms | 0.0163 ms | 1.800 ms |     736 B |
| ForeachCopyLocalReverseIncrements   | 1.802 ms | 0.0029 ms | 0.0139 ms | 1.797 ms |     736 B |
| UnsafeAddCopyLocalReverseIncrements | 1.828 ms | 0.0090 ms | 0.0430 ms | 1.815 ms |     736 B |
| PtrCopyLocalReverseIncrements       | 1.573 ms | 0.0342 ms | 0.1692 ms | 1.501 ms |     736 B |
|                                     |          |           |           |          |           |
| ForNoInReverseIncrements            | 1.834 ms | 0.0241 ms | 0.1154 ms | 1.800 ms |     736 B |
| ForeachNoInReverseIncrements        | 1.801 ms | 0.0021 ms | 0.0104 ms | 1.797 ms |     736 B |
| UnsafeAddNoInReverseIncrements      | 1.815 ms | 0.0027 ms | 0.0133 ms | 1.811 ms |     736 B |
| PtrNoInReverseIncrements            | 1.513 ms | 0.0047 ms | 0.0228 ms | 1.504 ms |     736 B |



### Intel i7-11800H

| Method                              | Mean     | Error     | StdDev    | Median   | Allocated |
|------------------------------------ |---------:|----------:|----------:|---------:|----------:|
| For                                 | 2.471 ms | 0.0537 ms | 0.2630 ms | 2.399 ms |     736 B |
| Foreach                             | 2.061 ms | 0.0339 ms | 0.1637 ms | 2.016 ms |     736 B |
| UnsafeAdd                           | 2.096 ms | 0.0359 ms | 0.1744 ms | 2.035 ms |     736 B |
| Ptr                                 | 1.762 ms | 0.0257 ms | 0.1248 ms | 1.717 ms |     736 B |
| FullPtr                             | 1.786 ms | 0.0237 ms | 0.1139 ms | 1.750 ms |     736 B |
|                                     |          |           |           |          |           |
| ForCopyLocal                        | 2.035 ms | 0.0364 ms | 0.1775 ms | 1.969 ms |     736 B |
| ForeachCopyLocal                    | 1.997 ms | 0.0267 ms | 0.1291 ms | 1.947 ms |     736 B |
| UnsafeAddCopyLocal                  | 2.045 ms | 0.0344 ms | 0.1674 ms | 1.984 ms |     400 B |
| PtrCopyLocal                        | 1.728 ms | 0.0225 ms | 0.1099 ms | 1.686 ms |     736 B |
| FullPtrCopyLocal                    | 1.748 ms | 0.0237 ms | 0.1142 ms | 1.704 ms |     736 B |
|                                     |          |           |           |          |           |
| ForNoIn                             | 2.023 ms | 0.0408 ms | 0.1989 ms | 1.952 ms |     736 B |
| ForeachNoIn                         | 2.002 ms | 0.0301 ms | 0.1460 ms | 1.946 ms |     736 B |
| UnsafeAddNoIn                       | 1.971 ms | 0.0213 ms | 0.1022 ms | 1.931 ms |     736 B |
| PtrNoIn                             | 1.713 ms | 0.0251 ms | 0.1212 ms | 1.668 ms |     736 B |
| FullPtrNoIn                         | 1.754 ms | 0.0239 ms | 0.1161 ms | 1.706 ms |     736 B |
|                                     |          |           |           |          |           |
| ForReverseIncrements                | 2.362 ms | 0.0327 ms | 0.1599 ms | 2.296 ms |     736 B |
| ForeachReverseIncrements            | 1.988 ms | 0.0277 ms | 0.1348 ms | 1.940 ms |      64 B |
| UnsafeAddReverseIncrements          | 2.036 ms | 0.0296 ms | 0.1411 ms | 2.005 ms |     736 B |
| PtrReverseIncrements                | 1.717 ms | 0.0252 ms | 0.1220 ms | 1.675 ms |     736 B |
|                                     |          |           |           |          |           |
| ForCopyLocalReverseIncrements       | 1.967 ms | 0.0237 ms | 0.1141 ms | 1.928 ms |     736 B |
| ForeachCopyLocalReverseIncrements   | 1.952 ms | 0.0214 ms | 0.1033 ms | 1.919 ms |     736 B |
| UnsafeAddCopyLocalReverseIncrements | 1.982 ms | 0.0230 ms | 0.1110 ms | 1.936 ms |     736 B |
| PtrCopyLocalReverseIncrements       | 1.732 ms | 0.0220 ms | 0.1064 ms | 1.722 ms |     736 B |
|                                     |          |           |           |          |           |
| ForNoInReverseIncrements            | 1.980 ms | 0.0240 ms | 0.1149 ms | 1.962 ms |     736 B |
| ForeachNoInReverseIncrements        | 1.972 ms | 0.0202 ms | 0.0959 ms | 1.965 ms |     736 B |
| UnsafeAddNoInReverseIncrements      | 1.986 ms | 0.0231 ms | 0.1112 ms | 1.950 ms |     736 B |
| PtrNoInReverseIncrements            | 1.693 ms | 0.0197 ms | 0.0945 ms | 1.659 ms |     736 B |