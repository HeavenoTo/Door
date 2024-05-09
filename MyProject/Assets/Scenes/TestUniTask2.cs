using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class TestUniTask2 : MonoBehaviour
{
    [Header("11111111111")]
    public int index;
    public CancellationTokenSource source;
    // Start is called before the first frame update
    async void Start()
    {
        Debug.Log("创建完成");
        name = index.ToString();
        source = new CancellationTokenSource();
        bool complete = await UniTask.Delay(10000, cancellationToken: source.Token).SuppressCancellationThrow();
        if (complete)
        {
            Debug.Log("已销毁-------------   " + index);
            GameObject.Destroy(this.gameObject);
        }
        else
        {
            Debug.Log("等待完成------------   " + index);
            GameObject.Destroy(this.gameObject);
        }
        source = null;
        Debug.Log("==========" + source);
    }
    private void OnDestroy()
    {
        source?.Cancel();
        source = null;
    }
}
