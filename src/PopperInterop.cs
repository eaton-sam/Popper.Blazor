using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Popper.Blazor;

internal class PopperInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;
    private readonly DotNetObjectReference<PopperInterop>? _objRef;

    public delegate Task Closed();
    public event Closed? OnClosed;
    
    public PopperInterop(IJSRuntime jsRuntime)
    {
        _moduleTask = new (() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Popper.Blazor/index.js").AsTask());
        _objRef = DotNetObjectReference.Create(this);
    }

    public async Task Create(Guid id, ElementReference anchorRef, ElementReference contentRef, Placement placement, Modifier[] modifiers, bool autoClose)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("createPopper", id, anchorRef, contentRef, CreateOptions(placement, modifiers), autoClose, _objRef);
    }

    public async Task Update(Guid id, ElementReference contentRef, Placement placement, Modifier[] modifiers, bool autoClose)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("updatePopper", id, contentRef, CreateOptions(placement, modifiers), autoClose, _objRef);
    }

    public async Task Destroy(Guid id)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("destroyPopper", id);
    }
    
    private object CreateOptions(Placement placement, Modifier[] modifiers)
    {
        return new
        {
            placement = placement.ToPopperString(),
            modifiers
        };
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