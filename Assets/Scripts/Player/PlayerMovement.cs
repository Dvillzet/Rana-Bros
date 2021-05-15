using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xSpeed;
    bool isLookingRight = true;
    public float jumpForce;
    bool isJumping;
    bool vive = true;
    Rigidbody2D rigi;
    bool isTouchingGround = true;
    public LayerMask whatIsFloor;
    int direccion = 0;

    GameManager Manager;

    public GameObject Collected;

    Animator anim;

    CircleCollider2D circle;
    // Start is called before the first frame update
    void Start()
    {
        rigi = GetComponent<Rigidbody2D>();

        circle = GetComponent<CircleCollider2D>();

        Manager = FindObjectOfType<GameManager>();

        anim = GetComponent<Animator>();
    }

    public void ButtonJump()
    {
        isJumping = true;
    }
    private void FixedUpdate()
    {
        isTouchingGround = Physics2D.IsTouchingLayers(circle, whatIsFloor) && rigi.velocity.y <= 0;
        if (isJumping && isTouchingGround)
        {
            rigi.AddForce(Vector2.up * jumpForce);
            isJumping = false;

        }
    }

    public void ApretarBoton(int Valor)
    {
        direccion = Valor;
    }

    public void LiberarBoton()
    {
        direccion = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (vive)
        {

#if (UNITY_ANDROID || UNITY_IOS)
            float horizontal = direccion;
            
#else
        float horizontal = Input.GetAxis("Horizontal");
#endif

            anim.SetBool("xMovement", horizontal!= 0);
            anim.SetFloat("ySpeed", rigi.velocity.y);
            if (horizontal != 0)
            {

                transform.Translate(Vector2.right * horizontal * xSpeed * Time.deltaTime);

                if (horizontal > 0 && !isLookingRight)
                {
                    transform.localScale = Vector3.one;
                    isLookingRight = true;
                }
                else if (horizontal < 0 && isLookingRight)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                    isLookingRight = false;
                }

            }
#if (UNITY_ANDROID || UNITY_IOS)
#else

            isJumping = Input.GetAxis("Jump") > 0 && isTouchingGround;
#endif

        }          
    }

    public void Mori()
    {
        circle.enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetTrigger("Golpeado");
        transform.localScale = new Vector3(1f, -1f, 1f);
        vive = false;
     

    }


    public void matePersonaje(int valorMuerte)
    {
        Manager.agregarPuntos(valorMuerte);
    }

    private void OnBecameInvisible()
    {
        if (!vive)
        {
            StartCoroutine(EsperarMuerte());
        }
    }

    

    public void OnTriggerEnter2D(Collider2D target)
    {
        switch (target.tag)
        {
            case "Finish":
                Manager.endLevel();
                break;
            case "Collector":
                if (vive)
                {
                    Manager.Muerto();
                    vive = false;
                }
                

                break;
            case "Fruit":
                Manager.agregarPuntos(target.GetComponent<Fruit>().pointValue);
                Destroy(target.gameObject);
                Instantiate(Collected, target.transform.position, Quaternion.identity);
                break;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma") && transform.position.y > collision.transform.position.y)
        {
            transform.SetParent(collision.transform);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            transform.SetParent(null);

        }
    }



    IEnumerator EsperarMuerte()
    {
        Debug.Log("Estoy entrando aqui");
        yield return new WaitForSeconds(2f);
        Manager.Muerto();

    }
}
