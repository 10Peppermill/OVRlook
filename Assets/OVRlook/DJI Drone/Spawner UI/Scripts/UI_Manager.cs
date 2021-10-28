using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    SpawnDrone SD;
    private List<GameObject> drones = new List<GameObject>();
    private List<Vector3> dronePositions = new List<Vector3>();

    public List<GameObject> entries = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        SD = GetComponent<SpawnDrone>();
    }

    // Update is called once per frame
    void Update()
    {
        drones = SD.getDroneList();
        dronePositions = SD.getDronePositionList();
        for (int i = 0; i < drones.Count; i++)
        {
            addEntry(i);
        }
    }

    void addEntry(int index)
    {
        entries[index].SetActive(true);
        Text[] text = entries[index].GetComponentsInChildren<Text>();
        text[1].text = dronePositions[index].ToString();
        

    }
    void removeEntry(int index)
    {
        entries[index].SetActive(false);

    }
}
