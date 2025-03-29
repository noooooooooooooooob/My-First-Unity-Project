using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CalculateCards : MonoBehaviour
{
    Dictionary<Type, List<int>> cardTypeNumbers = new Dictionary<Type, List<int>>();
    private List<PlayerCard> currentRoundCards = new List<PlayerCard>(); // 현재 라운드 카드, 오류제거용
    // 족보 우선순위 설정
    public string bestHand = "High Card"; // 기본값
    public int bestRank = 1; // 기본값 (가장 낮은 족보)
    // 테스트 플레이어
    public GameObject player;
    public DeckManager deckManager;

    private void ResetCalculate()
    {
        cardTypeNumbers.Clear();
        currentRoundCards.Clear();
    }

    void Start() // Test
    {
        CalculateAllCards();

        // 테스트 플레이어
        player = GameObject.Find("Player");

    }
    void Update()
    {
        // 테스트 플레이어
        player.GetComponent<Player>().setMultiply(bestRank);
    }


    public int CalculateAllCards() // 족보에 따른 배수를 반환
    {
        List<PlayerCard> currentRoundCards = deckManager.GetCurrentRoundCards(); // 현재 라운드 카드만 가져옴

        if (currentRoundCards.Count < 1)
        {
            Debug.Log("카드가 없습니다.");
            return 0;
        }

        Dictionary<Type, List<int>> cardTypeNumbers = new Dictionary<Type, List<int>>();

        foreach (PlayerCard card in currentRoundCards)
        {
            if (!cardTypeNumbers.ContainsKey(card.type))
            {
                cardTypeNumbers[card.type] = new List<int>();
            }
            cardTypeNumbers[card.type].Add(card.value);
        }

        string bestHand = FindBestHand(currentRoundCards);
        Debug.Log($"최고의 족보: {bestHand} /  BEST RANK: {bestRank}");

        return bestRank;
    }

    private string FindBestHand(List<PlayerCard> hand)
    {
        int cardCount = hand.Count;
        if (cardCount == 0) return "No Cards";

        // 5장 이상의 경우, 5장 조합을 만들어 가장 좋은 핸드를 찾음
        if (cardCount >= 5)
        {
            var allCombinations = GetCombinations(hand, 5);
            return allCombinations.Select(EvaluateHand).Max();
        }

        // 5장 미만이면 해당 개수로만 판별
        return EvaluateHand(hand);
    }

    private List<List<PlayerCard>> GetCombinations(List<PlayerCard> cards, int combinationSize)
    {
        List<List<PlayerCard>> result = new List<List<PlayerCard>>();
        GetCombinationsRecursive(cards, new List<PlayerCard>(), 0, combinationSize, result);
        return result;
    }

    private void GetCombinationsRecursive(List<PlayerCard> cards, List<PlayerCard> tempList, int start, int size, List<List<PlayerCard>> result)
    {
        if (tempList.Count == size)
        {
            result.Add(new List<PlayerCard>(tempList));
            return;
        }

        for (int i = start; i < cards.Count; i++)
        {
            tempList.Add(cards[i]);
            GetCombinationsRecursive(cards, tempList, i + 1, size, result);
            tempList.RemoveAt(tempList.Count - 1);
        }
    }

    private string EvaluateHand(List<PlayerCard> hand)
    {
        Dictionary<int, int> valueCounts = hand.GroupBy(card => card.value)
                                    .ToDictionary(group => group.Key, group => group.Count());

        Dictionary<Type, int> suitCounts = hand.GroupBy(card => card.type)
                                        .ToDictionary(group => group.Key, group => group.Count());

        var sortedValues = hand.Select(card => card.value).OrderBy(v => v).ToList();

        bool isFlush = suitCounts.Values.Any(count => count == hand.Count);
        bool isStraight = sortedValues.Zip(sortedValues.Skip(1), (a, b) => b - a).All(diff => diff == 1);
        bool hasFourOfAKind = valueCounts.Values.Contains(4);
        bool hasThreeOfAKind = valueCounts.Values.Contains(3);
        bool hasTwoPair = valueCounts.Values.Count(v => v == 2) == 2;
        bool hasOnePair = valueCounts.Values.Contains(2);
        bool isFullHouse = hasThreeOfAKind && hasOnePair;




        void UpdateBestHand(string handName, int rank)
        {
            if (rank > bestRank)  // 현재보다 높은 족보라면 업데이트
            {
                Debug.Log($"🛠 bestHand 업데이트: {bestHand} → {handName} (기존 rank: {bestRank}, 새로운 rank: {rank})");
                bestHand = handName;
                bestRank = rank;
            }
        }

        // 높은 순서대로 체크
        if (isFlush && isStraight) UpdateBestHand("Straight Flush", 9);
        if (hasFourOfAKind) UpdateBestHand("Four of a Kind", 8);
        if (isFullHouse) UpdateBestHand("Full House", 7);
        if (isFlush) UpdateBestHand("Flush", 6);
        if (isStraight) UpdateBestHand("Straight", 5);
        if (hasThreeOfAKind) UpdateBestHand("Three of a Kind", 4);
        if (hasTwoPair) UpdateBestHand("Two Pair", 3);
        if (hasOnePair) UpdateBestHand("One Pair", 2);

        return bestHand;
    }
}
