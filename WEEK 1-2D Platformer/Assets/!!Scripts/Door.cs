using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _prevRoom;
    [SerializeField] private Transform _nextRoom;
    [SerializeField] private CameraController _camController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(collision.transform.position.x < transform.position.x)
            {
                _camController.MoveToNewRooom(_nextRoom);
                _nextRoom.GetComponent<Room>().ActivateRoom(true);
                _prevRoom.GetComponent<Room>().ActivateRoom(false);

            }else
            {
                _camController.MoveToNewRooom(_prevRoom);
                _prevRoom.GetComponent<Room>().ActivateRoom(true);
                _nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }
}
