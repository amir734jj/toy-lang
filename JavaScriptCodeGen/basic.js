
/** The Any class is the root of the inheritance hierarchy. */
class Any {

    /** Returns a string representation for the object */
    toString() { return this.toString(); }

    /** return true if this object is equal (in some sense) to the argument */
    equals(x) { return this === x }
}

/** The IO class provides simple input and output operations */
class IO {

    /** Terminates program with given message.
     * Return type of native means that
     * (1) result type is compatible with anything
     * (2) function will not return.
     */
    abort(message) { new Error("stopped"); }

    /** Print the argument (without quotes) to stdout and return itself */
    out(arg : String) = console.log(arg);

    is_null(arg) {
        return arg === null;
    };

    /** Convert to a string and print */
    def out_any(arg : Any) : IO = {
        out(if (is_null(arg)) "null" else arg.toString())
};

    /** Read and return characters from stdin to the next newline character.
     * Return null on end of file.
     */
    def in() { return promot(); }

    /** Get the symbol for this string, creating a new one if needed. */
    def symbol(name) : Symbol = native;

    /** Return the string associated with this symbol. */
    def symbol_name(sym : Symbol) : String = native;

    /** Return the number of arguments from the command line */
    def getArgC() : Int = native;

    /** Return the argument from the command line. 0 = first argument */
    def getArg(i : Int) : String = native;
}

/** A class with no subclasses and which has only one instance.
 * It cannot be instantiated of inherited.
 * The null pointer is not legal for Unit.
 */
class Unit() { }

/** The class of integers in the range -2^31 .. (2^31)-1
 * null is not a legal value for integers, and Int can have no subclasses.
 */
class Int() {
    var value: Any = native;

    /** Convert to a string representation */
    override def toString() : String = native;

    /** Return true if the argument is an int with the same value */
    override def equals(other : Any) : Boolean = native;
}

/** The class of booleans with two legal values: true and false.
 * null is not a legal boolean.
 * It is illegal to inherit from Boolean.
 */
class Boolean() {
    var value: Any = native;

    /** Convert to a string representation */
    override def toString() : String = if (this) "true" else "false";

    /** Return true if the argument is a boolean with the same value */
    override def equals(other : Any) : Boolean = native;

}

/** The class of strings: fixed sequences of characters.
 * Unlike previous version of Cool, strings may be null.
 * It is illegal to inherit from String.
 */
class String() {
    var length : Int = 0;
    var str_field: Any = native;

    override def toString() : String = this;

    /** Return true if the argument is a string with the same characters. */
    override def equals(other : Any) : Boolean = native;

    /** Return length of string. */
    def length() : Int = length;

    /** Return (new) string formed by concatenating self with the argument */
    def concat(arg : String) : String = native;

    /** returns the  substring of self beginning at position start
     * to position end (exclusive)
     * A runtime error is generated if the specified
     * substring is out of range.
     */
    def substring(start : Int, end : Int) : String = native;

    /**
     * Return the character at the given index of the string
     * as an integer.
     */
    def charAt(index : Int) : Int = native;

    /**
     * Return the first index of given substring in this string,
     * or -1 if no such substring.
     */
    def indexOf(sub : String) : Int = {
        // we give a default implementation that is wasteful of space
        // but which enables us to use Cool to write it:
        var n : Int = sub.length();
    var diff : Int = length - n;
    var i : Int = 0;
    var result : Int = -1;
    while (i <= diff) {
        if (substring(i,i+n) == sub) {
            result = i;
            i = diff + 1
        } else {
            i = i + 1
        }
    };
    result
};
}

/**
 * A symbol is an interned string---two symbols with the same string
 * are always identically the same object.  This has two benefits: <ol>
 * <li> equality checking is very efficient
 * <li> we can have a fixed hash code for each symbol. </ol>
 * Creating symbols is restricted to ensure the uniqueness properties.
 * See IO.symbol.  In "Extended Cool", the name is immutable and
 * can be accessed directly.  In Cool, we use IO.symbol_name.
 */
class Symbol() {
    var next: Any = native;
    var name : String = "";
    var hash : Int = 0;

    override def toString() : String = "'".concat(name);

    def hashCode() : Int = hash;
}

/** An array is a mutable fixed-size container holding any objects.
 * The elements are numbered from 0 to size-1.
 * An array may be void.  It is not legal to inherit from ArrayAny.
 */
class ArrayAny {

    constructor(length) {
        this.length = length;
    }

    var array_field: Any = new Array(this.length);

    /** Return length of array. */
    def length() : Int = length;

    /** Return a new array of size s (the original array is unchanged).
     * Any values in the original array that fit within the new array
     * are copied over.  If the new array is larger than the original array,
     * the additional entries start void.  If the new array is smaller
     * than the original array, entries past the end of the new array are
     * not copied over.
     */
    def resize(s : Int) : ArrayAny = native;

    /* Returns the entry at location index.
     * precondition: 0 <= index < length()
     */
    def get(index : Int) : Any = native;

    /* change the entry at location index.
     * return the old value, if any (or null).
     * precondition: 0 <= index < length()
     */
    def set(index : Int, obj : Any) : Any = native;
}

