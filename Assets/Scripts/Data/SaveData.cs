using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public void saveDataNow(string json)
    {
        //en este vamos a traducir el textoa lenguaje maquina
        BinaryFormatter formatter = new BinaryFormatter();
        //aqui abrimos la direccion de nuestro archivo para poder escribir en el
        FileStream file = File.Create(Application.persistentDataPath + "/data.json");
        //guardamos la informacion
        formatter.Serialize(file, json);
        //cerramos el archivo para evitar errores
        file.Close();
    }

    public DataGuardable LoadData()
    {
        //revisamos si existe nuestro archivo
        if(File.Exists(Application.persistentDataPath + "/data.json"))
        {
            //iniciamos el formater
            BinaryFormatter formatter = new BinaryFormatter();
            //abrir el archivo para poder leerlo
            FileStream file = File.Open(Application.persistentDataPath + "/data.json", FileMode.Open);
            //obtenemos el json
            var json = formatter.Deserialize(file).ToString();
            //para revisar el retorno
            Debug.Log(json);
            //cerramos el archivo
            file.Close();

            //estoy regresando la informacion
            return JsonUtility.FromJson<DataGuardable>(json);
        }
        //si no existe el archivo, regreso un archivo en limpio
        return new DataGuardable();
    }
}


    [Serializable]

    public class DataGuardable
    {
        public string nombre;
        public int puntaje;
        public float tiempoJugado;
    }
