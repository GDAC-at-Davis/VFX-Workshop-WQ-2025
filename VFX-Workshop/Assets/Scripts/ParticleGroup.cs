using System.Collections.Generic;
using UnityEngine;

public class ParticleGroup : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> _particleSystems;

    private void OnValidate()
    {
        _particleSystems = new List<ParticleSystem>();
        _particleSystems.AddRange(GetComponentsInChildren<ParticleSystem>());
    }

    public void Play()
    {
        foreach (ParticleSystem particleSystem in _particleSystems)
        {
            particleSystem.Play();
        }
    }

    public void Stop()
    {
        foreach (ParticleSystem particleSystem in _particleSystems)
        {
            particleSystem.Stop();
        }
    }
}