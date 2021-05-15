using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rigi;
    protected Collider2D coli;

    public float velocidadMovimiento;
    Vector2 direccion = Vector2.left;
    public LayerMask queEsPared;

    public float fuerzaBrinco;
    protected bool necesitaBrincar;
    protected bool estaTocandoPiso;

    public bool mostrarGizmo;

    [Header("Detección Pared")]
    public bool necesitaDetectarPiso;
    public float radioPiso;
    public float distanciaPiso;
    public LayerMask queEsPiso;

    [Header("Detección Pared")]
    public bool necesitaDetectarPared;
    public float alturaVista;
    public float distanciaVista;

    protected bool enemyAlive;
    protected bool onScreen;

    private void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        coli = GetComponent<Collider2D>();
    }

    protected void Jump()
    {
        if (estaTocandoPiso)
        {
            rigi.AddForce(Vector2.up * fuerzaBrinco);
        }
        
    }
 
    protected void Walk()
    {
        if (!onScreen) return;
        transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
    }

    protected bool DetectarPared()
    {
        if (!necesitaDetectarPared) return false;
        if (!onScreen) return false;
        var posicionInicio = new Vector2(transform.position.x, transform.position.y + alturaVista);
        var hit = Physics2D.Raycast(posicionInicio, direccion, distanciaVista, queEsPared);
        return (hit.collider != null);
    }

    public void CambiarDireccion()
    {
        if (direccion == Vector2.left)
        {
            direccion = Vector2.right;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            direccion = Vector2.left;
            transform.localScale = Vector3.one;
        }
    }

    public bool detectarPiso()
    {
        estaTocandoPiso = coli.IsTouchingLayers(queEsPiso);
        if (!necesitaDetectarPiso) return true;
        if (!onScreen) return true;

        
        Debug.Log(estaTocandoPiso);

        var posicionCirculo = new Vector3(transform.position.x + -1 * distanciaVista, transform.position.y + alturaVista + distanciaPiso, transform.position.z);

        var circulo = Physics2D.OverlapCircle(posicionCirculo, radioPiso);
        return circulo != null;
    }

    private void OnDrawGizmos()
    {
        if (!mostrarGizmo) return;

        //mustra la distanbcia de la vista
        Gizmos.color = Color.red;
        var posicionInicio = new Vector3(transform.position.x, transform.position.y + alturaVista, transform.position.z);
        Gizmos.DrawLine(posicionInicio, posicionInicio + Vector3.left * distanciaVista);

        Gizmos.color = Color.green;

        var posicionCirculo = new Vector3(transform.position.x + -1 * distanciaVista, transform.position.y + alturaVista + distanciaPiso, transform.position.z);
        Gizmos.DrawWireSphere(posicionCirculo, radioPiso);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !enemyAlive)
        {
            collision.collider.GetComponent<PlayerMovement>().Mori();
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Player"))
        {
            Debug.Log("Me aplastaron");
            enemyAlive = true;
            target.GetComponent<PlayerMovement>().matePersonaje(100);
            GetComponent<Collider2D>().enabled = false;
        }
        if (target.CompareTag("Collector"))
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        if (enemyAlive)
        {
            Destroy(gameObject);

        }else{

            StartCoroutine(esperaDesactivar());
        }
        
    }

    IEnumerator esperaDesactivar()
    {
        yield return new WaitForSeconds(5f);
        onScreen = false;
    }

    private void OnBecameVisible()
    {
        StopAllCoroutines();
        onScreen = true;
    }
}
