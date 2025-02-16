using System;

public static class GameLifetimeEvent
{
    public static Action OnReady;
    public static Action<string> OnMessageReceive;
}
