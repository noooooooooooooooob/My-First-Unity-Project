using UnityEngine;

public class CardZone : MonoBehaviour
{
    public Type type;
    public int cardValue;
    public bool getType(Type cardType)
    {
        if (type == Type.DIAMOND && cardType == Type.HEART)
        {
            type = Type.HEART;
            return true;
        }
        if (type == Type.HEART && cardType == Type.DIAMOND)
        {
            type = Type.DIAMOND;
            return true;
        }
        return type == cardType;
    }
    public void OnReset()
    {
        cardValue = 0;
    }
}
