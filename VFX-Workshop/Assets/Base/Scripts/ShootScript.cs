using Gdac3PWorkshopPack.Annotating.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootScript : MonoBehaviourDevNote
{
    [Header("Dependencies")]

    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private Transform _aimTransform;

    [SerializeField]
    private Transform _aimCursor;

    [Header("Stats")]

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _projectileLifetime;

    private void Awake()
    {
        foreach (ParticleGroup particle in _aimCursor.GetComponentsInChildren<ParticleGroup>(true))
        {
            particle.Play();
        }
    }

    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            Vector3 diff = hit.point - _aimTransform.position;
            _aimTransform.rotation = Quaternion.LookRotation(diff, Vector3.up);
            _aimCursor.position = hit.point;
            _aimCursor.up = hit.normal;
        }

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Shoot();
        }
    }

    private void OnValidate()
    {
        _workshopNote = "Feel free to modify the shooting stats!";
    }

    private void Shoot()
    {
        Vector3 dir = _aimTransform.forward;

        Projectile projectile = Instantiate(_projectilePrefab, _aimTransform.position, _aimTransform.rotation);
        projectile.Launch(dir * _projectileSpeed, _projectileLifetime);
    }
}