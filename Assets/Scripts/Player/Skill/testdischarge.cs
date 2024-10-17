using UnityEngine;

public class testdischarge : MonoBehaviour
{
    public Transform PlayerPosition;
    public Transform CenterPosition;
    public Transform EnemyPosition;
    public GameObject Discharge_Hit;
    public Vector2 pointA; // 시작점
    public Vector2 pointB; // 제어점
    public Vector2 pointC; // 끝점\
    public float height;

    public float speed = 1.0f; // 이동 속도
    private float t = 0.0f; // 곡선 진행 비율
    void Awake()
    {
        PlayerPosition = GameObject.Find("Player").transform;
        CenterPosition = GameObject.Find("CenterPosition").transform;
        EnemyPosition = GameObject.Find("Enemy").transform;
        

        pointA = new Vector2(PlayerPosition.position.x, PlayerPosition.position.y);
        pointB = new Vector2(CenterPosition.position.x, CenterPosition.position.y - height);
        pointC = new Vector2(EnemyPosition.position.x, EnemyPosition.position.y);

        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            pointB = new Vector2(CenterPosition.position.x, PlayerPosition.position.y - 2f + height);
        }
    }

    private void Update()
    {
        pointC = new Vector2(EnemyPosition.position.x, EnemyPosition.position.y);
        // t 값을 업데이트하여 곡선을 따라 이동
        t += Time.deltaTime * speed;
        
        // t가 1을 초과하면 리셋
        if (t > 1.0f)
        {
            t = 0.0f; // 또는 다른 로직으로 다시 시작
        }

        // 현재 위치 계산
        Vector2 currentPosition = CalculateBezierPoint(t, pointA, pointB, pointC);
        transform.position = currentPosition;

        // 진행 방향에 맞춰 회전
        Vector2 direction = CalculateBezierDerivative(t, pointA, pointB, pointC).normalized;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private Vector2 CalculateBezierPoint(float t, Vector2 pointA, Vector2 pointB, Vector2 pointC)
    {
        // 2차 베지어 곡선 공식
        Vector2 p0 = Vector2.Lerp(pointA, pointB, t);
        Vector2 p1 = Vector2.Lerp(pointB, pointC, t);
        return Vector2.Lerp(p0, p1, t);
    }
    private Vector2 CalculateBezierDerivative(float t, Vector2 pointA, Vector2 pointB, Vector2 pointC)
    {
        // 베지어 곡선의 미분 계산
        Vector2 p0 = Vector2.Lerp(pointA, pointB, t);
        Vector2 p1 = Vector2.Lerp(pointB, pointC, t);
        return p1 - p0; // 미분을 통해 방향을 계산
    }

    void Next()
    {
        Instantiate(Discharge_Hit, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D other)//무언가에 닿앗을때 호출
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")) // 닿은 오브젝트 이름이 Monster일때
        {

            Destroy(this.gameObject);
            Next();
        }
    }
}