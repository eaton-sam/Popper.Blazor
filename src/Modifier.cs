namespace Popper.Blazor;

public abstract class Modifier
{
    public abstract string Name { get; }
    public abstract Dictionary<string, object> Options { get; }
    
    public static Modifier[] Collect(params Modifier[] modifiers) => modifiers;
}

public class OffsetModifier : Modifier
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
        { "offset", new int[] { _x, _y } }
    };
}