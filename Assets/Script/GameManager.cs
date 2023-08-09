using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Player player;

    // 플레이어
    public Vector3 pos;
    public float hp;

    // 적
    // isAlived가 true가 되었을 때 Destroy시키는 스크립트르 TestNav의 Start에 생성
    // public List<bool> isAlived = new List<bool>();
    public int aliveCount;

    // 인벤토리(총알, 권총알, 구급 상자)
    public int nB;
    public int bPB;
    public int healKits;
    // 남은 총알 권총알 구상
    public int leftNB;
    public int leftPB;

    private void Awake()
    {
        instance = this;
    }
}
