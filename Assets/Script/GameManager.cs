using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    Player player;

    // �÷��̾�
    public Vector3 pos;
    public float hp;

    // ��
    // isAlived�� true�� �Ǿ��� �� Destroy��Ű�� ��ũ��Ʈ�� TestNav�� Start�� ����
    // public List<bool> isAlived = new List<bool>();
    public int aliveCount;

    // �κ��丮(�Ѿ�, ���Ѿ�, ���� ����)
    public int nB;
    public int bPB;
    public int healKits;
    // ���� �Ѿ� ���Ѿ� ����
    public int leftNB;
    public int leftPB;

    private void Awake()
    {
        instance = this;
    }
}
