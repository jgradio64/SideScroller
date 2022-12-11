using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10;
    [SerializeField] private float detectionSpeed;
    [SerializeField] LayerMask playerLayerMask;
    public bool PlayerVisible { get; private set; }


    void Start()
    {
        PlayerVisible = false;
        SetDetector();
    }
    
    void Update()
    {
        if (PlayerVisible)
        {
            transform.parent.gameObject.GetComponent<Enemy>().PlayerDetected = true;
        } 
        else
        {
            transform.parent.gameObject.GetComponent<Enemy>().PlayerDetected = false;
        }
    }

    void SetDetector()
    {
        gameObject.transform.localScale = new Vector3(detectionRange, 1f, 1f);
        float detectionSize = detectionRange/2f - .5f;
        gameObject.transform.localPosition = new Vector3(detectionSize, 0f, 0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            PlayerVisible = true;
        }        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            PlayerVisible = false;
        }
    }
}
