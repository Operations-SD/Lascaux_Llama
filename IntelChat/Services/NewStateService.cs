using System;
using System.Threading.Tasks;

public class NewStateService
{
    
    public event Action? OnShowNewMemo;

    public void ShowNewMemo()
    {
        OnShowNewMemo?.Invoke();
    }
}
