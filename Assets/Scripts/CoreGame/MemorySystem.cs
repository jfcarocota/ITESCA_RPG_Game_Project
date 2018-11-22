﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace GameCore
{
    namespace MemorySystem
    {
        public class MemorySystem
        {
            public static bool loadGame = false;
            /// <summary>
            /// Sirve para guardar la memoria de la aplicación.
            /// </summary>
            /// <param name="gameData">Representa una variable de tipo GameData, esta es la info que quieres guardar.</param>
            /// <param name="fileName">Esto es la ruta o path donde quieres guadar el objeto GameData. (Se recomienda usar el PersistenceData path)</param>
            public static void Save(GameData gameData, string fileName)
            {
                BinaryFormatter bf = new BinaryFormatter();
                string path = Path.Combine(Application.persistentDataPath, fileName);
                FileStream file = File.Create(path);
                bf.Serialize(file, gameData);
                file.Close();
                Debug.Log("Saved at: " + path);
            }

            public static GameData LoadData(string fileName)
            {
                string path = Path.Combine(Application.persistentDataPath, fileName);

                if (File.Exists(path))
                {
                    FileStream file = File.Open(path, FileMode.Open);
                    BinaryFormatter bf = new BinaryFormatter();
                    GameData gameData = (GameData)bf.Deserialize(file);
                    file.Close();
                    return gameData;
                }

                return new GameData();
                
            }
        }
    }
}
