using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour
{
    // 0 ~ 9 숫자 이미지 배열 (0.png ~ 9.png)
    public Sprite[] numberSprites;
    PlayerStat stat;
    

    // 두 자리 숫자를 표시할 두 개의 UI 이미지
    public Image tens_minute_Image;  // 십의 자리
    public Image ones_minute_Image;  // 일의 자리
    public Image tens_second_Image;  // 십의 자리
    public Image ones_second_Image;  // 일의 자리
    
    float checktime = 0f;
    void Awake()
    {
        stat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }
    void Update()
    {
        checktime += Time.deltaTime;
        if(checktime >= 1)
        {
            checktime = 0f;
            stat.time--;
            DisplayNumber(stat.time);
        }
    }

    // 숫자를 표시하는 함수 (두 자리 숫자 처리)
    public void DisplayNumber(int number)
    {
        // 십의 자리와 일의 자리 계산
        int tens_minute = number / 600;
        int ones_minute = (number % 600) / 60;
        int tens_second = ((number % 600) % 60) / 10;
        int ones_second = ((number % 600) % 60) % 10;


        // 십의 자리 이미지 설정
        tens_minute_Image.sprite = numberSprites[tens_minute];

        ones_minute_Image.sprite = numberSprites[ones_minute];

        // 일의 자리 이미지 설정
        tens_second_Image.sprite = numberSprites[tens_second];

        ones_second_Image.sprite = numberSprites[ones_second];

    }
}
