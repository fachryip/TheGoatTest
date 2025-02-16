#if os(Windows)
import Foundation
import Dispatch
#endif

public class RandomIntervalRunner {
    #if os(Windows)
    private var timer: DispatchSourceTimer?
    private let queue = DispatchQueue.global(qos: .background)
    
    func start(action: @escaping () -> Void) {
        scheduleNextRun(action: action)
    }

    func stop() {
        timer?.cancel()
        timer = nil
    }

    private func scheduleNextRun(action: @escaping () -> Void) {
        let randomInterval = DispatchTimeInterval.seconds(Int.random(in: 1...5))
        
        let newTimer = DispatchSource.makeTimerSource(queue: queue)
        newTimer.schedule(deadline: .now() + randomInterval)
        newTimer.setEventHandler { [weak self] in
            action()
            self?.scheduleNextRun(action: action) // Schedule the next run
        }
        newTimer.resume()
        
        timer = newTimer
    }

    #else
    private var timer: Timer?
    
    func start(action: @escaping () -> Void) {
        scheduleNextRun(action: action)
    }
    
    func stop() {
        timer?.invalidate()
        timer = nil
    }
    
    private func scheduleNextRun(action: @escaping () -> Void) {
        let randomInterval = TimeInterval(Int.random(in: 1...5))
        timer = Timer.scheduledTimer(withTimeInterval: randomInterval, repeats: false) { [weak self] _ in
            action()
            self?.scheduleNextRun(action: action) // Schedule the next run
        }
    }
    #endif
}