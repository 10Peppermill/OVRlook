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
    public Image cameraViewPanel;
    public Button FinalizePath;


    // Start is called before the first frame update
    void Start()
    {
        SD = GetComponent<SpawnDrone>();
        foreach (var entry in entries)
        {
            Button[] buttons = entry.GetComponentsInChildren<Button>();
            buttons[0].onClick.AddListener(delegate { displayCamera(entries.IndexOf(entry)); }); //button 0 is the camera button
            buttons[1].onClick.AddListener(delegate { removeEntry(entries.IndexOf(entry)); }); //button 1 is the delete button
        }
        FinalizePath.GetComponent<Button>().onClick.AddListener(delegate { finalize(); });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateList()
    {
        foreach (var entry in entries)
        {
            entry.SetActive(false);
        }
        drones = SD.getDroneList();
        dronePositions = SD.getDronePositionList();
        for (int i = 0; i < drones.Count; i++)
        {
            addEntry(i);
        }
    }

    public void addEntry(int index)
    {
        entries[index].SetActive(true);
        Text[] text = entries[index].GetComponentsInChildren<Text>();
        text[1].text = dronePositions[index].ToString();


    }
    public void removeEntry(int index)
    {
        SD.delete(index);
    }

    public void finalize() 
    {
        drones = SD.getDroneList();
        foreach (var drone in drones)
        {
            Vector3 position = drone.GetComponent<Renderer>().bounds.center;
            Debug.Log(position);
        }
        Debug.Log("Jobs Done!");
    }

    private void displayCamera(int index)
    {
        CameraView CV = drones[index].GetComponent<CameraView>();
        Texture2D cameraView = CV.getScreenShot();
        cameraViewPanel.GetComponent<Image>().material.SetTexture("texture", cameraView);
        Debug.Log("Display camera for drone " + index + 1);
    }
}