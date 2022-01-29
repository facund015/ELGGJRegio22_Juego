using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : MonoBehaviour
{

    private Vector2 originPosition;
    public bool hasMirror;
    public GameObject mirror;
    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
    }

    public void resetPosition()
    {
        transform.position = originPosition;
    }
}
