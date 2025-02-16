#if os(Windows)
import WinSDK

@main
struct App {
    static func main() {
        sayHello(name: "Swift Windows") 
    }
}

#endif
