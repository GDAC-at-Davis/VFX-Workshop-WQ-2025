using Base.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private GameObject _model;

    [SerializeField]
    private LayerMask _collisionMask;

    public UnityEvent OnCollision;

    public UnityEvent OnSpawn;

    private void OnTriggerEnter(Collider other)
    {
        int layer = 1 << other.gameObject.layer;
        if ((_collisionMask.value & layer) == layer)
        {
            var targetDummy = other.gameObject.GetComponentInParent<TargetDummy>();
            if (targetDummy != null)
            {
                targetDummy.DestroyDummy();
            }

            OnCollision.Invoke();

            _model.SetActive(false);
            _rigidbody.isKinematic = true;

            Destroy(gameObject, 2f);
        }
    }

    public void Launch(Vector3 direction, float speed, float lifetime)
    {
        _rigidbody.velocity = direction.normalized * speed;
        transform.forward = direction;
        OnSpawn.Invoke();
        Destroy(gameObject, lifetime);
    }
}