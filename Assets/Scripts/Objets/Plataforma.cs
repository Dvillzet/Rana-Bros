using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    public float distancia;
    public float velocidad;
    public float direccion = 1;
    public float distanciaParaActivar;

    Transform target;

    bool estaVisible;
    bool seEstaMoviendo;

    Vector2 posicionInicial;
    Vector2 posicionFinal;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        distancia = Mathf.Abs(distancia);
        posicionInicial = new Vector2(transform.position.x - distancia, transform.position.y);
        posicionFinal = new Vector2(transform.position.x + distancia, transform.position.y);
    }

    public void OnBecameVisible()
    {
        estaVisible = true;
    }

    public void OnBecameInvisible()
    {
        estaVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estaVisible)
        {
            moverPlataforma();
        }
        else
        {
            bool seDebeMover = Vector3.Distance(transform.position, target.position) < distanciaParaActivar;

            if (seDebeMover)
            {
                moverPlataforma();
            }
        }
    }

    private void moverPlataforma()
    {
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);

        if (posicionFinal.x <= transform.position.x)
        {
            direccion = -1;
        }

        if (posicionInicial.x >= transform.position.x)
        {
            direccion = 1;
        }
    }
}
