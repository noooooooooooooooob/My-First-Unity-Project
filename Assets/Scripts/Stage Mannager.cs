using System;
using UnityEngine;

public class StageMannager : MonoBehaviour
{
    public CalculateCards calculateCards;
    public GameObject Enemy;
    public EnemyHpContoller enemyHpContoller;

    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        enemyHpContoller = Enemy.GetComponent<EnemyHpContoller>();
    }
    public void StartTurn()
    {
        // 모든 PlayerCard 찾기
        PlayerCard[] allCards = FindObjectsOfType<PlayerCard>();
        CardZone[] allCardZones = FindObjectsOfType<CardZone>();
        
        foreach (CardZone cardzone in allCardZones)
        {
            Debug.Log("Best Rank : " + calculateCards.bestRank);
            if(cardzone.type == Type.DIAMOND)
            {
                enemyHpContoller.GetDamage(cardzone.cardValue * calculateCards.bestRank);
                Debug.Log(cardzone.cardValue * calculateCards.bestRank + " Damaged");
            }
            else if(cardzone.type == Type.CLOVER)
            {
                Debug.Log("CLOVER " + cardzone.cardValue);
            }
            else if(cardzone.type == Type.SPADE)
            {
                Debug.Log("SPADE " + cardzone.cardValue);
            }
        }
        foreach (PlayerCard card in allCards)
        {
            card.OnReset();
            card.ReDraw();
        }
        calculateCards.bestRank = 1;
        calculateCards.bestHand = "High Card";
        calculateCards.CalculateAllCards();
    }
}
