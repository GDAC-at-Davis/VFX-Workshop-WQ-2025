using UnityEditor;
using UnityEngine;

namespace Gdac3PWorkshopPack.LevelAssets.Scripts
{
    /// <summary>
    ///     Simple spawner that spawns a prefab at its orientation
    /// </summary>
    public class PrefabSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefabToSpawn;

        [SerializeField]
        private bool _spawnOnAwake;

        private void Awake()
        {
            if (_spawnOnAwake)
            {
                SpawnPrefab();
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var style = new GUIStyle();
            style.normal.textColor = new Color(0.0f, 0.2f, 0.8f);
            Handles.Label(transform.position, "Player Spawn Point", style);
        }
#endif

        public void SpawnPrefab()
        {
            GameObject prefab = Instantiate(_prefabToSpawn, transform.position, transform.rotation);
        }
    }
}