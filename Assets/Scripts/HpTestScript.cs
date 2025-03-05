using UnityEngine;

public class HpTestScript : HPController
{
    
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
        if(Hp==0){
            GameOver();
        }   
    }
}
