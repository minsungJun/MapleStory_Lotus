using UnityEngine;
using UnityEngine.UI;

public class CountDisplay : MonoBehaviour
{
    // 0 ~ 9 숫자 이미지 배열
    public Sprite[] numberSprites;
    PlayerStat stat;

    // 숫자를 표시할 UI 이미지
    public Image numberImage;

    void Awake()
    {
        stat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    void Update()
    {
        DisplayNumber(stat.deathcount);
    }

    // 숫자를 표시하는 함수
    public void DisplayNumber(int number)
    {
        // 숫자를 범위 내에서 확인 (0 ~ 9 범위에 맞게 조정)
        if (number >= 0 && number <= 9)
        {
            numberImage.sprite = numberSprites[number];
        }
        else
        {
            Debug.LogError("Invalid number: " + number);
        }
    }
}
