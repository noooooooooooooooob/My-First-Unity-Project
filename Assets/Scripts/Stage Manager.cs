using System;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public CalculateCards calculateCards;
    public GameObject Enemy;
    public GameObject Player;
    public GameObject HPSkillController;
    public Player player;
    public EnemyHpContoller enemyHpContoller;
    public DeckManager deckManager;
    public HPController hPController;

    void Start()
    {
        Enemy = GameObject.Find("Enemy");
        Player = GameObject.Find("Player");
        HPSkillController = GameObject.Find("HPSkillController");
        enemyHpContoller = Enemy.GetComponent<EnemyHpContoller>();
        deckManager = GetComponent<DeckManager>();
        player = Player.GetComponent<Player>();
        hPController = HPSkillController.GetComponent<HPController>();
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
                enemyHpContoller.GetDamage(cardzone.cardValue * calculateCards.bestRank + player.buff - Enemy.GetComponent<Enemy>().shield);
                Debug.Log(cardzone.cardValue * calculateCards.bestRank + " Damaged");
            }
            else if (cardzone.type == Type.HEART)
            {
                hPController.Heal(cardzone.cardValue * calculateCards.bestRank + player.buff);
                Debug.Log(cardzone.cardValue * calculateCards.bestRank + " Healed");
            }
            // else if (cardzone.type == Type.CLOVER)
            // {
            //     player.setBuff
            //     (cardzone.cardValue * calculateCards.bestRank);
            //     Debug.Log("CLOVER " + cardzone.cardValue);
            // }
            // else if (cardzone.type == Type.SPADE)
            // {
            //     player.setDefence
            //     (cardzone.cardValue * calculateCards.bestRank);
            //     Debug.Log("SPADE " + cardzone.cardValue);
            // }
        }
        
        foreach (PlayerCard card in allCards)
        {
            card.destroyCard();
        }
        deckManager.DrawCard(6);
        calculateCards.bestRank = 1;
        calculateCards.bestHand = "High Card";
        Enemy.GetComponent<Enemy>().startTurn();
        player.endTurn();
        calculateCards.CalculateAllCards();
    }
}
