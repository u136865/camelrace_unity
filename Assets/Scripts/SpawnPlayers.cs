using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{

    public GameObject pt_value1, pt_value2, pt_value3, puntos, reinicio;
    public float minX, maxX, minZ, maxZ;
    float players;
    public GameObject[] playersPref;
    public GameObject[] camellos;
    GameObject camello, IU, bola, pt1, pt2, pt3, ptpuntos, btreinicio;

    private void Start()
    {
        IU = GameObject.Find("spawners");
        pt1 = PhotonNetwork.Instantiate(pt_value1.name, pt_value1.transform.position, Quaternion.identity);
        pt1.transform.SetParent(IU.transform);
        pt2 = PhotonNetwork.Instantiate(pt_value2.name, pt_value2.transform.position, Quaternion.identity);
        pt2.transform.SetParent(IU.transform);
        pt3 = PhotonNetwork.Instantiate(pt_value3.name, pt_value3.transform.position, Quaternion.identity);
        pt3.transform.SetParent(IU.transform);
        ptpuntos = PhotonNetwork.Instantiate(puntos.name, GameObject.Find("puntuacionp").transform.position, Quaternion.identity);
        ptpuntos.transform.SetParent(IU.transform);
        btreinicio = PhotonNetwork.Instantiate(reinicio.name, reinicio.transform.position, Quaternion.identity);
        btreinicio.transform.SetParent(IU.transform);
        players = PhotonNetwork.CurrentRoom.PlayerCount;//Miramos a ver cuántos players hay en la sala
        AsignarCamello();//asignamos el camello dependiendo el número de players
    }

    void AsignarCamello()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minZ, maxZ));
        switch (players)
        {
            case 1:
                camello = PhotonNetwork.Instantiate(camellos[0].name, GameObject.Find("camello1p").transform.position, Quaternion.identity); //Azul
                bola = PhotonNetwork.Instantiate(playersPref[0].name, randomPosition, Quaternion.identity);
                ColocarRenombrar();
                break;
            case 2:
                camello = PhotonNetwork.Instantiate(camellos[1].name, GameObject.Find("camello2p").transform.position, Quaternion.identity); //Morado
                bola = PhotonNetwork.Instantiate(playersPref[1].name, randomPosition, Quaternion.identity);
                ColocarRenombrar();
                break;
            case 3:
                camello = PhotonNetwork.Instantiate(camellos[2].name, GameObject.Find("camello3p").transform.position, Quaternion.identity); //Rojo
                bola = PhotonNetwork.Instantiate(playersPref[2].name, randomPosition, Quaternion.identity);
                ColocarRenombrar();
                break;
            case 4:
                camello = PhotonNetwork.Instantiate(camellos[3].name, GameObject.Find("camello4p").transform.position, Quaternion.identity); //Rosa
                bola = PhotonNetwork.Instantiate(playersPref[3].name, randomPosition, Quaternion.identity);
                ColocarRenombrar();
                break;
            case 5:
                camello = PhotonNetwork.Instantiate(camellos[4].name, GameObject.Find("camello5p").transform.position, Quaternion.identity); //Verde
                bola = PhotonNetwork.Instantiate(playersPref[4].name, randomPosition, Quaternion.identity);
                ColocarRenombrar();
                break;
            default:
                break;
        }
    }

    void ColocarRenombrar()
    {
        camello.transform.SetParent(IU.transform);
        camello.name = "Camello";
        bola.name = "Bola";
    }

}
