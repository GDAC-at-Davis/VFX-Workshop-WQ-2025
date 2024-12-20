using System;
using System.Collections.Generic;
using Gdac3PWorkshopPack.Annotating.Scripts;
using UnityEngine;

public class ParticleGroup : MonoBehaviourDevNote
{
    public static bool Visible;
    public static Action<bool> OnVisibleChanged;

    [SerializeField]
    private List<ParticleSystem> _particleSystems;

    private void Awake()
    {
        OnVisibleChanged += OnPlayParticlesChangedHandler;

        OnPlayParticlesChangedHandler(Visible);
    }

    private void OnDestroy()
    {
        OnVisibleChanged -= OnPlayParticlesChangedHandler;
    }

    private void OnValidate()
    {
        _particleSystems = new List<ParticleSystem>();
        _particleSystems.AddRange(GetComponentsInChildren<ParticleSystem>(true));
    }

    private void OnPlayParticlesChangedHandler(bool enabled)
    {
        if (!enabled)
        {
            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.maxParticles = 0;
            }
        }
        else
        {
            foreach (ParticleSystem particleSystem in _particleSystems)
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.maxParticles = 10000;
            }
        }
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