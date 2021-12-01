using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UI_Manager : MonoBehaviour
{

    SpawnDrone SD;
    private List<GameObject> drones = new List<GameObject>();
    private List<Vector3> dronePositions = new List<Vector3>();

    public List<GameObject> entries = new List<GameObject>();
    public Image cameraViewPanel;
    public Button FinalizePath;
    public GameObject marker;


    // Start is called before the first frame update
    void Start()
    {
        SD = GetComponent<SpawnDrone>();
        Vector2 latlong = new Vector2(42.336478f, -71.093402f);
        GPSEncoder.SetLocalOrigin(latlong);
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

    //public void finalize() 
    //{
    //    drones = SD.getDroneList();
    //    foreach (var drone in drones)
    //    {
    //        Vector3 position = drone.GetComponent<Renderer>().bounds.center;
    //        Instantiate(marker, position, Quaternion.identity);
    //        Debug.Log(position);
    //    }
    //    Debug.Log("Jobs Done!");
    //}
    public void finalize()
    {
        drones = SD.getDroneList();

            foreach (var drone in drones)
            {
                Vector3 position = drone.GetComponent<Renderer>().bounds.center;
                Instantiate(marker, position, Quaternion.identity);
                Vector2 latlong = new Vector2(0, 0);
                latlong = GPSEncoder.USCToGPS(position);
            writeCSV(latlong, position.y.ToString("R"), drone.transform.rotation.eulerAngles.y.ToString("R"));
                //Debug.Log(latlong.x);
                //Debug.Log(latlong.y);
            }
        Debug.Log("Jobs Done!");
    }

    private void writeCSV(Vector2 latlon, string altitude, string heading)
    {

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"FlightPath.csv", true))
            {
                file.WriteLine(latlon.x.ToString("R") + "," + latlon.y.ToString("R") + "," + altitude + "," + heading);
            }
    }

    private void displayCamera(int index)
    {
        CameraView CV = drones[index].GetComponent<CameraView>();
        Texture2D cameraView = CV.getScreenShot();
        cameraViewPanel.GetComponent<Image>().material.SetTexture("texture", cameraView);
        Debug.Log("Display camera for drone " + index + 1);
    }
}