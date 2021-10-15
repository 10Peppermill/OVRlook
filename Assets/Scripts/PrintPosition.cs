using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintPosition : MonoBehaviour
{
    private Vector3 objectPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objectPosition = GetComponent<Transform>().transform.position;
        Debug.Log(objectPosition);
    }
}
