using UnityEngine;

public class CardZone : MonoBehaviour
{
    public Type type;
    public int cardValue;
    public Type getType(){
        return type;
    }
    public void OnReset()
    {
        cardValue = 0;
    }
}
