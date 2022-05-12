using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHidder : MonoBehaviour
{
    private MeshRenderer[] meshs;

    private void Awake()
    {
        meshs = GetComponentsInChildren<MeshRenderer>();

    }

    public void Show()
    {
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.enabled = true;
        }
    }

    public void Hide()
    {
        foreach (MeshRenderer mesh in meshs)
        {
            mesh.enabled = false;
        }
    }

}
