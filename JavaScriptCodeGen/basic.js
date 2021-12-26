/** The Any class is the root of the inheritance hierarchy. */
class Any {

    /** Returns a string representation for the object */
    toString() {
        return JSON.stringify(this);
    }

    /** return true if this object is equal (in some sense) to the argument */
    equals(x) {
        return this === x
    }
}

/** The IO class provides simple input and output operations */
class IO {

    /** Terminates program with given message.
     * Return type of native means that
     * (1) result type is compatible with anything
     * (2) function will not return.
     */
    abort(message) {
        new Error(message.str_field);
    }

    /** Print the argument (without quotes) to stdout and return itself */
    out(arg) {
        console.log(arg);
        return this;
    }

    is_null(arg) {
        return arg === null;
    };

    /** Convert to a string and print */
    out_any(arg) {
        return this.out((this.is_null(arg)) ? "null" : arg.toString())
    }

    /** Read and return characters from stdin to the next newline character.
     * Return null on end of file.
     */
    in() {
        return prompt();
    }

    /** Get the symbol for this string, creating a new one if needed. */
    symbol(name) {
        let symb = new Symbol();
        symb.name = name;
        return symb;
    }

    /** Return the string associated with this symbol. */
    symbol_name(sym) {
        return sym.name;
    }
}

/** A class with no subclasses and which has only one instance.
 * It cannot be instantiated of inherited.
 * The null pointer is not legal for Unit.
 */
class Unit {
}

/** The class of integers in the range -2^31 .. (2^31)-1
 * null is not a legal value for integers, and Int can have no subclasses.
 */
class Int {
    
    constructor() {
        this.value = 0;
    }

    /** Convert to a string representation */
    toString() {
        return this.value.toString();
    }

    /** Return true if the argument is an int with the same value */
    equals(other) {
        return this.value === other.value;
    }
}

/** The class of booleans with two legal values: true and false.
 * null is not a legal boolean.
 * It is illegal to inherit from Boolean.
 */
class Boolean {
    constructor() {
        this.value = false;
    }

    /** Convert to a string representation */
    toString() {
        return this.value.toString();
    }

    /** Return true if the argument is a boolean with the same value */
    equals(other) {
        return this.value === other.value;
    }
}

/** The class of strings: fixed sequences of characters.
 * Unlike previous version of Cool, strings may be null.
 * It is illegal to inherit from String.
 */
class StringC {

    constructor() {
        this.length = 0;
        this.str_field = "";
    }

    toString() {
        return this;
    };

    /** Return true if the argument is a string with the same characters. */
    equals(other) {
        return this.str_field === other.str_field;
    }

    /** Return length of string. */
    length() {
        return this.length;
    }

    /** Return (new) string formed by concatenating self with the argument */
    concat(arg) {
        let result = new StringC();
        result.str_field = this.str_field + arg.str_field;
        result.length = this.length + arg.length;
        return result;
    }

    /** returns the  substring of self beginning at position start
     * to position end (exclusive)
     * A runtime error is generated if the specified
     * substring is out of range.
     */
    substring(start, end) {
        let result = new StringC();
        result.str_field = this.str_field.substring(start, end);
        return result;
    }

    /**
     * Return the character at the given index of the string
     * as an integer.
     */
    charAt(index) {
        return this.str_field.charAt(index);
    }

    /**
     * Return the first index of given substring in this string,
     * or -1 if no such substring.
     */
    indexOf(sub) {
        // we give a default implementation that is wasteful of space
        // but which enables us to use Cool to write it:
        let n = sub.length();
        let diff = length - n;
        let i = 0;
        let result = -1;
        while (i <= diff) {
            if (this.substring(i, i + n) === sub) {
                result = i;
                i = diff + 1
            } else {
                i = i + 1
            }
        }
        return result
    }
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
class Symbol {
    constructor() {
        this.name = "";
    }

    toString() {
        return "'".concat(this.name);
    }

    hashCode() {
        return this.name.hashCode()
    }
}

/** An array is a mutable fixed-size container holding any objects.
 * The elements are numbered from 0 to size-1.
 * An array may be void.  It is not legal to inherit from ArrayAny.
 */
class ArrayAny {

    constructor(length) {
        this.length = length;
        this.array_field = new Array(this.length);
    }

    /** Return length of array. */
    length() {
        return this.length;
    }

    /** Return a new array of size s (the original array is unchanged).
     * Any values in the original array that fit within the new array
     * are copied over.  If the new array is larger than the original array,
     * the additional entries start void.  If the new array is smaller
     * than the original array, entries past the end of the new array are
     * not copied over.
     */
    resize(s) {
        const result = new Array(s);
        for (let i = 0; i < s && i < this.length; i++) {
            result.push(this.array_field[i])
        }
        return result;
    }

    /* Returns the entry at location index.
     * precondition: 0 <= index < length()
     */
    get(index) {
        return this.array_field[index];
    }

    /* change the entry at location index.
     * return the old value, if any (or null).
     * precondition: 0 <= index < length()
     */
    set(index, obj) {
        return this.array_field[index] = obj;
    }
}