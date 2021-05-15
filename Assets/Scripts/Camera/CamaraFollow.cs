using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    //Aquí se declara que vamos a seguir al jugador.
    public GameObject follow;
    public Vector2 minCamPos, maxCamPos;

    void Start()
    {
        
    }



    void LateUpdate()
    {
        //Aquí dice que posX y posY seran los mismos que la posición X y Y del objeto seguido.
        float posX = follow.transform.position.x;
        float posY = follow.transform.position.y;

        //Aquí se le asigna a la cámara la posicion X y Y que antes de guardaron en posX y posY.
        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), 
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), 
            transform.position.z);
    }
}
