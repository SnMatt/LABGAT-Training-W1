using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private AudioClip _pickSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(_pickSFX);
            collision.GetComponent<Health>().AddHealth(1);
            Destroy(gameObject);
        }
    }
}
