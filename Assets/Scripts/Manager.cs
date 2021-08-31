using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.SendRate = 20; //20
        PhotonNetwork.SerializationRate = 5; //10
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public void Reiniciar()
    {
        SceneManager.LoadScene("Index");
    }

    public void Jugar()
    {
        SceneManager.LoadScene("Loading");
    }
}
