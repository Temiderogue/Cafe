using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            Data.ChangeScore(-10);
            transform.position = Data._menuPosition;
        }
    }
}
