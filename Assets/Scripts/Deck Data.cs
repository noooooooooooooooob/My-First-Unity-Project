using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObjects/DeckData")]
public class DeckData : ScriptableObject
{
    public List<Card> playerDeck = new List<Card>();

    public void InitializeDeck()
    {
        playerDeck.Clear();
        for (int suit = 0; suit < 4; suit++)
        {
            for (int value = 1; value <= 10; value++)
            {
                playerDeck.Add(new Card((Type)suit, value));
            }
        }
        ShuffleDeck();
    }

    public void ShuffleDeck()
    {
        for (int i = 0; i < playerDeck.Count; i++)
        {
            Card temp = playerDeck[i];
            int randomIndex = Random.Range(i, playerDeck.Count);
            playerDeck[i] = playerDeck[randomIndex];
            playerDeck[randomIndex] = temp;
        }
    }
    public void AddCard(Card newCard)
    {
        playerDeck.Add(newCard);
    }

    public void RemoveCard(Card cardToRemove)
    {
        playerDeck.Remove(cardToRemove);
    }
}