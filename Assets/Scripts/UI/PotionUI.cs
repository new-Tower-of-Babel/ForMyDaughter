using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PotionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI potionUsedText; // "포션 사용!" 텍스트
    [SerializeField] private float displayDuration = 2f; // 텍스트 표시 시간

    private void Start()
    {
        // 텍스트 비활성화 상태로 시작
        potionUsedText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 키보드 1번 입력 감지
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowPotionUsedText();
        }
    }

    public void ShowPotionUsedText()
    {
        // 텍스트 활성화
        potionUsedText.gameObject.SetActive(true);
        CancelInvoke(nameof(HidePotionUsedText)); // 중복 호출 방지
        Invoke(nameof(HidePotionUsedText), displayDuration); // 일정 시간 후 비활성화
    }

    private void HidePotionUsedText()
    {
        // 텍스트 비활성화
        potionUsedText.gameObject.SetActive(false);
    }
}
