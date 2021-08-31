using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class Puntuacion : MonoBehaviour
{
    public TMP_Text textTiempo, textTiempoFinal, player1, player2, player3, player4, player5;
    public int total, punt1, punt2, punt3, totalMovimientoCamello;
    public int multiplicadorCamello = 3;
    public GameObject pantallaWin, pantallaInicio;
    public Button botonSalaEspera;
    public float tiempoMovimiento = 10;
    float segundosTiempo = 0, camelloMovimiento = 450; //El camello va del 450 de x al -450 de x para llegar a la meta   
    
    int minutosTiempo = 0;
    //int horasTiempo = 0;

    bool winner = false;
    Bola bola;

    PhotonView view;

    void Start()
    {
        Time.timeScale = 0;
        view = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient)
            botonSalaEspera.interactable = true;

    }

    private void Update()
    {
        ConexionPlayers();
        
        TiempoJuego();
        if (view.IsMine) //Comprobamos que es nuestro el player
        {
            MoverCamello();
            GameObject.Find("puntos(Clone)").GetComponent<TextMeshProUGUI>().text = total.ToString();
            GameObject.Find("1pt_value(Clone)").GetComponent<TextMeshProUGUI>().text = punt1.ToString();
            GameObject.Find("2pt_value(Clone)").GetComponent<TextMeshProUGUI>().text = punt2.ToString();
            GameObject.Find("3pt_value(Clone)").GetComponent<TextMeshProUGUI>().text = punt3.ToString();
            GameObject.Find("Camello").GetComponent<RectTransform>().localPosition = Vector3.Lerp(GameObject.Find("Camello").GetComponent<RectTransform>().localPosition, new Vector3(camelloMovimiento, GameObject.Find("Camello").GetComponent<RectTransform>().localPosition.y, GameObject.Find("Camello").GetComponent<RectTransform>().localPosition.z), tiempoMovimiento);
        }        
    }

    private void ConexionPlayers()
    {
        int players = PhotonNetwork.CurrentRoom.PlayerCount;
        player1.GetComponent<TextMeshProUGUI>().text = "Player 1 Connected";
        if (players == 2) player2.GetComponent<TextMeshProUGUI>().text = "Player 2 Connected";
        if (players == 3) player3.GetComponent<TextMeshProUGUI>().text = "Player 3 Connected";
        if (players == 4) player4.GetComponent<TextMeshProUGUI>().text = "Player 4 Connected";
        if (players == 5) player5.GetComponent<TextMeshProUGUI>().text = "Player 5 Connected";
    }

    void TiempoJuego()
    {
        segundosTiempo += Time.deltaTime;
        minutosTiempo = (int)segundosTiempo / 60;
        // horasTiempo = (int)segundosTiempo / 3600;
        //UI Text refresh
        textTiempo.text = string.Format("{0}:{1}", minutosTiempo.ToString("00"), ((int)segundosTiempo % 60).ToString("00"));//{0}:{1}:{2} horasTiempo.ToString("00"),
    }

    public void MeterPuntos(int punto)
    {
        if (view.IsMine) //Comprobamos que es nuestro el player
        {
            if (punto == 1)
                punt1++;
            if (punto == 2)
                punt2++;
            if (punto == 3)
                punt3++;

            total += punto;
        }
    }
    public void BotonBola()
    {
        bola = GameObject.Find("Bola").GetComponent<Bola>();
        bola.Reiniciar();   
    }

    void MoverCamello()
    {
        if (!winner)
        {
            int mover = 450 - ((punt1 + punt2 * 2 + punt3 * 3) * multiplicadorCamello);
            camelloMovimiento = mover;
            if (mover <= -450)
            {
                winner = true;
                Winner();
            }
        }
    }

    void Winner()
    {
        textTiempoFinal.text = textTiempo.text;
        Debug.Log("¡VICTORIA!");
        pantallaWin.SetActive(true);
    }

    void ResetPositions(GameObject pepito)
    {
        pepito.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(0f, 0f, 0f);
        pepito.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        pepito.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 0f);
        pepito.GetComponent<RectTransform>().position = new Vector3(0f, 0f, 0f);
        pepito.transform.position = new Vector3(0f, 0f, 0f);
        pepito.transform.localPosition = new Vector3(0f, 0f, 0f);
    }   

    public void ComenzarPartida()
    {
        pantallaInicio.SetActive(false);
        Time.timeScale = 1;
    }
}
