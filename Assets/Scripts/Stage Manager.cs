using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public CalculateCards calculateCards;
    public GameObject Enemy;
    public GameObject Player;
    public Player player;
    public EnemyHpContoller enemyHpContoller;
    public DeckManager deckManager;

    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        Player = GameObject.Find("Player");
        enemyHpContoller = Enemy.GetComponent<EnemyHpContoller>();
        deckManager = GetComponent<DeckManager>();
        player = Player.GetComponent<Player>();
        deckManager.DrawCard(6);
    }
    public void StartTurn() // Click START BUTTON
    {
        // 모든 PlayerCard 찾기
        PlayerCard[] allCards = FindObjectsOfType<PlayerCard>();
        CardZone[] allCardZones = FindObjectsOfType<CardZone>();

        foreach (CardZone cardzone in allCardZones)
        {
            Debug.Log("Best Rank : " + calculateCards.bestRank);
            if (cardzone.type == Type.DIAMOND)
            {
                enemyHpContoller.GetDamage(cardzone.cardValue * calculateCards.bestRank);
                Debug.Log(cardzone.cardValue * calculateCards.bestRank + " Damaged");
            }
            else if (cardzone.type == Type.CLOVER)
            {
                player.setBuff
                (cardzone.cardValue * calculateCards.bestRank);
                Debug.Log("CLOVER " + cardzone.cardValue);
            }
            else if (cardzone.type == Type.SPADE)
            {
                player.setDefence
                (cardzone.cardValue * calculateCards.bestRank);
                Debug.Log("SPADE " + cardzone.cardValue);
            }
        }
        
        foreach (PlayerCard card in allCards)
        {
            card.destroyCard();
        }
        deckManager.DrawCard(6);
        calculateCards.bestRank = 1;
        calculateCards.bestHand = "High Card";
        calculateCards.CalculateAllCards();
    }
}
