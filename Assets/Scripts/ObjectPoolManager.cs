using UnityEngine;

public enum PoolType
{
    Bullet, Enemy
}

public interface IObjectPool<T> where T : class
{
    int CountInactive { get; set; }

    T Get();

    void Release(T element);

    void Clear();
}

public class ObjectPoolManager : MonoBehaviour
{

}
