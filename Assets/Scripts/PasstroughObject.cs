using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasstroughObject : MonoBehaviour
{
  void Start()
  {
    GameObject ovrCameraRig = GameObject.Find("OVRCameraRig");
    OVRPassthroughLayer layer = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
    layer.AddSurfaceGeometry(gameObject, false);

    // Disable the mesh renderer to avoid rendering the surface within Unity
    MeshRenderer mr = GetComponent<MeshRenderer>();
    if (mr)
    {
      mr.enabled = false;
    }
  }
}