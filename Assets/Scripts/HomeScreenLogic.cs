using System.Collections.Generic;
using UnityEngine;

public class HomeScreenLogic : MonoBehaviour
{
    private const string SparkAction = "spark";

    [SerializeField] private HomeParticleLogic ParticleLogic;
    [SerializeField] private HomeObjectLogic ObjectLogic;

    private Queue<string> _pluginMessages = new Queue<string>();

    private void OnEnable()
    {
        GameLifetimeEvent.OnReady += OnHomeScreenReady;
        GameLifetimeEvent.OnMessageReceive += OnMessageReceived;
    }

    private void OnDisable()
    {
        GameLifetimeEvent.OnReady -= OnHomeScreenReady;
        GameLifetimeEvent.OnMessageReceive -= OnMessageReceived;
    }

    private void Update()
    {
        while (_pluginMessages.Count > 0)
        {
            DispatchMessage(_pluginMessages.Dequeue());
        }
    }

    private void OnHomeScreenReady()
    {
        UnityInterop.SetupEmitter();
        ObjectLogic.SetReady();
    }

    private void OnMessageReceived(string message)
    {
        _pluginMessages.Enqueue(message);
    }

    private void DispatchMessage(string message)
    {
        if (message == SparkAction)
        {
            ParticleLogic.Play();
        }
    }
}
