//��δ���������ǵ���ҽ��볡��2ʱ�����������10ֻ��ͷ�ã�10ֻ��ը�ú�3ֻ��Ҫץס�������Ӳ��������ڵ������Ҵ�
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2 : MonoBehaviour
{
    public Transform player; //���
    public Vector3 targetPosition = new Vector3(38.95341f, 0.1199989f, 200.7748f); //stage2����ʼλ��
    public GameObject blueRabbitPrefab; //��ͷ��
    public GameObject redRabbitPrefab; //��ը��
    public GameObject trueRabbitPrefab; // ������Ԥ����
    public GameObject stage2Ground; //Stage2-ground ƽ��

    public int blueRabbitCount = 10; 
    public int redRabbitCount = 10; 
    public int trueRabbitCount = 3;  

    public float spawnRange = 10f; 
    public float moveSpeed = 10f;  //�ƶ��ٶ�
    private bool triggered = false; 

    void Update()
    {
        if (!triggered && Vector3.Distance(player.position, targetPosition) < 0.1f)
        {
            triggered = true;
            SpawnRabbits(blueRabbitPrefab, blueRabbitCount, "Blue rabbits");
            SpawnRabbits(redRabbitPrefab, redRabbitCount, "Red rabbits");
            SpawnRabbits(trueRabbitPrefab, trueRabbitCount, "True rabbits");
        }
    }

    void SpawnRabbits(GameObject prefab, int count, string tag)
    {
        for (int i = 0; i < count; i++)
        {
            //�������λ��
            Vector3 randomPosition = GetRandomPositionOnGround();

            //�������߰��������
            GameObject rabbit = Instantiate(prefab, randomPosition, Quaternion.identity);
            rabbit.tag = tag;

            //��������ƶ��ű����������
            Rigidbody rb = rabbit.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rabbit.AddComponent<RabbitRandomMove>().moveSpeed = moveSpeed;
        }
    }

    Vector3 GetRandomPositionOnGround()
    {
        //��ȡ�����λ�úͷ�Χ
        Vector3 groundPosition = stage2Ground.transform.position;
        Vector3 groundScale = stage2Ground.transform.localScale;

        //��������λ�÷�Χ
        float randomX = Random.Range(groundPosition.x - spawnRange, groundPosition.x + spawnRange);
        float randomZ = groundPosition.z;
        float randomY = groundPosition.y;

        return new Vector3(randomX, randomY, randomZ);
    }
}

public class RabbitRandomMove : MonoBehaviour
{
    public float moveSpeed = 10f; //���Ӷ��ƶ��ٶ�

    private Vector3 moveDirection;

    void Start()
    {
        //�����ʼ���ƶ�����
        moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
    }

    void FixedUpdate()
    {
        //�������ӵ�λ��
        transform.Translate(moveDirection * moveSpeed * Time.fixedDeltaTime, Space.World);

        //�����ײ�ͻ�һ������
        if (Physics.Raycast(transform.position, moveDirection, 0.5f))
        {
            moveDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized;
        }
    }
}