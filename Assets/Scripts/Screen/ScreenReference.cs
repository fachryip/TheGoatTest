using System;
using UnityEngine;

[Serializable]
public class ScreenReference
{
    [SerializeField] private MonoBehaviour ScreenObject;

    public IScreen Screen => ScreenObject as IScreen;
}
