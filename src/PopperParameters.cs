namespace Popper.Blazor;

internal class PopperParameters : IEquatable<PopperParameters>
{
    public bool AutoClose { get; set; }
    public (int x, int y)? Offset { get; set; }
    public Placement Placement { get; set; }
    public Modifier[] Modifiers { get; set; } = { };


    public bool Equals(PopperParameters? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        
        Console.WriteLine($"{Placement} - {other.Placement}");
        
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