using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bola : MonoBehaviour
{
    public GameObject pepito;
    Rigidbody _bola;
    public float potenciadorDisparo = 5f;
    public bool agarrar = false;
    public Transform inicio;
    private Vector3 posInicio, posFinal;
    float fuerzaDisparo = 3.0f;

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
        }

        if (_bola.isKinematic)
        {
            if (Input.GetMouseButtonDown(0))
             {
                 RaycastHit hit;
                 Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                 if (Physics.Raycast(ray, out hit) && hit.transform.tag == "ZonaTiro")
                 {
                    Debug.Log("Punto de entrada: " + hit.point);
                    _bola.transform.position = hit.point;
                    _bola.transform.rotation = Quaternion.Euler(0, 0, 35);
                    posInicio = hit.point;
                 }
            }
            if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && hit.transform.tag == "ZonaTiro")
                {
                    float distance = transform.position.z - Camera.main.transform.position.z;
                    posFinal = hit.point;
                    Vector3 direction = posFinal - posInicio;                   
                    fuerzaDisparo = Vector3.Distance(posInicio,posFinal);
                    Debug.Log("Dirección: " + direction);
                    Debug.Log("Fuerza disparo: " + fuerzaDisparo);
                   // _bola.transform.rotation = Quaternion.Euler(0, direction.y, 35);
                    _bola.isKinematic = false;
                    _bola.AddForce(direction * fuerzaDisparo * potenciadorDisparo, ForceMode.Impulse);
                }
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "ZonaRecogida")
            agarrar = true;
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "ZonaRecogida")
            agarrar = false;
    }

}
