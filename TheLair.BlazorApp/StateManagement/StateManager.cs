using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;

namespace TheLair.BlazorApp.StateManagement;

public abstract class StateManager<TState> : IStateManager
    where TState : class
{
    private readonly ISyncLocalStorageService LocalStorage;
    //private readonly List<Action<TState>> SessionWrites = new();

    protected StateManager(ISyncLocalStorageService localStorage)
    {
        LocalStorage = localStorage;
    }

    protected abstract TState BuildState();

    public TState GetState()
    {
        if (!LocalStorage.ContainKey("ApplicationState"))
            LocalStorage.SetItem("ApplicationState", BuildState());

        try
        {
            TState state = LocalStorage.GetItem<TState>("ApplicationState") ?? BuildState();

            return state;
        }
        catch
        {
            // ignored
        }

        return (BuildState());
    }

    public TState WriteState(Action<TState> fct)
    {
        TState state = GetState();

        fct(state);
        LocalStorage.SetItem("ApplicationState", state);
        return state;
    }

    /*public void WriteSessionState(Action<TState> fct)
    {
        SessionWrites.Add(fct);
    }*/
}