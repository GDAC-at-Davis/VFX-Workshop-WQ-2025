using UnityEngine;
using UnityEngine.UI;

namespace Base.Scripts
{
    /// <summary>
    ///     Globally toggles particle groups on and off, for demonstration purposes.
    /// </summary>
    public class ParticleToggle : MonoBehaviour
    {
        [SerializeField]
        private Toggle _toggle;

        private void Awake()
        {
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
            OnToggleValueChanged(_toggle.isOn);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool toggle)
        {
            VisualEffectGroup.Visible = toggle;
            VisualEffectGroup.OnVisibleChanged?.Invoke(toggle);
        }
    }
}