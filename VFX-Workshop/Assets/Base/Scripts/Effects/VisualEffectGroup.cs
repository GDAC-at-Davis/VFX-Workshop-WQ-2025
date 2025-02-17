using System;
using System.Collections.Generic;
using Gdac3PWorkshopPack.Annotating.Scripts;
using UnityEngine;

namespace Base.Scripts
{
    public class VisualEffectGroup : MonoBehaviourDevNote
    {
        public static bool Visible;
        public static Action<bool> OnVisibleChanged;

        private List<ParticleSystem> _particleSystems;
        private List<TrailRenderer> _trailRenderers;
        private List<IVisualEffect> _visualEffects;

        private bool _useEffects = true;

        private void Awake()
        {
            OnVisibleChanged += OnPlayParticlesChangedHandler;

            _particleSystems = new List<ParticleSystem>();
            _trailRenderers = new List<TrailRenderer>();
            _visualEffects = new List<IVisualEffect>();

            _particleSystems.AddRange(GetComponentsInChildren<ParticleSystem>(true));
            _trailRenderers.AddRange(GetComponentsInChildren<TrailRenderer>(true));
            _visualEffects.AddRange(GetComponentsInChildren<IVisualEffect>(true));

            OnPlayParticlesChangedHandler(Visible);
        }

        private void OnDestroy()
        {
            OnVisibleChanged -= OnPlayParticlesChangedHandler;
        }

        private void OnPlayParticlesChangedHandler(bool particlesEnabled)
        {
            _useEffects = particlesEnabled;
            if (!particlesEnabled)
            {
                foreach (ParticleSystem particle in _particleSystems)
                {
                    if (particle == null)
                    {
                        continue;
                    }

                    ParticleSystem.MainModule main = particle.main;
                    main.maxParticles = 0;
                }

                foreach (TrailRenderer trail in _trailRenderers)
                {
                    if (trail == null)
                    {
                        continue;
                    }

                    trail.emitting = false;
                }

                foreach (IVisualEffect effect in _visualEffects)
                {
                    effect.Hide();
                }
            }
            else
            {
                foreach (ParticleSystem particle in _particleSystems)
                {
                    if (particle == null)
                    {
                        continue;
                    }

                    ParticleSystem.MainModule main = particle.main;
                    main.maxParticles = 10000;
                }

                foreach (TrailRenderer trail in _trailRenderers)
                {
                    if (trail == null)
                    {
                        continue;
                    }

                    trail.emitting = true;
                }

                foreach (IVisualEffect effect in _visualEffects)
                {
                    effect.Show();
                }
            }
        }

        public void Play()
        {
            if (!_useEffects)
            {
                return;
            }

            foreach (ParticleSystem particle in _particleSystems)
            {
                if (particle == null)
                {
                    continue;
                }

                particle.Play();
            }

            foreach (TrailRenderer trail in _trailRenderers)
            {
                if (trail == null)
                {
                    continue;
                }

                trail.emitting = true;
            }

            foreach (IVisualEffect effect in _visualEffects)
            {
                effect.Play();
            }
        }

        public void Stop()
        {
            foreach (ParticleSystem particle in _particleSystems)
            {
                if (particle == null)
                {
                    continue;
                }

                particle.Stop();
            }

            foreach (TrailRenderer trail in _trailRenderers)
            {
                if (trail == null)
                {
                    continue;
                }

                trail.emitting = false;
            }

            foreach (IVisualEffect effect in _visualEffects)
            {
                effect.Stop();
            }
        }
    }
}