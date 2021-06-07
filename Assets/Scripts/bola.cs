using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bola : MonoBehaviour
{
    public Transform pepito;
    Rigidbody _bola;
    public float shootPower = 30f;
    public bool agarrar = false;
    public Transform inicio;
    private Vector3 mOffset, targetPos;
    public float mZCoord,speed = 3.0f;

    void Start()
    {
        targetPos = transform.position;
        _bola = transform.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
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
                 if (Physics.Raycast(ray, out hit, 1000f) && hit.transform.tag == "ZonaTiro")
                 {
                    Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward, Color.green);
                    Debug.Log(hit.transform.tag);
                    Debug.Log("Posicion de entrada: " + hit.transform.position);
                    Instantiate(pepito, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.Euler(55, 90, 180));
                 }
            }
             if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 1000f) && hit.transform.tag == "ZonaTiro")
                {
                    Debug.Log("Posicion de salida: " + hit.transform.position);
                    Instantiate(pepito, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.Euler(55, 90, 180));
                    float distance = transform.position.z - Camera.main.transform.position.z;
                     targetPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
                     targetPos = Camera.main.ScreenToWorldPoint(targetPos);
                    Vector3 direction = transform.position - targetPos;
                     _bola.isKinematic = false;
                     _bola.AddForce(direction * speed);
                }
             }
        }
    }

    void OnMouseDrag()
        {
            transform.position = GetMouseAsWorldPoint() + mOffset;
        }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
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
