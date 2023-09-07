using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Librerias necesarias para el guardado
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class SaveData
{
    //Datos principales a guardar
    public List<int> gotoAdderir = new List<int>();
    public List<string> inventarioitemsnombre = new List<string>();
    public List<int> inventarioitemsamount = new List<int>();

}
public class GameData : MonoBehaviour
{
    public SaveData saveData;
    public static GameData instancia;

    private void Awake()
    {
        if(instancia == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instancia = this;
        }else if(instancia != null)
        {
            Destroy(instancia.gameObject);
            instancia = this;
        }

        //Si el archivo existo vamos a cargarlo, sino a guardarlo
        if (File.Exists(Application.persistentDataPath + "/GamePlayer.dat"))
        {
            Load();
        }
        else
        {
            Save();
        }
    }


    //metodo que llamaremos cuando queramos guardar

    public void Save()
    {
        //Formateador que puede leer archivos binarios
        BinaryFormatter formatter = new BinaryFormatter();
        //Ruta donde queremos guardar el archivo
        FileStream file = File.Open(Application.persistentDataPath + "/GamePlayer.dat", FileMode.Create);
        //Creamos una copia de los datos del SaveData
        SaveData data = new SaveData();
        data = saveData;
        //Serializamos la informacion y guardamos
        formatter.Serialize(file, data);
        file.Close();
        print("Data Saved");
    }


    public void Load()
    {
      if(File.Exists(Application.persistentDataPath + "/GamePlayer.dat"))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/GamePlayer.dat", FileMode.Open);
            //Cargamos nuevamente los datos
            saveData = formatter.Deserialize(file) as SaveData;
            file.Close();
            print("Data Loaded");
        } 
    }

    public void BorrarData()
    {
        if (File.Exists(Application.persistentDataPath + "/GamePlayer.dat"))
        {
            Debug.Log("Aqui borré");
            File.Delete(Application.persistentDataPath + "/GamePlayer.dat");
        }
    }


    public void LiampiarLista()
    {
        saveData.gotoAdderir.Clear();
        saveData.inventarioitemsnombre.Clear();
        saveData.inventarioitemsamount.Clear();
        Save();
    }
}
