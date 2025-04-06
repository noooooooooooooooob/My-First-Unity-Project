using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckData", menuName = "ScriptableObjects/DeckData")]
public class DeckData : ScriptableObject
{
    public List<Card> playerDeck = new List<Card>(); // 신 바뀔 때마다 initialize
    public List<Card> Deck; // 현재 카드 덱

    void OnEnable() {
        makeNewDeck();
    }
    public void makeNewDeck()
    {
        if (Deck == null || Deck.Count == 0) { 
            Deck = new List<Card>();
            for (int suit = 0; suit < 4; suit++)
            {
                for (int value = 1; value <= 10; value++)
                {
                    Deck.Add(new Card((Type)suit, value));
                }
            }
            Debug.Log("✅ Deck 초기화 완료!");
        }
    }
    public void InitializeDeck()
    {
        playerDeck.Clear();
        foreach (var card in Deck)
        {
            playerDeck.Add(card);
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
        Deck.Add(newCard);
        Debug.Log("🃏 카드 추가: " + newCard.type + " " + newCard.value);
    }

    public void RemoveCard(Card cardToRemove)
    {
        Deck.Remove(cardToRemove);
    }
}