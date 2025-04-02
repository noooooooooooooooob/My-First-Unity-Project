using System.Linq;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject[] cards;
    private Sprite[] sprites;
    public Card[] card;

    void Start()
    {
        card = new Card[cards.Count()];
        sprites = Resources.LoadAll<Sprite>("Sprites/kenney_playing-cards-pack/PNG/Cards (large)");
        
        foreach (GameObject card in cards)
        {
            
        }
    }
}

