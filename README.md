[![Build Status](https://travis-ci.com/TimJentzsch/DigitMath.svg?branch=master)](https://travis-ci.com/TimJentzsch/DigitMath)

# DigitMath

A library for digit-based calculations.

## Functionality

Please note that this library is still work in progress.

The purpose of this library is to provide convenient functionality for digit-oriented calculations, such as determining the [multiplicative persistency](https://en.wikipedia.org/wiki/Persistence_of_a_number) of a number.

(Planned) functionality:

- Basic arithmetics (`+`, `-`, `*`, `/`, `%`)
- Digit shifting (`<<`, `>>`)
- Comparisions (`==`, `!=`, `<`, `>`)
- [Radix / Base](https://en.wikipedia.org/wiki/Radix) conversion
- Conversions to and from common [C# types](https://docs.microsoft.com/en-us/dotnet/csharp/tour-of-csharp/types-and-variables) as well as [BigInteger](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.biginteger?view=netframework-4.8)
- Sum, Product
- DigitSum, DigitProduct
- [Number persistency](https://en.wikipedia.org/wiki/Persistence_of_a_number) (additive and multiplicative)
- [Digital roots](https://en.wikipedia.org/wiki/Persistence_of_a_number) (additive and multiplicative)
- [Indexing](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/using-indexers) and [enumeration](https://docs.microsoft.com/en-us/dotnet/api/system.collections.ienumerable?view=netframework-4.8)

If you have other requests, please create a feature request in the issue section.

## Reliability

We are trying to ensure via unit tests that everything works as expected, but we might have missed serveral test cases.

If you found a case in which the library returns unexpected results, 
please create a bug report in the issue section and we will try to fix it as soon as possible.

## Performance

This library is still in its early stages of development. 
We can't recommend yet to use it in performance-critical scenarios.

We are still prioritizing to expand the functionality of the library before improving the performance.
However, if you have specific performance problems / suggestions, 
please create a performance report in the issue section and we will try to fix it later.
