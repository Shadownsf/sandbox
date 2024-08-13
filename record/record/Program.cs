// See https://aka.ms/new-console-template for more information

using System.Runtime.InteropServices.JavaScript;
using System.Text;

Console.WriteLine("Hello, World!");

CustomMononym aristotle = new ("Aristotle");
CustomFullname samuel = new ("Samuel", "Jackson");
CustomFullname samuelL = new(samuel) { FirstName = "Samuel L." };
var (firstName, _) = samuelL;

Mononym thales = new("Thales");
FullName jack = new("Jack", "Bower");
FullName jackB = jack with { FirstName = "Jack B." };
var (firstName1, _) = jackB;

record Mononym(string FirstName);
record FullName(string FirstName, string LastName) : Mononym(FirstName);

class CustomMononym : IEquatable<CustomMononym>
{
    public string FirstName { get; init; }
    public CustomMononym(string firstName) => FirstName = firstName;
    public CustomMononym(CustomMononym other) : this(other.FirstName) {}
    public void Deconstruct(out string firstName) => firstName = FirstName;
    protected virtual Type EqualityContract => typeof(CustomMononym);
    public override bool Equals(object? other) => Equals(other as CustomFullname);

    public bool Equals(CustomMononym? other) =>
        other is not null
        && other.FirstName == FirstName;

    public override int GetHashCode() => HashCode.Combine(FirstName);

    public static bool operator ==(CustomMononym? left, CustomMononym? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(CustomMononym? left, CustomMononym? right) =>
        !(left == right);

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append(nameof(FirstName)).Append(" = ").Append(FirstName);
        return true;
    }

    public override string ToString()
    {
        StringBuilder builder = new StringBuilder().Append($"{GetType().Name} {{ ");
        PrintMembers(builder);
        builder.Append(" }");
        return builder.ToString();
    }
}

public class CustomFullname : CustomMononym, IEquatable<CustomFullname>
{
    public string FirstName { get; init; }
    public string LastName  { get; init; }

    public CustomFullname(string lastName, string firstName) =>
        (FirstName, LastName) = (firstName, lastName);

    public CustomFullname(CustomFullname other) 
        : this(other.FirstName, other.LastName) {}

    public void Deconstruct(out string firstName, out string lastName) =>
        (firstName, lastName) = (FirstName, LastName);

    public override bool Equals(object? other) => Equals(other as CustomFullname);

    public bool Equals(CustomFullname? other) =>
        other is not null
        && other.FirstName == FirstName
        && other.LastName == LastName;

    public override int GetHashCode() => HashCode.Combine(FirstName, LastName);

    public static bool operator ==(CustomFullname? left, CustomFullname? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(CustomFullname? left, CustomFullname? right) =>
        !(left == right);

    protected virtual bool PrintMembers(StringBuilder builder)
    {
        builder.Append(nameof(FirstName)).Append(" = ").Append(FirstName);
        return true;
    }
}