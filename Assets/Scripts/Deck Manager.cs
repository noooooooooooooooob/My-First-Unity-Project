using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    private List<Card> deck = new List<Card>();

    public GameObject cardPrefab;
    public Transform[] drawPosition;
    int cnt = 0;

    void Awake()
    {
        InitializeDeck();
        ShuffleDeck();
    }

    void InitializeDeck()
    {
        deck.Clear();
        for (int suit = 0; suit < 4; suit++)
        {
            for (int value = 1; value <= 10; value++) // 1~10까지만 사용
            {
                deck.Add(new Card((Type)suit, value));
            }
        }
    }

    void ShuffleDeck()
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void DrawCard(int count)
    {
        if (deck.Count == 0)
        {
            Debug.Log("Deck is empty!");
            return;
        }
        while (count-->0)
        {
            Card drawnCard = deck[0];
            deck.RemoveAt(0);

            GameObject newCardObj = Instantiate(cardPrefab, drawPosition[cnt++].position, Quaternion.identity);
            PlayerCard playerCard = newCardObj.GetComponent<PlayerCard>();
            playerCard.Initialize(drawnCard.type, drawnCard.value);
        }
        cnt=0;
    }
}

public class Card
{
    public Type type;
    public int value;

    public Card(Type type, int value)
    {
        this.type = type;
        this.value = value;
    }
}
