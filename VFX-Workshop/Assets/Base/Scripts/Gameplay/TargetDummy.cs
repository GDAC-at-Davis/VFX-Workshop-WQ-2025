using System;
using UnityEngine;

namespace Base.Scripts
{
    public class TargetDummy : MonoBehaviour
    {
        public event Action<TargetDummy> OnDestroyed;

        public void DestroyDummy()
        {
            Destroy(gameObject);
            OnDestroyed?.Invoke(this);
        }
    }
}