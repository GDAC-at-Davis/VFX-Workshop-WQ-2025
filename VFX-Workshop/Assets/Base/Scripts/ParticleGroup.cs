using System.Collections.Generic;
using Gdac3PWorkshopPack.Annotating.Scripts;
using UnityEngine;

public class ParticleGroup : MonoBehaviourDevNote
{
    [SerializeField]
    private List<ParticleSystem> _particleSystems;

    private void OnValidate()
    {
        _particleSystems = new List<ParticleSystem>();
        _particleSystems.AddRange(GetComponentsInChildren<ParticleSystem>());

        _workshopNote =
            "This script automatically searches for particle systems in children and adds them to the list, " +
            "so don't worry about adding them manually.";
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