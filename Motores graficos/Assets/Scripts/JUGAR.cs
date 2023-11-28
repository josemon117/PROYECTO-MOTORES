using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JUGAR : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Nivel 1"); // Reemplaza "Level1" con el nombre de tu escena de nivel 1
    }
}