# C# Iteration-Speed-Comparison

I investigated this some time ago after seeing a video that claimed the usage of `UnsafeAdd` is the fastest way of iterating in c#, which for my usecase never hold true.  

The topic of `for`- vs `foreach`-performance and compiler optimizations for `foreach` where then briefly touched by the latest .NET deep-dive ( https://www.youtube.com/watch?v=W4-NVVNwCWs) which got me interested in looking at it again.   

This repo contains some benchmarks where the same algorithm (used to sort 24 bit colors data by the red channel) is implemented with different ways of iterating the data. (The benchmark code is held as simple as possible with all validations and error-handling removed.)   
I consider the results from this extremely interesting as not only does `foreach` consistently performs best of all managed approaches (only pointers are faster), but the difference between `for` and `foreach` is huge and there is a noticable difference between AMD and Intel CPUs.   

I'd be highly interested in seeing some more result for different CPUs! (Just open an issue with your results if you're interested in this.)

## Results

### AMD Ryzen 9 5900x

| Method    | Mean     | Error     | StdDev    | Median   | Allocated |
|---------- |---------:|----------:|----------:|---------:|----------:|
| For       | 3.279 ms | 0.0142 ms | 0.0680 ms | 3.258 ms |     736 B |
| Foreach   | 1.833 ms | 0.0503 ms | 0.2492 ms | 1.742 ms |     736 B |
| UnsafeAdd | 2.165 ms | 0.0142 ms | 0.0686 ms | 2.139 ms |     736 B |
| Ptr       | 1.569 ms | 0.0244 ms | 0.1191 ms | 1.515 ms |     736 B |
| FullPtr   | 1.929 ms | 0.0181 ms | 0.0873 ms | 1.897 ms |     736 B |



### Intel i7-11800H

| Method    | Mean     | Error     | StdDev    | Median   | Allocated |
|---------- |---------:|----------:|----------:|---------:|----------:|
| For       | 2.477 ms | 0.0538 ms | 0.2602 ms | 2.404 ms |     736 B |
| Foreach   | 2.052 ms | 0.0269 ms | 0.1289 ms | 2.022 ms |     736 B |
| UnsafeAdd | 2.098 ms | 0.0321 ms | 0.1542 ms | 2.061 ms |     736 B |
| Ptr       | 1.772 ms | 0.0260 ms | 0.1250 ms | 1.741 ms |     736 B |
| FullPtr   | 1.836 ms | 0.0243 ms | 0.1175 ms | 1.802 ms |     736 B |