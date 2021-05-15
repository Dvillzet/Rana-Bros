using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var dataGuardada = FindObjectOfType<SaveData>().LoadData();
        Debug.Log($"puntos anteriores {dataGuardada.puntaje}");
        DataGuardable infoAGuardar = new DataGuardable();
        infoAGuardar.nombre = "pedro";
        infoAGuardar.puntaje = GameManager.points;
        infoAGuardar.tiempoJugado = 15.60f;
        string json = JsonUtility.ToJson(infoAGuardar);
        Debug.Log($"Json es {json}");
        FindObjectOfType<SaveData>().saveDataNow(json);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
