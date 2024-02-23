using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip _checkpointSFX;
    private Transform _currCheckpointTf;
    private Health _playerHealth;
    private UIManager _UIManager;

    private void Awake()
    {
        _playerHealth = GetComponent<Health>();
        _UIManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        if(_currCheckpointTf == null)
        {
            _UIManager.GameOver();
            return;
        }

        transform.position = _currCheckpointTf.position;
        _playerHealth.Respawn();

        Camera.main.GetComponent<CameraController>().MoveToNewRooom(_currCheckpointTf.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Checkpoint"))
        {
            _currCheckpointTf = collision.transform;
            AudioManager.Instance.PlaySound(_checkpointSFX);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("Active");
        }
    }
}
