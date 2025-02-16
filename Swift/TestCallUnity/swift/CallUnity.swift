import WinSDK

@_cdecl("CallUnityMessage")
public func CallUnityMessage()
{
    let message = "Hello this message from Swift!"

    let lib = LoadLibraryA("CppWrapper.dll")
    let sendMessage = unsafeBitCast(GetProcAddress(lib, "CallUnityFromSwift"), to: (@convention(c) (UnsafePointer<CChar>) -> Void).self)

    sendMessage(message)
    FreeLibrary(lib)
}