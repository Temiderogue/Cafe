using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoom : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed;

    private float ZoomMinBound = 6f;
    private float ZoomMaxBound = 40f;

    private void OnMouseDown()
    {
        Debug.Log("Zoomin");
        _camera.fieldOfView += _speed;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, ZoomMinBound, ZoomMaxBound);
    }
}
