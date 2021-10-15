using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;



public class HandPresence : MonoBehaviour
{
    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

       
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        Debug.Log(devices.Count);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corresponding model");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }
        }
        spawnedHandModel = Instantiate(handModelPrefab, transform);
        handAnimator = spawnedHandModel.GetComponent<Animator>();
    }

    void UpdateHandAnimation()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void Update()
    {
        if (showController)
        {
            spawnedHandModel.SetActive(false);
            spawnedController.SetActive(true);
            //Debug.Log(spawnedController.transform.position);
        }
        else
        {
            spawnedHandModel.SetActive(true);
            spawnedController.SetActive(false);
            UpdateHandAnimation();
            targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
            //Debug.Log("Trigger: " + triggerValue);
            //Debug.Log("Grip: " + gripValue);

            //Debug.Log(spawnedHandModel.transform.position);
        }
    }
}