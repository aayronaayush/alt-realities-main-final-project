using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshTeleport : MonoBehaviour
{
    public MeshCollider meshCollider;
    // Start is called before the first frame update
    void Start()
    {
        var triangulated = NavMesh.CalculateTriangulation();
        var mesh = new Mesh
        {
            vertices = triangulated.vertices,
            triangles = triangulated.indices,
        };

        meshCollider.sharedMesh = mesh;
    }

}
