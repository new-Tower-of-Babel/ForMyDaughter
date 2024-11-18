using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage1_Puzzle1_rotSquare : MonoBehaviour
{
    private bool isHolding = false;
    public float holdInterval = 0.1f; // 동작 실행 간격
    public GameObject target;
    public float rotSpeed;
    private void FixedUpdate()
    {
        if (isHolding)
        {
            target.transform.Rotate(new Vector3(0,0,rotSpeed*Time.deltaTime));
        }
    }
    
    public void OnPointerLeftDown()
    {
        isHolding = true;
        rotSpeed *= -1;
    }

    public void OnPointerLeftUp()
    {
        isHolding = false;
        rotSpeed *= -1;
    }
    public void OnPointerRightDown()
    {
        isHolding = true;
    }

    public void OnPointerRightUp()
    {
        isHolding = false;
    }
}
