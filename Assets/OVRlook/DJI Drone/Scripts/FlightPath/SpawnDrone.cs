using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SpawnDrone : MonoBehaviour
{
    private List<GameObject> drones = new List<GameObject>();
    private List<Vector3> dronePositions = new List<Vector3>();
    private Texture2D cameraView;

    public LineRenderer lineRenderer;
    public GameObject drone;
    public Transform position;

    UI_Manager UI;

    public void Start()
    {
        UI = GetComponent<UI_Manager>();

        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        Material blackDiffuseMat = new Material(Shader.Find("Unlit/Color"));
        blackDiffuseMat.color = Color.black;
        lineRenderer.material = blackDiffuseMat;
        lineRenderer.useWorldSpace = true;
    }
    void Update()
    {
        if (this.GetComponent<Transform>().hasChanged)
        {
            drawLine();
            transform.hasChanged = false;
        }
        foreach (var item in drones)
        {

            if (item.transform.hasChanged)
            {
                drawLine();
                UI.UpdateList();
                transform.hasChanged = false;
            }
        }

    }

    private void OnDestroy()
    {
        Destroy(lineRenderer.material);
    }

    public void spawn()
    {
        GameObject spawnedDrone = (GameObject)Instantiate(drone, position.position, position.rotation);
        drones.Add(spawnedDrone);
        dronePositions.Add(spawnedDrone.GetComponent<Renderer>().bounds.center);
        UI.UpdateList();
        if (drones.Count > 1)
        {
            drawLine();
        }
    }
    public void delete(int index)
    {
        Destroy(drones[index]);
        drones.RemoveAt(index);
        dronePositions.RemoveAt(index);
        UI.UpdateList();
        drawLine();
    }


    public void drawLine()
    {
        lineRenderer.positionCount = drones.Count + 1;

        for (int i = 0; i < drones.Count; i++)
        {
            Renderer rend = drones[i].GetComponent<Renderer>();
            dronePositions[i] = drones[i].GetComponent<Renderer>().bounds.center;
            lineRenderer.SetPosition(i, rend.bounds.center);
        }

        lineRenderer.SetPosition(lineRenderer.positionCount - 1, this.GetComponent<Renderer>().bounds.center);
    }

    public List<GameObject> getDroneList() 
    {
        return drones;
    }
    public List<Vector3> getDronePositionList()
    {
        return dronePositions;
    }
}
