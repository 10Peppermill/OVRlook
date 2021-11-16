using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBounds : MonoBehaviour
{
    public GameObject map;
    public GameObject marker;

    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = map.GetComponent<Renderer>();
        Debug.Log(rend.bounds.min);
        Debug.Log(rend.bounds.max);

        //Instantiate(marker, transform.TransformPoint(rend.bounds.min), Quaternion.identity);
        //Instantiate(marker, transform.TransformPoint(rend.bounds.max), Quaternion.identity);

        Instantiate(marker, rend.bounds.min, Quaternion.identity);
        Instantiate(marker, rend.bounds.max, Quaternion.identity);

        //List<Vector3> VertList = new List<Vector3>(map.GetComponent<MeshFilter>().sharedMesh.vertices);
        //Debug.Log(VertList[0]);
        //Debug.Log(VertList[10]);
        //Debug.Log(VertList[110]);
        //Debug.Log(VertList[120]);


        //foreach (var Vert in VertList)
        //{
        //    Instantiate(marker, transform.TransformPoint(Vert), Quaternion.identity);
        //}

        //Instantiate(marker, transform.TransformPoint(VertList[0]), Quaternion.identity);
        //Instantiate(marker, transform.TransformPoint(VertList[10]), Quaternion.identity);
        //Instantiate(marker, transform.TransformPoint(VertList[110]), Quaternion.identity);
        //Instantiate(marker, transform.TransformPoint(VertList[120]), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
