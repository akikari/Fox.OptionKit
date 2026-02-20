//==================================================================================================
// Contains global code analysis suppressions for the Fox.OptionKit library.
// Suppressions are documented with justifications for each rule.
//==================================================================================================

using System.Diagnostics.CodeAnalysis;

//======================================================================================================
// CA1716: Identifiers should not match keywords
// Justification: "Option" is a widely accepted and idiomatic name in functional programming
// (F#, Rust, Scala, etc.). The name collision with F# keyword is intentional and acceptable
// as this is the standard naming convention for the Option type pattern.
//======================================================================================================
[assembly: SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Option is the standard name for this functional programming pattern", Scope = "type", Target = "~T:Fox.OptionKit.Option`1")]
[assembly: SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "Option is the standard name for this functional programming pattern", Scope = "type", Target = "~T:Fox.OptionKit.Option")]
