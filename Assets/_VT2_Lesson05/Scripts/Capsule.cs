using Unity.VectorGraphics;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    [Header("*** Raycast Settings ***")]
    [SerializeField] private float _raycastDistance = 2.0f;    // 判定の距離設定

    [SerializeField] private LayerMask _layerMask;                      // 判定のレイヤー設定

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 光線の判定
        bool isHit = Physics.Raycast(
            origin: transform.position,     // 光線の始点
            direction: Vector3.down,        // 光線の方向
            hitInfo: out RaycastHit hit,    // 衝突したオブジェクトの情報
            maxDistance: _raycastDistance,  // 光線の長さ
            layerMask: _layerMask           // 衝突レイヤー
            );

        // 光線を可視化
        Debug.DrawRay(transform.position, Vector3.down * _raycastDistance, Color.red);

        if(isHit)
        {
            // 光線に反応がある場合
            Debug.Log($"衝突情報 ----" + 
                $"\nName = {hit.transform.name}" +
                $"\nPoint(衝突位置) = {hit.point}" + 
                $"\nNormal(法線) = {hit.normal}" +
                $"\nDistance(距離) = {hit.distance}");

            // 光線を可視化
            Debug.DrawRay(hit.point, hit.normal, Color.yellow);
        }

        // ひたすら前進
        Vector3 velocity = transform.forward;
        Debug.DrawRay(transform.position, velocity, Color.yellow);

        // 進行方向と法線の外積　= 進行方向に右向きのベクトル
        Vector3 right = Vector3.Cross(velocity, hit.normal);
       
        // 右向きベクトルと法線の外積 = スロープのベクトル
        //Vector3 slope = Vector3.Cross(right, hit.normal);

        // 直接坂道のベクトルを取得する
        Vector3 slope = Vector3.ProjectOnPlane(velocity, hit.normal);

        // 坂道のベクトルを可視化
        Debug.DrawRay(hit.point, slope.normalized, Color.green);

        transform.position += slope.normalized * -1 * 30.0f * Time.deltaTime;
    }
}
