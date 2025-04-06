using UnityEngine;

public class ResetCards : MonoBehaviour
{
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
    }
    public void ResetAllCards()
    {
        // 모든 PlayerCard 찾기
        PlayerCard[] allCards = FindObjectsOfType<PlayerCard>();
        CardZone[] allCardZones = FindObjectsOfType<CardZone>();

        foreach (PlayerCard card in allCards)
            card.OnReset(); // 모든 카드의 초기화 함수 호출
        
        foreach (CardZone cardzone in allCardZones)
            cardzone.OnReset(); // 모든 카드의 초기화 함수 호출
        Player.GetComponent<Player>().endTurn(); // 플레이어의 버프 초기화
    }
}
