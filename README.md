# C# Iteration-Speed-Comparison

I investigated this some time ago after seeing a video that claimed the usage of `UnsafeAdd` is the fastest way of iterating in c#, which for my usecase never hold true.  

The topic of `for`- vs `foreach`-performance and compiler optimizations for `foreach` where then briefly touched by the latest .NET deep-dive ( https://www.youtube.com/watch?v=W4-NVVNwCWs) which got me interested in looking at it again.   

This repo contains some benchmarks where the same algorithm (used to sort 24 bit colors data by the red channel) is implemented with different ways of iterating the data. (The benchmark code is held as simple as possible with all validations and error-handling removed.)   
I consider the results from this extremely interesting as not only does `foreach` consistently performs best of all managed approaches (only pointers are faster), but the difference between `for` and `foreach` is huge and there is a noticable difference between AMD and Intel CPUs.   

I'd be highly interested in seeing some more result for different CPUs! (Just open an issue with your results if you're interested in this.)

**Update**: TIL `For` + `in Span` is a bad combo (which in hindsight is quite logical).   
Added some more benchmarks to test the differences between the code generated for the `foreach` and the `for`. But `forach` is still the best (the gap is way smaller though).

**Update 2**: Changed the baseline to not use `in`-parameters, improved the FullPtr-Implementation.   
Still wondering, why the `in` + copy local variation outperforms the "normal" one consistently.

## Results

### AMD Ryzen 9 5900x

| Method               | Mean     | Error     | StdDev    | Median   | Allocated |
|--------------------- |---------:|----------:|----------:|---------:|----------:|
| Foreach              | 1.889 ms | 0.0266 ms | 0.1314 ms | 1.825 ms |     400 B |
| For                  | 1.853 ms | 0.0171 ms | 0.0829 ms | 1.821 ms |     736 B |
| UnsafeAdd            | 1.874 ms | 0.0208 ms | 0.1026 ms | 1.833 ms |     736 B |
| Ptr                  | 1.573 ms | 0.0206 ms | 0.1032 ms | 1.524 ms |     736 B |
| FullPtr              | 1.532 ms | 0.0104 ms | 0.0496 ms | 1.515 ms |     736 B |
|                      |          |           |           |          |           |
| ForCopyLocal         | 1.844 ms | 0.0203 ms | 0.0981 ms | 1.810 ms |     736 B |
| ForeachCopyLocal     | 1.893 ms | 0.0263 ms | 0.1282 ms | 1.845 ms |     736 B |
| UnsafeAddCopyLocal   | 1.896 ms | 0.0219 ms | 0.1084 ms | 1.852 ms |     736 B |
| PtrCopyLocal         | 1.576 ms | 0.0224 ms | 0.1115 ms | 1.522 ms |     736 B |
| FullPtrCopyLocal     | 1.568 ms | 0.0217 ms | 0.1077 ms | 1.516 ms |     736 B |
|                      |          |           |           |          |           |
| ForeachIn            | 1.801 ms | 0.0218 ms | 0.1076 ms | 1.752 ms |     736 B |
| ForIn                | 3.252 ms | 0.0125 ms | 0.0600 ms | 3.228 ms |     736 B |
| UnsafeAddIn          | 2.126 ms | 0.0042 ms | 0.0204 ms | 2.121 ms |     736 B |
| PtrIn                | 1.519 ms | 0.0046 ms | 0.0223 ms | 1.514 ms |     736 B |
| FullPtrIn            | 1.522 ms | 0.0045 ms | 0.0219 ms | 1.514 ms |     736 B |
|                      |          |           |           |          |           |
| ForInCopyLocal       | 1.821 ms | 0.0057 ms | 0.0278 ms | 1.813 ms |     736 B |
| ForeachInCopyLocal   | 1.845 ms | 0.0207 ms | 0.0989 ms | 1.818 ms |     736 B |
| UnsafeAddIdCopyLocal | 1.841 ms | 0.0056 ms | 0.0277 ms | 1.834 ms |     736 B |
| PtrInCopyLocal       | 1.518 ms | 0.0048 ms | 0.0232 ms | 1.510 ms |     736 B |
| FullPtrInCopyLocal   | 1.522 ms | 0.0041 ms | 0.0202 ms | 1.517 ms |     736 B |




### Intel i7-11800H

*outdated*