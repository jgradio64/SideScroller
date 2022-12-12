using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInteract : MonoBehaviour
{
    private bool OnChest;
    private Animator animator;
    [SerializeField] private int NumCoins;

    // Start is called before the first frame update
    void Start()
    {
        OnChest = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s") && OnChest)
        {
            animator.SetTrigger("Open");
            PlayerStats.Gold = PlayerStats.Gold + NumCoins;
            gameObject.GetComponent<OnInteract>().Interacted = true;
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnChest = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnChest = false;
    }
}
