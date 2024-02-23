using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private int _sceneIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(_sceneIndex);
            //collision.GetComponent<Health>().StopAllCoroutines();
            collision.GetComponent<Health>().ResetState();
            Physics2D.IgnoreLayerCollision(8, 9, false);
        }
    }


}
