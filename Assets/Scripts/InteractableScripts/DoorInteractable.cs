using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorInteractable : MonoBehaviour
{
    private bool OnTravel;
    [SerializeField] private SceneAsset NextScene;

    // Start is called before the first frame update
    void Start()
    {
        OnTravel = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s") && OnTravel)
        {
            // Load the next Scene
            SceneManager.LoadScene(NextScene.name);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnTravel = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTravel = false;
    }
}
