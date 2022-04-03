using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : MonoBehaviour
{
    [SerializeField] GameObject _combatCanvas;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AIMovement aiMove = collision.collider.gameObject.GetComponent<AIMovement>();

        if(aiMove == null)
        {
            return;
        }

        Debug.Log("We Have Hit An AI");
        //Enter Combat
        _combatCanvas.SetActive(true);
        Time.timeScale = 0;//Pause Game
        //Time.timeScale = 1;//Unpause
    }
}
