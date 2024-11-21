using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats; // 플레이어 스텟 참조
    [SerializeField] private Slider hpSlider;         // HP 슬라이더

    private void Start()
    {
        // 초기 HP 슬라이더 설정
        if (playerStats != null && hpSlider != null)
        {
            hpSlider.maxValue = playerStats.MaxHealth;
            hpSlider.value = playerStats.CurrentHealth;
        }
    }

    private void Update()
    {
        // HP UI를 플레이어의 현재 체력으로 업데이트
        if (playerStats != null && hpSlider != null)
        {
            hpSlider.value = playerStats.CurrentHealth;
        }
    }
}
