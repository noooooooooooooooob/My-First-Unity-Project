using System.Linq;
using UnityEngine.UI;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public Button[] cardButtons;
    private Sprite[] sprites;
    public Card[] card;
    public DeckData deckData;
    int cardButtonsSize;
    void Start()
    {
        cardButtonsSize= cardButtons.Count();
        card = new Card[cardButtonsSize];
        sprites = Resources.LoadAll<Sprite>("Sprites/kenney_playing-cards-pack/PNG/Cards (large)");

        int idx = 0;
        foreach (var c in cardButtons)
        {
            card[idx] = new Card((Type)Random.Range(0, 4), Random.Range(1, 10));
            Card curcard = card[idx]; // 참조 가져오기

            int spriteIDX = GetSpriteIndex(curcard.type, curcard.value);
            c.image.sprite = sprites[spriteIDX];

            idx++; // 인덱스 증가
        }
        idx = 0;
    }

    private int GetSpriteIndex(Type cardType, int cardValue)
    {
        int baseIndex = 0;
        switch (cardType)
        {
            case Type.CLOVER: baseIndex = 0; break;
            case Type.DIAMOND: baseIndex = 10; break;
            case Type.HEART: baseIndex = 20; break;
            case Type.SPADE: baseIndex = 30; break;
        }
        return baseIndex + (cardValue - 1);
    }

    public Card GetCard(int idx)
    {
        if (idx < 0 || idx >= cardButtonsSize)
        {
            Debug.LogError("Index out of range: " + idx);
            return null;
        }
        return card[idx];
    }
}

