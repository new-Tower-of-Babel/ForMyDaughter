using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage1_BackGround : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private Vector3 cameraStartPosition;
    private float distance;

    private Material[] materials;
    private float[] layerMoveSpeed;

    [SerializeField] [Range(0.01f, 1.0f)] private float parallaxSpeed;

    private void Awake()
    {
        cameraStartPosition = cameraTransform.position;
        int backGroundCount = transform.childCount;
        GameObject[] backGrounds = new GameObject[backGroundCount];

        materials = new Material[backGroundCount];
        layerMoveSpeed = new float[backGroundCount];

        for (int i = 0; i < backGroundCount; i++)
        {
            backGrounds[i] = transform.GetChild(i).gameObject;
            Renderer renderer = backGrounds[i].GetComponent<Renderer>();

            if (renderer != null)
            {
                materials[i] = renderer.material;
            }
            else
            {
                Debug.LogError($"배경 오브젝트 '{backGrounds[i].name}'에 Renderer가 없습니다.");
            }
        }
        CalculateMoveSpeedByLayer(backGrounds, backGroundCount);
    }

    private void CalculateMoveSpeedByLayer(GameObject[] backgrounds, int count)
    {
        float farthestBackDistance = 0;
        for (int i = 0; i < count; i++)
        {
            float distanceZ = backgrounds[i].transform.position.z - cameraTransform.position.z;
            if (distanceZ > farthestBackDistance)
            {
                farthestBackDistance = distanceZ;
            }
        }

        for (int i = 0; i < count; i++)
        {
            float distanceZ = backgrounds[i].transform.position.z - cameraTransform.position.z;
            layerMoveSpeed[i] = 1 - (distanceZ / farthestBackDistance);
        }
    }

    private void LateUpdate()
    {
        distance = cameraTransform.position.x - cameraStartPosition.x;
        transform.position = new Vector3(cameraTransform.position.x, transform.position.y, 0);
        for (int i = 0; i < materials.Length; i++)
        {
            float speed = layerMoveSpeed[i] * parallaxSpeed;
            materials[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
