using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static int points = 0;
    public static int vidas = 3;

    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI VidasDisplay;

    public GameObject botonJump;
    public GameObject botonIzq;
    public GameObject botonDer;

    public void agregarPuntos(int Puntos)
    {
        points += Puntos;
        scoreText.text = points.ToString("000");
    }

 
    // Start is called before the first frame update
    void Start()
    {
        agregarPuntos(0);
        VidasDisplay.text = vidas.ToString("0");

#if(UNITY_STANDALONE || UNITY_WEBGL)
        botonJump.SetActive(false);
        botonIzq.SetActive(false);
        botonDer.SetActive(false);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Muerto()
    {
        vidas--;
        VidasDisplay.text = vidas.ToString("0");
        if (vidas <= 0)
        {

            SceneManager.LoadScene("GameOver");

        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        

    }

    public void endLevel()
    {
        int indexActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexActual + 1);
        Debug.Log("Terminé el nivel");
    }
}
