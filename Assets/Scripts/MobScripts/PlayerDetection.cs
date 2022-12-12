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
    private Enemy EnemyScript;

    void Start()
    {
        EnemyScript = transform.parent.gameObject.GetComponent<Enemy>();
        PlayerVisible = false;
        SetDetector();
    }

    void SetDetector()
    {
        gameObject.transform.localScale = new Vector3(detectionRange, 1f, 1f);
        float detectionSize = detectionRange/2f - .5f;
        gameObject.transform.localPosition = new Vector3(detectionSize, 0.75f, 0f);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            PlayerVisible = true;
            EnemyScript.PlayerDetected = true;
        }        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.CompareTag("Player")){
            PlayerVisible = false;
            EnemyScript.PlayerDetected = false;
        }
    }
}
