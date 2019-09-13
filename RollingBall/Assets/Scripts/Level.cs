using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<Transform> zoneAnchors = new List<Transform>();
    public List<GameObject> zonePrefabs = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < zoneAnchors.Count; i++)
        {
            Instantiate(zonePrefabs[i], zoneAnchors[i].transform.position, Quaternion.identity, zoneAnchors[i].transform);
        }
    }
}
