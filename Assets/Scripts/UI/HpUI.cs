using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats; // �÷��̾� ���� ����
    [SerializeField] private Slider hpSlider;         // HP �����̴�

    private void Start()
    {
        // �ʱ� HP �����̴� ����
        if (playerStats != null && hpSlider != null)
        {
            hpSlider.maxValue = playerStats.MaxHealth;
            hpSlider.value = playerStats.CurrentHealth;
        }
    }

    private void Update()
    {
        // HP UI�� �÷��̾��� ���� ü������ ������Ʈ
        if (playerStats != null && hpSlider != null)
        {
            hpSlider.value = playerStats.CurrentHealth;
        }
    }
}
