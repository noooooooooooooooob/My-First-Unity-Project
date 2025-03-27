using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHpContoller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Text; // HP 표시를 위한 Text
    public GameObject gameClearUI;
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
    	// HP 값을 초기 세팅합니다.
        _hp = 100;

        // MaxValue를 세팅하는 함수입니다.
        SetMaxHealth(_hp);

        Text.text = "100 / 100";
    }
    public void SetMaxHealth(int health)
    {
        Text.text = _hp + " / " + _maxHp;
    }
    public void GetDamage(int damage)
    {
        Hp -= damage;
        Text.text = _hp + " / " + _maxHp;
        if(Hp==0){
            EnemyDie();
        }
    }
    public void Heal(int heal)
    {
        if(Hp == 0) return;
        Hp += heal;
        Text.text = _hp + " / " + _maxHp;
    }
    public void EnemyDie(){
        gameClearUI.SetActive(true);
        Destroy(gameObject);
    }
}
