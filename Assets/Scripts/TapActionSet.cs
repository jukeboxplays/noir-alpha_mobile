using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using Cinemachine;

public class TapActionSet : MonoBehaviour
{
    public GameObject cameraSet;
    CinemachineBlendListCamera cameraSet_blendCam;
    CinemachineInvertBlendList cameraSet_Inverter;

    public TextAsset sceneJson;
    JSONNode sceneSet;

    string vcamName;
    GameObject vcam;

    // Start is called before the first frame update
    void Start()
    {
        sceneSet = JSON.Parse(sceneJson.text);
        cameraSet_blendCam = cameraSet.GetComponent<CinemachineBlendListCamera>();
        cameraSet_Inverter = cameraSet.GetComponent<CinemachineInvertBlendList>();
    }

    void Update()
    {
        //if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log(hit.collider.name);
                if (hit.collider != null)
                {
                    SwitchCameraHandler(hit.collider.name);
                }
            }
        }
    }

    void SwitchCameraHandler(string objectName)
    {
        //Debug.Log(sceneSet["Scene"][0]["vcam"]);
        
        
        foreach (var i in sceneSet["Scene"])
        {
            //Debug.Log(objectName + " | " + i.Value["name"]);

            if (i.Value["name"] == objectName)
            {
                vcamName = i.Value["vcam"];
            }
        }

        vcam = cameraSet.transform.Find(vcamName).gameObject;

        for (int c = 0; c < cameraSet_blendCam.m_Instructions.Length; c++)
        {
            if (cameraSet_blendCam.m_Instructions[c].m_VirtualCamera.name != "CM_mainCam")
            {
                cameraSet_blendCam.m_Instructions[c].m_VirtualCamera = vcam.GetComponent<CinemachineVirtualCameraBase>();
            }
        }

        cameraSet_Inverter.Inverse();
    }
}


