using System.Runtime.InteropServices;
using UnityEngine;

public class SwiftInterop : MonoBehaviour
{
    [DllImport("TestSwiftLib")]
    private static extern int addNumbers(int a, int b);

    private void Start()
    {
        int result = addNumbers(5, 10);
        Debug.Log("Swift DLL result: " + result);
    }
}
