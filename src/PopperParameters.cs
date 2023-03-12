namespace Popper.Blazor;

internal class PopperParameters : IEquatable<PopperParameters>
{
    public bool AutoClose { get; init; }
    public (int x, int y)? Offset { get; init; }
    public Placement Placement { get; init; }
    public Modifier[] Modifiers { get; init; } = Array.Empty<Modifier>();

    public bool Equals(PopperParameters? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        return AutoClose == other.AutoClose && Nullable.Equals(Offset, other.Offset) && Placement == other.Placement && Modifiers.SequenceEqual(other.Modifiers);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((PopperParameters)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(AutoClose, Offset, Modifiers, (int)Placement);
    }
}