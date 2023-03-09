using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Popper.Blazor;

public class PopperInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly DotNetObjectReference<PopperInterop>? _objRef;

    public delegate void Closed();
    public event Closed OnClosed;
    
    public PopperInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Popper.Blazor/index.js").AsTask());
        _objRef = DotNetObjectReference.Create(this);
    }

    public async Task Create(ElementReference anchorRef, ElementReference contentRef, Placement placement, bool autoClose)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("createPopper", anchorRef, contentRef, CreateOptions(placement), autoClose, _objRef);
    }

    private object CreateOptions(Placement placement)
    {
        return new { placement = placement.ToPopperString() };
    }

    [JSInvokable]
    public void HidePopper()
    {
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