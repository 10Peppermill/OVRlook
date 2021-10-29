using UnityEngine;
using System.Collections;

public class CameraView : MonoBehaviour
{
    public int resWidth = 2550;
    public int resHeight = 3300;
    public Camera cam;

    private bool takeHiResShot = false;
    private Texture2D screenShot;

    public Texture2D getScreenShot()
    {
        cam.enabled = true;
        TakeHiResShot();
        cam.enabled = false;
        return screenShot;
    }

    private void TakeHiResShot()
    {
        takeHiResShot = true;
    }

    void LateUpdate()
    {
        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot)
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            cam.targetTexture = rt;
            screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            cam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            cam.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
        }
    }
}