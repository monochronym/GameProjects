using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public Camera camera;
    public int resolutionx;
    public int resolutiony;
    void Start()
    {
        Screen.SetResolution(resolutionx, resolutiony,FullScreenMode.MaximizedWindow);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(transform.position, new Vector3(resolutionx,resolutiony,0));
    }
}
