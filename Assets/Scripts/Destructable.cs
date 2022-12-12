using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    // How many hits a destructable can take
    [SerializeField] private int ObjHealth = 2;
    private Collider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HitDesctructable()
    {
        ObjHealth -= 1;
        if (ObjHealth <= 0) DestroyObject();
    }

    void DestroyObject()
    {
        col.enabled = false;
        gameObject.SetActive(false);
    }
}
