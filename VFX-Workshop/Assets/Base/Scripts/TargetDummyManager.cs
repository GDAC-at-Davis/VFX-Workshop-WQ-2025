using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Base.Scripts
{
    public class TargetDummyManager : MonoBehaviour
    {
        [SerializeField]
        private TargetDummy _targetDummyPrefab;

        [SerializeField]
        private int _dummyCount;

        [SerializeField]
        private float _spawnRadius;

        [SerializeField]
        private TMP_Text _score;

        private readonly List<TargetDummy> _dummies = new();

        private int _scoreValue;

        private void Start()
        {
            for (var i = 0; i < _dummyCount; i++)
            {
                SpawnDummy();
            }
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, _spawnRadius);
#endif
        }

        private void SpawnDummy()
        {
            Vector2 randomPosition = Random.insideUnitCircle * _spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPosition.x, 0, randomPosition.y) + transform.position;

            TargetDummy dummy = Instantiate(_targetDummyPrefab, spawnPosition, Quaternion.identity);
            dummy.OnDestroyed += HandleDummyDestroyed;
            _dummies.Add(dummy);
        }

        private void HandleDummyDestroyed(TargetDummy targetDummy)
        {
            targetDummy.OnDestroyed -= HandleDummyDestroyed;
            _dummies.Remove(targetDummy);

            _scoreValue++;
            _score.text = $"{_scoreValue}";
            SpawnDummy();
        }
    }
}