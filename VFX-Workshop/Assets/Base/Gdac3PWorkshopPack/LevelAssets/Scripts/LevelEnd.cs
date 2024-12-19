using Gdac3PWorkshopPack.Protag.Scripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gdac3PWorkshopPack.LevelAssets.Scripts
{
    /// <summary>
    ///     Simple level end trigger that loads the next level when the protag enters it.
    /// </summary>
    public class LevelEnd : MonoBehaviour
    {
        [SerializeField]
        private string _nextLevel;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var style = new GUIStyle();
            style.normal.textColor = new Color(0.0f, 0.8f, 0.2f);
            Handles.Label(transform.position, "Level Goal", style);

            var boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Gizmos.color = new Color(0.0f, 1f, 0.2f);
                Gizmos.DrawWireCube(transform.position + boxCollider.center, boxCollider.size);
            }
        }
#endif

        private void OnTriggerEnter(Collider other)
        {
            var entity = other.gameObject.GetComponent<ProtagEntity>();
            if (entity != null)
            {
                SceneManager.LoadScene(_nextLevel);
            }
        }
    }
}