using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Networking;
public class TestUniTask : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Button button;
    [SerializeField] private Button button2;
    private List<CancellationTokenSource> CanceS;
    private List<TestUniTask2> tasks;
    // Start is called before the first frame update
    void Start()
    {
        CanceS = new List<CancellationTokenSource>();
        tasks = new List<TestUniTask2>();
        button.onClick.AddListener(ButtonOnClick);
        button2.onClick.AddListener(ButtonOnClick2);




    }


    int a = 0;
    private async void ButtonOnClick()
    {
        GameObject game = new GameObject();
        game.transform.SetParent(transform);
        TestUniTask2 task = game.AddComponent<TestUniTask2>();
        task.index = a;
        tasks.Add(task);
        a++;
        button.enabled = false;
        await UniTask.WaitUntil(() => task.source == null);
        // await UniTask.Delay(50);
        // await UniTask.WaitUntilValueChanged(task, x => x.source);
        button.enabled = true;
        Debug.Log("重启button");

        text.text = await GetTextAsync(UnityWebRequest.Get("http://google.com"));
        // ToUniTask(IProgress, PlayerLoopTiming, CancellationToken);
    }
    IEnumerator Enumerator()
    {
        yield return new WaitForSeconds(1);
    }

    private void ButtonOnClick2()
    {
        if (tasks.Count == 0)
        {
            return;
        }
        tasks[tasks.Count - 1].source.Cancel();
        // GameObject.Destroy(tasks[tasks.Count - 1].gameObject);
        // tasks.RemoveAt(tasks.Count - 1);
    }

    async UniTask<string> GetTextAsync(UnityWebRequest req)
    {
        var op = await req.SendWebRequest();
        return op.downloadHandler.text;
    }
}
