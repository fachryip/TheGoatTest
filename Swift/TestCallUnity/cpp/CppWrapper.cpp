#include <iostream>
typedef void (*UnityCallback)(const char*);

UnityCallback unityCallback = nullptr;

extern "C" __declspec(dllexport) void SetUnityCallback(UnityCallback callback)
{
    unityCallback = callback;
}

extern "C" __declspec(dllexport) void CallUnityFromSwift(const char* message)
{
    if(unityCallback)
    {
        unityCallback(message);
    }
}