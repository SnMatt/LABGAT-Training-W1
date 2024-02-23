using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _currPosX;
    private Vector3 _velocity = Vector3.zero;

    private void Update()
    {
        if(transform.position.x != _currPosX)
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_currPosX, transform.position.y, transform.position.z), ref _velocity, _speed);
    }

    public void MoveToNewRooom(Transform room)
    {
        _currPosX = room.position.x;
    }
}
