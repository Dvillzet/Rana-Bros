using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{
    //Aqu� se declara que vamos a seguir al jugador.
    public GameObject follow;
    public Vector2 minCamPos, maxCamPos;

    void Start()
    {
        
    }



    void LateUpdate()
    {
        //Aqu� dice que posX y posY seran los mismos que la posici�n X y Y del objeto seguido.
        float posX = follow.transform.position.x;
        float posY = follow.transform.position.y;

        //Aqu� se le asigna a la c�mara la posicion X y Y que antes de guardaron en posX y posY.
        transform.position = new Vector3(
            Mathf.Clamp(posX, minCamPos.x, maxCamPos.x), 
            Mathf.Clamp(posY, minCamPos.y, maxCamPos.y), 
            transform.position.z);
    }
}
