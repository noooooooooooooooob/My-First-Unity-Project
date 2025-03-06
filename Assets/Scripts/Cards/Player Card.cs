using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    public Type type;
    public int value;
    void Awake()
    {
        type = Type.CLOVER;
        value = 1;
    }

    public void pointerEnter()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += 0.1f;
        transform.position = newPosition;
        Debug.Log("pointer in");
    }
    public void pointOut()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.1f;
        transform.position = newPosition;
        Debug.Log("pointer out");
    }
}
