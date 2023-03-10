using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Popper.Blazor;

public class PopperInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly DotNetObjectReference<PopperInterop>? _objRef;

    public delegate Task Closed();
    public event Closed OnClosed;
    
    public PopperInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Popper.Blazor/index.js").AsTask());
        _objRef = DotNetObjectReference.Create(this);
    }

    public async Task Create(ElementReference? anchorRef, ElementReference contentRef, Placement placement, Modifier[]? modifiers, bool autoClose)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("createPopper", anchorRef, contentRef, CreateOptions(placement, modifiers), autoClose, _objRef);
    }

    private object CreateOptions(Placement placement, Modifier[]? modifiers)
    {
        return new
        {
            placement = placement.ToPopperString(),
            modifiers = modifiers ?? Enumerable.Empty<Modifier>()
        };
    }

    [JSInvokable]
    public void HidePopper()
    {
        Console.WriteLine("HidePopper hit");
        OnClosed?.Invoke();
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}