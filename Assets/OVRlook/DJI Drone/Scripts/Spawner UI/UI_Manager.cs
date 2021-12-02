using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class UI_Manager : MonoBehaviour
{

    SpawnDrone SD;
    private List<SpawnDrone.DroneData> drones = new List<SpawnDrone.DroneData>();
    private List<Vector3> dronePositions = new List<Vector3>();
    private bool cameraOn = false;
    private Color grey;

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
            buttons[0].onClick.AddListener(delegate { startStopCamera(entries.IndexOf(entry)); }); //button 0 is the camera button
            buttons[1].onClick.AddListener(delegate { removeEntry(entries.IndexOf(entry)); }); //button 1 is the delete button
        }
        FinalizePath.GetComponent<Button>().onClick.AddListener(delegate { finalize(); });
        grey = entries[0].GetComponent<Image>().color;
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
        foreach (var droneData in drones)
        {
            Vector3 position = droneData.drone.GetComponent<Renderer>().bounds.center;
            Instantiate(marker, position, Quaternion.identity);
            Vector2 latlong = new Vector2(0, 0);
            latlong = GPSEncoder.USCToGPS(position);
            string altitude = position.y.ToString("R");
            string heading = droneData.drone.transform.rotation.eulerAngles.y.ToString("R");
            string curvesize = 0f.ToString("R");
            string rotationdir = 0f.ToString("R");
            string gimbalmode = 0f.ToString("R");
            string gimbalpitchangle = 0f.ToString("R");
            string actiontype1 = droneData.camState.ToString();

            writeCSV(latlong, altitude, heading, curvesize, rotationdir, gimbalmode, gimbalpitchangle, actiontype1);

        }
        Debug.Log("Jobs Done!");
    }

    private void writeCSV(Vector2 latlon, string altitude, string heading, string curvesize, string rotationdir, string gimbalmode, string gimbalpitchangle, string actiontype1)
    {

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"FlightPath.csv", true))
            {
                file.WriteLine(latlon.x.ToString("R") + "," + 
                               latlon.y.ToString("R") + "," + 
                               altitude + "," + 
                               heading + "," + 
                               curvesize + "," + 
                               rotationdir + "," + 
                               gimbalmode + "," + 
                               gimbalpitchangle + "," + 
                               actiontype1);
            }
    }

    private void startStopCamera(int index)
    {
        Color currentColor = entries[index].GetComponent<Image>().color;
        Color red = new Color(1, 0, 0, 1), green = new Color(0, 1, 0, 1);

        if (currentColor == red || currentColor == green)
        {
            cameraOn = !cameraOn;
            drones[index].setCamState(-1);
            entries[index].GetComponent<Image>().color = grey;
        }
        else if(!cameraOn)
        {
            cameraOn = !cameraOn;
            drones[index].setCamState(2);
            entries[index].GetComponent<Image>().color = green;
        } 
        else
        {
            cameraOn = !cameraOn;
            drones[index].setCamState(3);
            entries[index].GetComponent<Image>().color = red;
        }
    }
}