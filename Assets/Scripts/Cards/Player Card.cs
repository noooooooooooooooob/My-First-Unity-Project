using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCard : MonoBehaviour
{
    [Header("-----------------Card Type")]
    public Type type;
    public int value;
    private Vector3 defaultPos;
    private Vector3 offset;
    private Camera cam;
    [SerializeField] private bool isInTargetZone;
    private Vector3 targetPos;
    private int defaultTargetValue;
    private Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    public GameObject stageManager;
    public GameObject Player;
    public GameObject calculateCards;

    void Awake()
    {
        cam = Camera.main; // 메인 카메라 참조
        defaultPos = transform.position;
        stageManager = GameObject.Find("Stage Manager");
        Player = GameObject.Find("Player");
    }
    void OnEnable()
    {
        // 스프라이트 로드
        sprites = Resources.LoadAll<Sprite>("Sprites/kenney_playing-cards-pack/PNG/Cards (large)");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Type cardType, int cardValue)
    {
        type = cardType;
        value = cardValue;

        // 스프라이트 로드
        if (sprites == null || sprites.Length == 0)
        {
            sprites = Resources.LoadAll<Sprite>("Sprites/kenney_playing-cards-pack/PNG/Cards (large)");
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        int spriteIndex = GetSpriteIndex(type, value);

        if (spriteIndex >= 0 && spriteIndex < sprites.Length)
        {
            spriteRenderer.sprite = sprites[spriteIndex];
        }
        else
        {
            Debug.LogError("잘못된 스프라이트 인덱스: " + spriteIndex);
        }
    }

    private int GetSpriteIndex(Type cardType, int cardValue)
    {
        int baseIndex = 0;
        switch (cardType)
        {
            case Type.CLOVER: baseIndex = 0; break;
            case Type.DIAMOND: baseIndex = 10; break;
            case Type.HEART: baseIndex = 20; break;
            case Type.SPADE: baseIndex = 30; break;
        }
        return baseIndex + (cardValue - 1);
    }



    void OnMouseDown()
    {
        offset = transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }

    void OnMouseUp()
    {
        if (isInTargetZone)
        {
            ClearTargetZone();
            transform.position = targetPos;
            if (type == Type.CLOVER)
            {
                Player.GetComponent<Player>().setBuff(value * stageManager.GetComponent<CalculateCards>().bestRank);
                if(Player.GetComponent<Player>().defence != 0)
                    Player.GetComponent<Player>().setDefence(Player.GetComponent<Player>().defence * stageManager.GetComponent<CalculateCards>().bestRank + Player.GetComponent<Player>().buff);
            }
            if (type == Type.SPADE)
            {
                Player.GetComponent<Player>().setDefence(value * stageManager.GetComponent<CalculateCards>().bestRank + Player.GetComponent<Player>().buff);
            }
        }
        else
        {
            transform.position = defaultPos;
            if (type == Type.CLOVER)
            {
                Player.GetComponent<Player>().setDefence(Player.GetComponent<Player>().defence / Player.GetComponent<Player>().buff);
                Player.GetComponent<Player>().setBuff(0);
            }
            if (type == Type.SPADE)
            {
                Player.GetComponent<Player>().setDefence(0);
            }
        }
        
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = 0f;
        return cam.ScreenToWorldPoint(mousePoint);
    }
    // 특정 영역에 들어왔는지 감지하는 메서드
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CardZone cardZone = collision.GetComponent<CardZone>();
        if (collision.tag == "TargetZone" && cardZone.getType(type)) // 특정 태그를 가진 오브젝트와 충돌 시
        {
            isInTargetZone = true;
            targetPos = collision.transform.position;
            defaultTargetValue = cardZone.cardValue;
            cardZone.cardValue = value;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CardZone cardZone = collision.GetComponent<CardZone>();
        if (collision.tag == "TargetZone" && cardZone.getType(type))
        {
            isInTargetZone = false;

            // **만약 TargetZone에 남아 있는 다른 카드가 없다면 값 복구**
            Collider2D[] colliders = Physics2D.OverlapCircleAll(collision.transform.position, 0.1f);
            bool otherCardExists = false;

            foreach (Collider2D col in colliders)
            {
                PlayerCard existingCard = col.GetComponent<PlayerCard>();
                if (existingCard != null && existingCard != this)
                {
                    otherCardExists = true;
                    break;
                }
            }

            if (!otherCardExists)
            {
                cardZone.cardValue = defaultTargetValue;
            }
        }
    }
    void ClearTargetZone()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPos, 0.1f); // targetZone 근처에 있는 오브젝트 찾기
        foreach (Collider2D col in colliders)
        {
            PlayerCard existingCard = col.GetComponent<PlayerCard>();
            if (existingCard != null && existingCard != this)
            {
                existingCard.transform.position = existingCard.defaultPos; // 기존 카드 원래 자리로 복귀
            }
        }
    }

    public void OnReset()
    {
        transform.position = defaultPos;
    }
    public void destroyCard()
    {
        Destroy(gameObject);
    }
}
