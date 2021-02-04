using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineInvertBlendList : MonoBehaviour
{
    CinemachineBlendListCamera blendCam;
    int lenInstruct;

    public void Inverse()
    {
        blendCam = this.gameObject.GetComponent<CinemachineBlendListCamera>();
        lenInstruct = blendCam.m_Instructions.Length;

        for (int i = 0; i < lenInstruct / 2; ++i)
        {
            var aux = blendCam.m_Instructions[i].m_VirtualCamera;
            blendCam.m_Instructions[i].m_VirtualCamera = blendCam.m_Instructions[lenInstruct - 1 - i].m_VirtualCamera;
            blendCam.m_Instructions[lenInstruct - 1 - i].m_VirtualCamera = aux;
        }

    }
}
