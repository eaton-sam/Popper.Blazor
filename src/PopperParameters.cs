namespace Popper.Blazor;

public class PopperParameters
{
    public bool AutoClose { get; set; }
    public (int x, int y)? Offset { get; set; }
    public Modifier[] Modifiers { get; set; } = { };
    public Placement Placement { get; set; }
    
    
}