using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sirenix.OdinInspector;

namespace Extentions
{
public interface IGameObjectPool<T> where T : MonoBehaviour
{
    T Pop();
    T Pop(Vector3 position);
    T Pop(Transform parent, Vector3 localPosition);
    void Push(T elementView);
}

public class GameObjectPool<T> : MonoBehaviour, IGameObjectPool<T>
    where T : MonoBehaviour
{
    private enum StoringType
    {
        Default,
        InHere,
        InChoseParent
    }

    private enum PoolType
    {
        Resizeable,
        Const
    }

    [SerializeField] private GameObject prefab;
    [SerializeField] private int startSize;
    [SerializeField] private bool switchOff;
    [SerializeField] private StoringType storingType = StoringType.InHere;
    [SerializeField] private PoolType poolType = PoolType.Resizeable;
    private bool isInChoseParent => storingType == StoringType.InChoseParent;

    [ShowIf("isInChoseParent"), SerializeField]
    private Transform parentToSpawn;

    [ShowInInspector, ReadOnly] private Stack<T> pool = new();
    private bool poolPrepared;

    private async void Awake()
    {
        await SpawnPoolPrivate();
    }

    public async Task WaitPoolPrepare()
    {
        while (!poolPrepared)
        {
            await Task.Delay(10);
        }
    }

    private async Task SpawnPoolPrivate()
    {
        for (var i = 0; i < startSize; i++)
        {
            pool.Push(Modify(SpawnElement()));
            await Task.Delay(20);
        }

        poolPrepared = true;
    }

    private T Modify(T t)
    {
        if (switchOff)
            t.gameObject.SetActive(false);
        t.gameObject.name += "_" + pool.Count;
        return t;
    }

    private T SpawnElement()
    {
        return storingType switch
        {
            StoringType.InHere => Instantiate(prefab, transform).GetComponent<T>(),
            StoringType.InChoseParent => Instantiate(prefab, parentToSpawn).GetComponent<T>(),
            _ => Instantiate(prefab).GetComponent<T>()
        };
    }


    public virtual T Pop()
    {
        return pool.Count == 0 ? (poolType == PoolType.Resizeable ? SpawnElement() : null) : pool.Pop();
    }

    public virtual T Pop(Vector3 position)
    {
        var t = Pop();
        t.transform.position = position;
        return t;
    }

    public virtual T Pop(Transform parent, Vector3 localPosition)
    {
        var t = Pop();
        t.transform.parent = parent;
        t.transform.localPosition = localPosition;
        return t;
    }

    public virtual void Push(T elementView)
    {
        pool.Push(elementView);
        elementView.transform.SetParent(storingType switch
        {
            StoringType.InHere => transform,
            StoringType.InChoseParent => parentToSpawn,
            _ => elementView.transform.parent
        });
    }
}
}