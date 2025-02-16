using UnityEngine;

public class HomeParticleLogic : MonoBehaviour
{
    [SerializeField] private ParticleSystem Spark;

    public void Play()
    {
        Spark.Play();
    }
}