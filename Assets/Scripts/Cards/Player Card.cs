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
    void Awake()
    {
    }

    void Start()
    {
        cam = Camera.main; // 메인 카메라 참조
        defaultPos = transform.position;
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
        if(isInTargetZone)
        {
            ClearTargetZone();
            transform.position = targetPos;
        }
        else transform.position = defaultPos;
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
        if (collision.tag== "TargetZone" && collision.GetComponent<CardZone>().getType() == type) // 특정 태그를 가진 오브젝트와 충돌 시
        {
            isInTargetZone = true;
            targetPos = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag== "TargetZone" && collision.GetComponent<CardZone>().getType() == type)
        {
            isInTargetZone = false;
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
}
