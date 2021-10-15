using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpawnDrone))]

public class midpointDrone : MonoBehaviour
{
    SpawnDrone spawnDroneScript;

    void Start()
    {
        spawnDroneScript = GetComponent<SpawnDrone>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.hasChanged)
        {
            spawnDroneScript.drawLine();
            transform.hasChanged = false;
        }

    }
}
