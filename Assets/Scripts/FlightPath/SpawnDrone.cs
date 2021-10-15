using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnDrone : MonoBehaviour
{
    private List<GameObject> drones = new List<GameObject>();
    private List<Vector3> dronePositions = new List<Vector3>();

    public LineRenderer lineRenderer;
    public GameObject drone;
    public Transform position;

    public void Start()
    {
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        if (this.GetComponent<Transform>().hasChanged)
        {
            transform.hasChanged = false;
            drawLine();
        }
        foreach (var item in drones)
        {

            if (item.transform.hasChanged)
            {
                drawLine();
                transform.hasChanged = false;
            }
        }

    }

    public void spawn()
    {
        GameObject spawnedDrone = (GameObject)Instantiate(drone, position.position, position.rotation);
        drones.Add(spawnedDrone);
        dronePositions.Add(spawnedDrone.GetComponent<Renderer>().bounds.center);
        if (drones.Count > 1)
        {
            drawLine();
        }
    }

    public void drawLine()
    {
        lineRenderer.positionCount = drones.Count + 1;

        for (int i = 0; i < drones.Count; i++)
        {
            Renderer rend = drones[i].GetComponent<Renderer>();
            lineRenderer.SetPosition(i, rend.bounds.center);
        }

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, this.GetComponent<Renderer>().bounds.center);
    }

}
