#if os(Windows)
import Foundation
#endif

let runner = RandomIntervalRunner()

@_cdecl("SetupEmitter")
public func setupEmitter() {
    runner.start {
        let red = Float.random(in: 0.0...1.0)
        let green = Float.random(in: 0.0...1.0)
        let blue = Float.random(in: 0.0...1.0)

        print("Action executed at \(Date()). Color: \(red) \(green) \(blue)")
        
        // TODO: call the unity code here to trigger the spark
        CallUnityMessage(message: "spark")
    }
}

@_cdecl("StopEmitter")
public func stopEmitter() {
    runner.stop()
}