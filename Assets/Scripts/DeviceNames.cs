using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeviceNames : MonoBehaviour
{
    private InputDevice targetDevice;
    public InputDeviceCharacteristics controllerCharacteristics;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        Debug.Log("# of devices: " + devices.Count);
        foreach (var item in devices)
        {
            Debug.Log("   Manufacturer: " + item.manufacturer + "\n");
            Debug.Log("           Name: " + item.name + "\n");
            Debug.Log("Characteristics: " + item.characteristics + "\n");

        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
