using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaCaida : MonoBehaviour
{
    public int espera;
    Rigidbody2D rigi;
    public bool seNecesitaRestablecer;

    public GameObject modelo;
    Vector3 posicionInicial;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        rigi.bodyType = RigidbodyType2D.Static;
        posicionInicial = transform.position;
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(empezarCaida());
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Collector"))
        {
            if(seNecesitaRestablecer){
                Instantiate(modelo, posicionInicial, Quaternion.identity);
            }
            Destroy(gameObject);

        }
    }

    IEnumerator empezarCaida()
    {
        yield return new WaitForSeconds(espera);
        rigi.bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Animator>().Play("Apagado");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
