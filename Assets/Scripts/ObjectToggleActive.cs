using UnityEngine;

public class ObjectToggleActive : MonoBehaviour
{
    public bool isActiveFalse;
    void Start()
    {   
        if(isActiveFalse)
        {
            gameObject.SetActive(false);
        }
    }
    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    public void AcitveFalse()
    {
        gameObject.SetActive(false);
    }
    public void AcitveTrue()
    {
        gameObject.SetActive(true);
    }
}
