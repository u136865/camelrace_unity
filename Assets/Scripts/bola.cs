using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bola : MonoBehaviour
{
    Rigidbody _bola;
    public GameObject pepito;
    public float potenciadorDisparo = 5f;
    public bool agarrar = false;
    public int puntos=0;
    public Transform inicio;
    public Vector3 posInicio, posFinal;
    [SerializeField]
    float fuerzaDisparo;
    [SerializeField]
    float tiempoInicio;
    [SerializeField]
    float divisor = 10f;
    [SerializeField]
    float velocidad;
    [SerializeField]
    float distanciaPuntos;

    void Start()
    {
        _bola = transform.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if (agarrar)
        {
            _bola.transform.position = inicio.position;
            _bola.isKinematic = true;
            agarrar = false;
        }

        if (_bola.isKinematic)
        {
            if (Input.GetMouseButtonDown(0)) 
             {
                tiempoInicio = Time.time;
                TimeSpan timeSpan = TimeSpan.FromSeconds(tiempoInicio);
                string timeText = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                 RaycastHit hit;
                 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                 if (Physics.Raycast(ray, out hit))
                 {
                    if(hit.transform.tag == "ZonaTiro")
                    {
                        _bola.transform.position = hit.point;
                        _bola.transform.rotation = Quaternion.Euler(0, 0, 35);
                        posInicio = hit.point;
                    }
                 }
            }
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && hit.transform.tag == "ZonaTiro")
                {
                    posFinal = hit.point;
                    Vector3 direction = posFinal - posInicio;
                    distanciaPuntos = direction.magnitude;
                    fuerzaDisparo = (distanciaPuntos / (Time.time - tiempoInicio))/divisor;
                    _bola.isKinematic = false;
                    _bola.AddForce(direction * fuerzaDisparo, ForceMode.Impulse);
                }
            }
            velocidad = _bola.velocity.magnitude;
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "ZonaRecogida")
            agarrar = true;
        if (col.gameObject.tag == "ZonaExterior")
            agarrar = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ZonaRecogida")
            agarrar = false;
        if (col.gameObject.tag == "Puntua")
        {
            puntos = int.Parse(col.gameObject.name);
            puntuacion.MeterPuntos(puntos);
        }
    }

    public void Reiniciar()
    {
        agarrar = true;
    }

}
