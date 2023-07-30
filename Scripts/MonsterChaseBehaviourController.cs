// ʹ��UnityEngine�⣬����һ��Unity3D��API�⣬�ṩ�˺ܶ����õ���ͺ���
using UnityEngine;

/// <summary>
/// �����(MonsterChaseBehaviourController)����ҪĿ���ǿ��ƹ����׷����Ϊ�����ص��������͵Ķ����ϣ�
/// �ýű���ʹ�����ܹ�׷�ٲ������ǩΪ��Player������Ҷ���ͬʱ�ڽӽ���������ʱ�ܿ����ǡ�
/// ������ƶ��ٶȡ��������Ⱥͱ��Ͼ������Unity�༭���еĹ������Խ������á�
/// Start�����У��ҵ���������Ҷ�������á���Update�����У��������Ӧ���ƶ��ķ��򲢸��¹����λ�á�
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class MonsterChaseBehaviourController : MonoBehaviour
{

    public float speed = 1.0f; // ���ǹ�����ƶ��ٶ�
    public float avoidStrength = 0.5f; // ���ǹ���ı������ȣ�Խ�����ڽӽ���������ʱ�ܿ���ԽԶ
    public float avoidDistance = 1.0f; // ���ǹ����ڶ�Զ�ľ��뿪ʼ���Աܿ���������

    private GameObject player; // ������Ҷ��󣬹��ｫ�᳢��׷�ٵĶ���

    /// <summary>
    /// Start�����ڳ�ʼ��ʱ���ã�����������ȡ���������Ҷ�������á�
    /// </summary>
    void Start()
    {


        player = GameObject.FindWithTag("Player"); // �ڳ������ҵ���ǩΪ"Player"�Ķ��󲢱��浽player������
    }

    /// <summary>
    /// Update����ÿ֡������á������������¹����λ�ã�ʹ�䳯������ƶ��ұܿ���������
    /// </summary>
    private void Update()
    {
        if (player != null) // �����Ҷ������
        {
            // ����ӹ��ﵽ��ҵķ���Ȼ���һ���õ���λ����
            Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
            direction.Normalize();

            Vector2 avoidForce = Vector2.zero; // ��ʼ��������Ϊ��
            // �ڹ�����Χ��һ����Χ���ҵ�������ײ��
            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, avoidDistance);
            foreach (Collider2D nearbyObject in nearbyObjects)
            {
                if (nearbyObject.gameObject != gameObject) // ������ֵĶ����ǹ�������
                {
                    // ����ӷ��ֵĶ��󵽹���ķ��򣬲�������ĵ�����Ȩ���õ�������
                    Vector2 avoidDirection = (Vector2)transform.position - (Vector2)nearbyObject.transform.position;
                    avoidForce += avoidDirection.normalized / avoidDirection.magnitude;
                }
            }

            // �����ܵ�����������������ҵ����ͱ�������Ȼ���һ���õ���λ����
            Vector2 combinedForce = direction + avoidStrength * avoidForce;
            combinedForce.Normalize();

            // ��������µ�λ�ã�����ǰλ�ü����ٶȳ���ʱ����Է���
            Vector2 targetPosition = (Vector2)transform.position + combinedForce * speed * Time.deltaTime;
            transform.position = targetPosition; // ���¹����λ��
        }
    }
}
