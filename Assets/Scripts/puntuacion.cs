using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class puntuacion : MonoBehaviour
{
    public TMP_Text puntos, pt1, pt2, pt3;
    public static int total, punt1, punt2, punt3;

    void Start()
    {
        puntos.GetComponent<TextMeshProUGUI>().text = "0";
    }

    private void Update()
    {
        puntos.GetComponent<TextMeshProUGUI>().text = total.ToString();
        pt1.GetComponent<TextMeshProUGUI>().text = punt1.ToString();
        pt2.GetComponent<TextMeshProUGUI>().text = punt2.ToString();
        pt3.GetComponent<TextMeshProUGUI>().text = punt3.ToString();
    }

    public static void MeterPuntos(int punto)
    {
        if (punto == 1)
            punt1 ++;
        if (punto == 2)
            punt2 ++;
        if (punto == 3)
            punt3 ++;

        total += punto;
    }


}
