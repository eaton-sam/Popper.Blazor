namespace Popper.Blazor;

public abstract class Modifier
{
    public abstract string Name { get; }
    public abstract Dictionary<string, object> Options { get; }
    
    // public static Modifier[] Collect(params Modifier[] modifiers) => modifiers;
}

public sealed class OffsetModifier : Modifier, IEquatable<OffsetModifier>
{
    private readonly int _x;
    private readonly int _y;

    public OffsetModifier(int x, int y)
    {
        _x = x;
        _y = y;
    }
    
    public override string Name => "offset";
    public override Dictionary<string, object> Options => new()
    {
        { "offset", new[] { _x, _y } }
    };

    public bool Equals(OffsetModifier? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _x == other._x && _y == other._y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((OffsetModifier)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_x, _y);
    }
}