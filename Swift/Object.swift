@_cdecl("ObjectRotated")
public func objectRotated(x: Float, y: Float, z: Float) {
    print("Object is rotated: \(x), \(y), \(z)")
    CallUnityMessage(message: "Object is rotated: \(x), \(y), \(z)")
}