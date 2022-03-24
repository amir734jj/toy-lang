# toy-lang
Toy programming language: my attempt at creating a new toy/experimental language in just 2 weeks.

Please check out the Wiki page

```scala
/**
 * This is an example of multiline comment
 */
class Driver() extends IO() {
  // This is an example of single line comment
  def fibonacci(n: Int): Int = if (n <= 1)
    n
  else
    fibonacci(n - 1) + fibonacci(n - 2);

  def assertEquals(expected: Any, actual: Any, msg: String): IO = if (expected != actual)
    out(
      "["
        .concat(msg)
        .concat("]")
        .concat("expected: ")
        .concat(expected.toString())
        .concat(" but received: ")
        .concat(actual.toString())
    )
  else
    out("passed!");

  { assertEquals(34, fibonacci(9), "fibonacci") }
}
```


TODO:
- ~Antlr4~
- ~FParsec.Sharp~
- ~Semantics~
- ~JavaScript~
- MIPS
- .NET IL
- LLVM
- C
