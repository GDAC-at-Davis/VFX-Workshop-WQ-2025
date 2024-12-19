using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gdac3PWorkshopPack.Protag.Scripts
{
    /// <summary>
    ///  Simple monobehavior to act as "target" for interactions with protag.
    ///  E.g kill zone, checkpoint, etc.
    /// </summary>
    public class ProtagEntity : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _controller;

        [SerializeField]
        private GameObject _protagModel;
        
        /// <summary>
        /// Simple respawn that just reloads the scene after a delay.
        /// </summary>
        [ContextMenu("Respawn")]
        public void Respawn()
        {
            // reload scene
            _controller.enabled = false;
            _protagModel.SetActive(false);
            StartCoroutine(Restart());
        }

        private IEnumerator Restart()
        {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
