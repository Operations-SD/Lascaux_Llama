using System;
using System.Threading.Tasks;

public class NewStateService
{
    public string message { get; set; } = "";
    public event Action? OnShowNewMemo;

    public void ShowNewMemo()
    {
        OnShowNewMemo?.Invoke();
        Console.WriteLine("ShowNewMemo event fired!");
    }
}
