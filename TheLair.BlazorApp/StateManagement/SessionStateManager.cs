using Blazored.LocalStorage;

namespace TheLair.BlazorApp.StateManagement;

public abstract class SessionStateManager<TState, TSessionState> : StateManager<TState>, ISessionStateManager
    where TState : class
{
    public TSessionState SessionState;

    protected SessionStateManager(ISyncLocalStorageService localStorage) : base(localStorage)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        SessionState = SessionStateBuilder();
    }

    protected abstract TSessionState SessionStateBuilder();
}