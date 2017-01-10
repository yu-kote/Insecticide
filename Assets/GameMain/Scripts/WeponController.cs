using UnityEngine;
using System.Collections;

/// <summary>
/// 武器が必ず持つクラス（武器のステータス管理）
/// </summary>
public class WeponController : MonoBehaviour
{

    public GameObject hitobject;
    public void OnTriggerExit(Collider other)
    {
        hitobject = other.gameObject;
    }

    public float power;

    [SerializeField]
    PlayerController player;

    void Start()
    {

    }

    void Update()
    {
    }
}
