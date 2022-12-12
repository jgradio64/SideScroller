using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToStartMenu : MonoBehaviour
{
    public void StartMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }
}

