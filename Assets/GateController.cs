using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{   
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetBool("character_nearby", true);
        }
    }
}
