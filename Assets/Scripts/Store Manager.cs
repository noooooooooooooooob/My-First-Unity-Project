using System.Linq;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject[] cards;
    private Sprite[] sprites;
    public cardtype[] cardtypes;

    void Start()
    {
        cardtypes = new cardtype[cards.Count()];
        sprites = Resources.LoadAll<Sprite>("Sprites/kenney_playing-cards-pack/PNG/Cards (large)");
        
        foreach (GameObject card in cards)
        {
            
        }
    }
}

public class cardtype
{
    public Type type;
    public int value;
    public cardtype(Type type, int value)
    {
        this.type = type;
        this.value = value;
    }
}
