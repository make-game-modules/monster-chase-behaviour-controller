// 使用UnityEngine库，这是一个Unity3D的API库，提供了很多有用的类和函数
using UnityEngine;

/// <summary>
/// 这个类(MonsterChaseBehaviourController)的主要目的是控制怪物的追踪行为。挂载到怪物类型的对象上，
/// 该脚本将使怪物能够追踪并移向标签为“Player”的玩家对象，同时在接近其他对象时避开它们。
/// 怪物的移动速度、避障力度和避障距离可由Unity编辑器中的公开属性进行设置。
/// Start方法中，找到并储存玩家对象的引用。在Update方法中，计算怪物应该移动的方向并更新怪物的位置。
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class MonsterChaseBehaviourController : MonoBehaviour
{

    public float speed = 1.0f; // 这是怪物的移动速度
    public float avoidStrength = 0.5f; // 这是怪物的避障力度，越大则在接近其他对象时避开的越远
    public float avoidDistance = 1.0f; // 这是怪物在多远的距离开始尝试避开其他对象

    private GameObject player; // 这是玩家对象，怪物将会尝试追踪的对象

    /// <summary>
    /// Start方法在初始化时调用，这里用来获取并储存对玩家对象的引用。
    /// </summary>
    void Start()
    {


        player = GameObject.FindWithTag("Player"); // 在场景中找到标签为"Player"的对象并保存到player变量中
    }

    /// <summary>
    /// Update方法每帧都会调用。这里用来更新怪物的位置，使其朝向玩家移动且避开其他对象。
    /// </summary>
    private void Update()
    {
        if (player != null) // 如果玩家对象存在
        {
            // 计算从怪物到玩家的方向，然后归一化得到单位向量
            Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;
            direction.Normalize();

            Vector2 avoidForce = Vector2.zero; // 初始化避障力为零
            // 在怪物周围的一定范围内找到所有碰撞体
            Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, avoidDistance);
            foreach (Collider2D nearbyObject in nearbyObjects)
            {
                if (nearbyObject.gameObject != gameObject) // 如果发现的对象不是怪物自身
                {
                    // 计算从发现的对象到怪物的方向，并按距离的倒数加权，得到避障力
                    Vector2 avoidDirection = (Vector2)transform.position - (Vector2)nearbyObject.transform.position;
                    avoidForce += avoidDirection.normalized / avoidDirection.magnitude;
                }
            }

            // 计算总的力量，包括朝向玩家的力和避障力，然后归一化得到单位向量
            Vector2 combinedForce = direction + avoidStrength * avoidForce;
            combinedForce.Normalize();

            // 计算怪物新的位置，即当前位置加上速度乘以时间乘以方向
            Vector2 targetPosition = (Vector2)transform.position + combinedForce * speed * Time.deltaTime;
            transform.position = targetPosition; // 更新怪物的位置
        }
    }
}
