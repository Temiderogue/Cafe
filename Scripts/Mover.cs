using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Rigidbody _body;
    [SerializeField] private bool _isEmpty;
    private Camera _camera;
    private Vector3 screenPoint;

    public bool CanDrag;

    private void Start()
    {
        _camera = Camera.main;
    }

    void OnMouseDown()
    {
        if (CanDrag)
        {
            Data.isItemDragged = true;
            Vector3 _pos = new Vector3(transform.position.x, 2, transform.position.z);
            screenPoint = _camera.WorldToScreenPoint(_pos);
            Cursor.visible = false;
            _body.isKinematic = true;
        }
    }

    void OnMouseDrag()
    {
        if (CanDrag && Data.CanDragEmptyFood)
        {
            screenPoint.z = _camera.WorldToScreenPoint(transform.position).z;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = _camera.ScreenToWorldPoint(curScreenPoint);
            transform.position = new Vector3(curPosition.x, 2, curPosition.z);
        }
    }

    void OnMouseUp()
    {
        /*
        if (_isEmpty)
        {
            _body.isKinematic = true ;
        }
        else if (CanDrag)
        {
            _body.isKinematic = false;
        }
        */
        _body.isKinematic = false;
        Cursor.visible = true;
        Data.isItemDragged = false;
    }
}
