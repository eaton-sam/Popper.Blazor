namespace Popper.Blazor;

public abstract class Modifier
{
    public abstract string Name { get; }
    public abstract Dictionary<string, object> Options { get; }
}

public sealed class OffsetModifier : Modifier, IEquatable<OffsetModifier>
{
    private readonly int _skid;
    private readonly int _distance;

    public OffsetModifier(int skid, int distance)
    {
        _skid = skid;
        _distance = distance;
    }
    
    public override string Name => "offset";
    public override Dictionary<string, object> Options => new()
    {
        { "offset", new[] { _skid, _distance } }
    };

    public bool Equals(OffsetModifier? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _skid == other._skid && _distance == other._distance;
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
        return HashCode.Combine(_skid, _distance);
    }
}