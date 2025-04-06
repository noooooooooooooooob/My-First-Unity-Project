using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject hPSkillController;
    public GameObject Player;
    public HPController hpController;
    private Sprite[] sprites;
    public GameObject[] Cards;
    private Card SpadeCard;
    private Card CloverCard;
    private Card DiamondCard;
    public int buff = 0;
    public TextMeshProUGUI buffText;
    public int shield = 0;
    public TextMeshProUGUI shieldText;
    public int damage = 0;
    public TextMeshProUGUI damageText;

    void Start()
    {
        Player = GameObject.Find("Player");
        hPSkillController = GameObject.Find("HPSkillController");
        hpController = hPSkillController.GetComponent<HPController>();
        sprites = Resources.LoadAll<Sprite>("Sprites/kenney_playing-cards-pack/PNG/Cards (large)");
        setCards();
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
    public void setCards()
    {
        int value = Random.Range(1, 10);
        CloverCard = new Card(Type.CLOVER, value);
        Cards[1].GetComponent<SpriteRenderer>().sprite = sprites[GetSpriteIndex(Type.CLOVER, value)];
        buff=value;

        value = Random.Range(1, 10);
        SpadeCard = new Card(Type.SPADE, value);
        Cards[2].GetComponent<SpriteRenderer>().sprite = sprites[GetSpriteIndex(Type.SPADE, value)];
        shield = value + buff;

        value = Random.Range(1, 10);
        DiamondCard = new Card(Type.DIAMOND, value);
        Cards[0].GetComponent<SpriteRenderer>().sprite = sprites[GetSpriteIndex(Type.DIAMOND, value)];
        damage = value + buff;

        buffText.text = "Buff : " + buff.ToString();
        shieldText.text = "Shield : " + shield.ToString();
        damageText.text = "Damage : " + damage.ToString();
    }
    public void startTurn()
    {
        hpController.GetDamage(DiamondCard.value + buff - Player.GetComponent<Player>().defence);
        shield = SpadeCard.value + buff;
        buff = CloverCard.value + buff;
        setCards();
    }
}