using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage1_Puzzle1_GoalBoxCheck : MonoBehaviour
{
    public bool goalCheck = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        goalCheck = true;
        Debug.Log(goalCheck);
    }
}
