using UnityEngine;

public class HpTestScript : HPController
{
    public SkillManager skillManager;

    void Start(){
        skillManager = GetComponent<SkillManager>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Damage");
            GetDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Heal");
            Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Add Point");
            skillManager.addPoint(1);
        }
        if(Hp==0){
            GameOver();
        }   
    }
}
