using Gdac3PWorkshopPack.Annotating.Scripts;
using UnityEngine;

public class ShootScript : MonoBehaviourDevNote
{
    [Header("Dependencies")]

    [SerializeField]
    private Projectile _projectilePrefab;

    [SerializeField]
    private Transform _aimTransform;

    [Header("Stats")]

    [SerializeField]
    private float _projectileSpeed;

    [SerializeField]
    private float _projectileLifetime;

    private void Update()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(mouseRay, out RaycastHit hit))
        {
            Vector3 diff = hit.point - _aimTransform.position;
            diff.y = 0;
            _aimTransform.rotation = Quaternion.LookRotation(diff, Vector3.up);
        }

        if (Input.GetMouseButtonDown(0))
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
        projectile.Launch(dir.normalized, _projectileSpeed, _projectileLifetime);
    }
}