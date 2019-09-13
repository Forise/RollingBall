using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    #region Fields
    public Player player;
    public List<Transform> zoneAnchors = new List<Transform>();
    public List<GameObject> zonePrefabs = new List<GameObject>();

    private List<GameObject> generatedPrefabs = new List<GameObject>();
    #endregion Fields

    #region Mono Methods
    private void Awake()
    {
        for (int i = 0; i < zoneAnchors.Count; i++)
        {
            int zoneIndex = Random.Range(0, zonePrefabs.Count);
            Instantiate(zonePrefabs[zoneIndex], zoneAnchors[i].transform.position, Quaternion.identity, zoneAnchors[i].transform);
            zonePrefabs.RemoveAt(zoneIndex);
        }
    }
    #endregion Mono Methods
}
