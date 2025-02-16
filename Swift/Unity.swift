import WinSDK

public func CallUnityMessage(message: String)
{
    let lib = LoadLibraryA("CppWrapper.dll")
    let sendMessage = unsafeBitCast(GetProcAddress(lib, "CallUnityFromSwift"), to: (@convention(c) (UnsafePointer<CChar>) -> Void).self)

    sendMessage(message)
    FreeLibrary(lib)
}