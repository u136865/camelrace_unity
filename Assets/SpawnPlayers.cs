using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject playerPrefab, estadisticas;
    public Vector3 posicionEstadisticas;
    public float minX, maxX, minZ, maxZ;

    private void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
        PhotonNetwork.Instantiate(estadisticas.name, posicionEstadisticas, Quaternion.identity);
    }

}