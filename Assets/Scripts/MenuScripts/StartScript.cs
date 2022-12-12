using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private void Start()
    {
        // Set the initial player attribute values
        PlayerAttributes.strength = 5;
        PlayerAttributes.agility = 5;
        PlayerAttributes.dexterity = 5;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
