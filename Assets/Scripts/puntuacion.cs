using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class Puntuacion : MonoBehaviour
{
    public TMP_Text puntos, pt1, pt2, pt3, textTiempo, textTiempoFinal;
    public int total, punt1, punt2, punt3, totalMovimientoCamello;
    public int multiplicadorCamello = 3;
    public RectTransform camello;
    public GameObject pantallaWin;

    float segundosTiempo = 0, camelloMovimiento = 450; //El camello va del 450 de x al -450 de x para llegar a la meta   
    int minutosTiempo = 0;
    //int horasTiempo = 0;

    bool winner = false;


    PhotonView view; //variable de Photon para saber de quién es el player

    void Start()
    {
        puntos.GetComponent<TextMeshProUGUI>().text = "0";
        camello = camello.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (view.IsMine) //Comprobamos que es nuestro el player
        {
            TiempoJuego();
            MoverCamello();
            puntos.GetComponent<TextMeshProUGUI>().text = total.ToString();
            pt1.GetComponent<TextMeshProUGUI>().text = punt1.ToString();
            pt2.GetComponent<TextMeshProUGUI>().text = punt2.ToString();
            pt3.GetComponent<TextMeshProUGUI>().text = punt3.ToString();
            camello.localPosition = new Vector3(camelloMovimiento, camello.localPosition.y, camello.localPosition.z);
        }
        
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
        if (punto == 1)
            punt1++;
        if (punto == 2)
            punt2++;
        if (punto == 3)
            punt3++;

        total += punto;
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
}
