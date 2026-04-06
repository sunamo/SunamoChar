### SunamoChar

Methods for advanced working with the char data type including Unicode character detection, classification, and string manipulation.

Part of PlatformIndependentNuGetPackages:

- [nuget.org](https://www.nuget.org/profiles/sunamo)
- [github.org](https://github.com/sunamo/PlatformIndependentNuGetPackages)

Another links:

- [Developer site](https://sunamo.cz)

Request for new features / bug report / etc: [Mail](mailto:radek.jancik@sunamo.cz) or on GitHub

## Key Classes

- **CharHelper** - Main helper class for character operations (splitting, filtering, Unicode classification)
- **GeneralCharService** - Service for handling general character operations and predicates
- **LetterAndDigitCharService** - Service providing letter and digit character lists
- **LetterAndDigitKeyCodeService** - Service providing letter and digit key codes
- **SpecialCharsService** - Service providing special character definitions
- **SpecialKeyCodeServices** - Service providing special character key codes
- **WhitespaceCharService** - Service for handling whitespace characters and their key codes

## Key Methods

- `CharHelper.SplitSpecial()` - Splits text by special delimiters including Unicode generic characters
- `CharHelper.IsSpecialChar()` - Checks if a character is whitespace or punctuation
- `CharHelper.IsUnicodeChar()` - Classifies a character into Unicode character type categories
- `CharHelper.OnlyAccepted()` - Filters text to keep only characters matching a predicate
- `CharHelper.OnlyDigits()` - Extracts only digit characters from text
- `CharHelper.CharWhichIsNotContained()` - Finds a character constant not present in text

## Target Frameworks

**TargetFrameworks:** `net10.0;net9.0;net8.0`

## Installation

```bash
dotnet add package SunamoChar
```

## Dependencies

- **Microsoft.Extensions.Logging.Abstractions**

## License

MIT
