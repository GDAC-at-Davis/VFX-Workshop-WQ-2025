using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class ProtagEntity : MonoBehaviour
{
    [SerializeField]
    private PlayerController _controller;

    [SerializeField]
    private GameObject _protagModel;


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

    public void Launch(float jumpHeight)
    {
        _controller.Launch(jumpHeight);
    }
}
