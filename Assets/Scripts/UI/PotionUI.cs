using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI potionUsedText; // "���� ���!" �ؽ�Ʈ
    [SerializeField] private float displayDuration = 2f; // �ؽ�Ʈ ǥ�� �ð�

    private void Start()
    {
        // �ؽ�Ʈ ��Ȱ��ȭ ���·� ����
        potionUsedText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Ű���� 1�� �Է� ����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowPotionUsedText();
        }
    }

    public void ShowPotionUsedText()
    {
        // �ؽ�Ʈ Ȱ��ȭ
        potionUsedText.gameObject.SetActive(true);
        CancelInvoke(nameof(HidePotionUsedText)); // �ߺ� ȣ�� ����
        Invoke(nameof(HidePotionUsedText), displayDuration); // ���� �ð� �� ��Ȱ��ȭ
    }

    private void HidePotionUsedText()
    {
        // �ؽ�Ʈ ��Ȱ��ȭ
        potionUsedText.gameObject.SetActive(false);
    }
}
