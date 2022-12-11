using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private float detectionRange;
    [SerializeField] private float detectionSpeed;
    [SerializeField] LayerMask playerLayerMask;
    public bool PlayerVisible { get; private set; }
    private Transform PlayerTransform = null;
    private BoxCollider2D coll;


    private void DetectPlayer()
    {
        
    }
}
