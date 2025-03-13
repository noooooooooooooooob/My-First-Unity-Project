using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public SkillManager skillManager;
    public GameObject HPSkillController;
    public HPController hPController;
    public int buff = 0;
    public int defence = 0;
    public TextMeshProUGUI buffText;
    public TextMeshProUGUI defenceText;

    void Start(){
        HPSkillController = GameObject.Find("HPSkillController");
        hPController = HPSkillController.GetComponent<HPController>();
        skillManager = HPSkillController.GetComponent<SkillManager>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Damage");
            hPController.GetDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Heal");
            hPController.Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Add Point");
            skillManager.addPoint(1);
        }
        if(hPController.Hp==0){
            hPController.GameOver();
        }   
    }
    public void setBuff(int value)
    {
        buff = value;
        buffText.text = "Buff : " + buff;
    }
    public void setDefence(int value)
    {
        defence = value;
        defenceText.text = "Defence : " + defence;
    }
}
