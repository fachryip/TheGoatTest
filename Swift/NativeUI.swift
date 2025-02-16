#if os(Windows)
import WinSDK

var hWnd: HWND?
var threadHandle: HANDLE?

@_cdecl("ShowNativePage")
public func showNativePage(){
    threadHandle = CreateThread(nil, 0, { _ in 
        createNativeWindow()
        return 0
    }, nil, 0, nil)
}

public func createNativeWindow() {
    let hInstance = GetModuleHandleW(nil)

    var wc = WNDCLASSW()
    wc.lpfnWndProc = { (hWnd, msg, wParam, lParam) -> LRESULT in
        switch msg {
            case UINT(WM_CLOSE):
                DestroyWindow(hWnd)
                return 0
            case UINT(WM_DESTROY):
                PostQuitMessage(0)
                return 0
            case UINT(WM_PAINT):
                var ps = PAINTSTRUCT()
                let hdc = BeginPaint(hWnd, &ps)

                let blueBrush = CreateSolidBrush(COLORREF(255 << 16))
                var rect = RECT(left: 0, top: 0, right: 500, bottom: 300)
                FillRect(hdc, &rect, blueBrush)

                EndPaint(hWnd, &ps)
                return 0
            default:
                return DefWindowProcW(hWnd, msg, wParam, lParam)
        }
    }
    wc.hInstance = hInstance

    let className: [WCHAR] = "SwiftWinUIWindow".utf16.map { WCHAR($0) } + [0]
    wc.lpszClassName = className.withUnsafeBufferPointer { $0.baseAddress }

    RegisterClassW(&wc)

    let windowTitle: [WCHAR] = "Swift WinUI Window".utf16.map { WCHAR($0) } + [0]

    hWnd = CreateWindowExW(
        0, wc.lpszClassName, windowTitle.withUnsafeBufferPointer { $0.baseAddress },
        DWORD(WS_OVERLAPPEDWINDOW), 100, 100, 500, 300,
        nil, nil, hInstance, nil
    )

    ShowWindow(hWnd, SW_SHOW)
    UpdateWindow(hWnd)

    var msg = MSG()
    while GetMessageW(&msg, nil, 0, 0) {
        TranslateMessage(&msg)
        DispatchMessageW(&msg)
    }
}

#else
@_cdecl("ShowNativePage")
func showNativePage() {
	let vc = UIViewController()
	vc.view.backgroundColor = .blue
	self.present(vc, animated: true)
}
#endif