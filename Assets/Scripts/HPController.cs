using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HPController : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    public GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI Text; // HP 표시를 위한 Text
    // 플레이어의 HP
    private int _hp;
    private int _maxHp = 100; // HP의 최대값을 따로 설정

    public int Hp
    {
        get => _hp;
        private set => _hp = Mathf.Clamp(value, 0, _maxHp); // 최대 체력을 유지하도록 변경
    }
    private void Awake()
    {
    	// 플레이어의 HP 값을 초기 세팅합니다.
        _hp = 100;

        // MaxValue를 세팅하는 함수입니다.
        SetMaxHealth(_hp);

        Text.text = "100 / 100";
    }

    public void SetMaxHealth(int health)
    {
        hpBar.maxValue = health;
        hpBar.value = health;
        Text.text = _hp + " / " + _maxHp;
    }

	// 플레이어가 대미지를 받으면 대미지 값을 전달 받아 HP에 반영합니다.
    public void GetDamage(int damage)
    {
        if(damage < 0) return;
        Hp -= damage;
        hpBar.value = Hp;
        Text.text = _hp + " / " + _maxHp;
    }

    public void Heal(int heal)
    {
        if(Hp == 0) return;
        Hp += heal;
        hpBar.value = Hp;
        Text.text = _hp + " / " + _maxHp;
    }

    public void GameOver(){
        gameOverUI.SetActive(true);
    }
}
