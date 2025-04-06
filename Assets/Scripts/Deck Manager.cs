using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public DeckData deckData;
    public GameObject cardPrefab;
    public Transform[] drawPosition;
    private List<PlayerCard> currentRoundCards = new List<PlayerCard>();
    int cnt = 0;
    public GameObject gameOverUI;
    public GameObject gameClearUI;

    void Awake()
    {
        if (deckData.playerDeck.Count == 0)
        {
            deckData.InitializeDeck();
        }
    }

    public void DrawCard(int count)
    {
        currentRoundCards.Clear(); // 현재 라운드 카드 초기화
        if (deckData.playerDeck.Count == 0)
        {
            Debug.Log("Deck is empty!");
            if(gameClearUI.activeSelf == false)
                gameOverUI.SetActive(true);
            return;
        }

        while (count-- > 0)
        {
            if (deckData.playerDeck.Count == 0)
            {
                Debug.Log("No more cards left in the deck!");
                break;
            }

            Card drawnCard = deckData.playerDeck[0];
            deckData.playerDeck.RemoveAt(0);

            GameObject newCardObj = Instantiate(cardPrefab, drawPosition[cnt++].position, Quaternion.identity);
            PlayerCard playerCard = newCardObj.GetComponent<PlayerCard>();
            playerCard.Initialize(drawnCard.type, drawnCard.value);
            currentRoundCards.Add(playerCard); 
            Debug.Log($"🃏 카드 뽑음: {drawnCard.type} {drawnCard.value}");
            Debug.Log($"현재 라운드 카드 개수: {currentRoundCards.Count}");
        }

        cnt = 0;
    }

    public List<PlayerCard> GetCurrentRoundCards()
    {
        return new List<PlayerCard>(currentRoundCards);
    }

    public void ResetRound()
    {
        foreach (var card in currentRoundCards)
        {
            Destroy(card.gameObject);
        }
        currentRoundCards.Clear();
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
