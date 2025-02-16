using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnityInterop : MonoBehaviour
{
    private const string CppWrapper = "CppWrapper";
    private const string NativeUI = "NativeUI";
    private const string NativeSwift = "NativeSwift";

    private static Queue<string> _pluginMessages = new Queue<string>();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void LogCallback(string message);

    [AOT.MonoPInvokeCallback(typeof(LogCallback))]
    public static void ReceiveMessage(string message)
    {
        Debug.Log("Message from DLL: " + message);
        _pluginMessages.Enqueue(message);
    }

    [DllImport("kernel32.dll")]
    private static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr LoadLibrary(string lpFileName);

    private List<IntPtr> _handlers = new List<IntPtr>();

    private void Start()
    {
        _handlers.Add(LoadLibrary(CppWrapper + ".dll"));
        _handlers.Add(LoadLibrary(NativeUI + ".dll"));
        _handlers.Add(LoadLibrary(NativeSwift + ".dll"));

        SetUnityCallback(ReceiveMessage);
    }

    private void Update()
    {
        while (_pluginMessages.Count > 0)
        {
            DispatchMessage(_pluginMessages.Dequeue());
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");

        StopEmitter();
        foreach (var handler in _handlers)
        {
            FreeLibrary(handler);
        }
    }

    private void DispatchMessage(string message)
    {
        GameLifetimeEvent.OnMessageReceive?.Invoke(message);
    }

    [DllImport(CppWrapper)]
    public static extern void SetUnityCallback(LogCallback callback);

    [DllImport(NativeUI)]
    public static extern void ShowNativePage();

    [DllImport(NativeSwift)]
    public static extern void ObjectRotated(float x, float y, float z);

    [DllImport(NativeSwift)]
    public static extern void SetupEmitter();

    [DllImport(NativeSwift)]
    public static extern void StopEmitter();
}
