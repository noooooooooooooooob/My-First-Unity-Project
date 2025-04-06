using UnityEngine;

public class StoreCard : MonoBehaviour
{
    [SerializeField]
    int buttonidx;
    public GameObject deckManager;
    public DeckData deckData;
    public StoreManager storeManager;

    void Start()
    {
        storeManager = GameObject.Find("Store Manager").GetComponent<StoreManager>();
        deckManager = GameObject.Find("Deck Manager");
    }
    public void onClick()
    {
        Card card = storeManager.GetCard(buttonidx);
        if(card != null)
            deckData.AddCard(card);
        
        Destroy(gameObject);
    }
}
