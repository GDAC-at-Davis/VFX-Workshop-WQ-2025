using Cinemachine;
using Gdac3PWorkshopPack.Annotating.Scripts;
using UnityEngine;

namespace Base.Scripts.Effects
{
    public class ScreenShakeEffect : MonoBehaviourDevNote, IVisualEffect
    {
        [SerializeField]
        private CinemachineImpulseDefinition _screenShakeEffect;

        [SerializeField]
        private Vector3 _shakeVelocity;

        [SerializeField]
        private float _force;

        [SerializeField]
        private bool _continuous;

        private bool _isPlayingContinuously;

        private bool _isHidden;

        private float _continuousTimer;

        private void Update()
        {
            if (_isPlayingContinuously)
            {
                _continuousTimer += Time.deltaTime;
                if (_continuousTimer >= _screenShakeEffect.m_ImpulseDuration)
                {
                    Play();
                    _continuousTimer = 0;
                }
            }
        }

        public void Play()
        {
            if (_isHidden)
            {
                return;
            }

            _isPlayingContinuously = _continuous;
            _screenShakeEffect.CreateEvent(transform.position, _shakeVelocity * _force);
        }

        public void Stop()
        {
            _isPlayingContinuously = false;
        }

        public void Hide()
        {
            _isHidden = true;
        }

        public void Show()
        {
            _isHidden = false;
        }
    }
}