using UnityEngine;

public class DeckManager : MonoBehaviour
{
    // 0: 하트, 1: 다이아몬드, 2: 클로버, 3: 스페이드
    public static int[][] deck = new int[4][];

    void Awake()
    {

        for (int i = 0; i < 4; i++)
        {
            deck[i] = new int[11]; // 카드 번호는 0~10까지
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 11; j++)
            {
                deck[i][j] = 1;
            }
        }
    }

    public void CardDrawed(Type type, int value)
    {
        for (int i = 0; i < 4; i++)
        {
            if (deck[i] == null)
            {
                Debug.Log("Deck is NULL!!");
                return;
            }
        }

        switch (type)
        {
            case Type.HEART:
                deck[0][value]--;
                break;
            case Type.DIAMOND:
                deck[1][value]--;
                break;
            case Type.CLOVER:
                deck[2][value]--;
                break;
            case Type.SPADE:
                deck[3][value]--;
                break;
        }
    }
}