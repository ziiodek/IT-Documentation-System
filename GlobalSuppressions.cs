// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

// Since we're deploying to Windows Server, we can use Windows-only calls.
[assembly: SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Windows Server project", Scope = "module")]

