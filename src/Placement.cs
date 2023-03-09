namespace Popper.Blazor;

public enum Placement
{
    Bottom,
    BottomStart,
    BottomEnd,
    Top,
    TopStart,
    TopEnd,
    Left,
    LeftStart,
    LeftEnd,
    Right,
    RightStart,
    RightEnd
}

public static class PlacementExtensions
{
    public static string ToPopperString(this Placement placement)
    {
        return placement switch
        {
            Placement.Bottom => "bottom",
            Placement.BottomStart => "bottom-start",
            Placement.BottomEnd => "bottom-end",
            Placement.Top => "top",
            Placement.TopStart => "top-start",
            Placement.TopEnd => "top-end",
            Placement.Left => "left",
            Placement.LeftStart => "left-start",
            Placement.LeftEnd => "left-end",
            Placement.Right => "right",
            Placement.RightStart => "right-start",
            Placement.RightEnd => "right-end",
            _ => throw new ArgumentOutOfRangeException(nameof(placement), placement, null)
        };
    }
}