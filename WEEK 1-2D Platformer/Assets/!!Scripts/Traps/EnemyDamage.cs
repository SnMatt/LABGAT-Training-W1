using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("eeeee");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("ssss");
            collision.GetComponent<Health>().TakeDamage(1);
        }
    }
}
