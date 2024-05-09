using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Networking;
using System.IO;
public class TestUniTask : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private RawImage Raw;
    [SerializeField] private Image Img;
    [SerializeField] private Button button;
    [SerializeField] private Button button2;
    private List<CancellationTokenSource> CanceS;
    private List<TestUniTask2> tasks;
    // Start is called before the first frame update
    async void Start()
    {
        CanceS = new List<CancellationTokenSource>();
        tasks = new List<TestUniTask2>();
        button.onClick.AddListener(ButtonOnClick);
        button2.onClick.AddListener(ButtonOnClick2);

        string url = "https://plus.unsplash.com/premium_photo-1686591099761-42af5938273a?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxlZGl0b3JpYWwtZmVlZHwxfHx8ZW58MHx8fHx8";
        Img.sprite = await DownloadImage(url);
        Img.SetNativeSize();
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

    async UniTask<Sprite> DownloadImage(string url)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        await webRequest.SendWebRequest();

        if (webRequest.isNetworkError || webRequest.isHttpError)
        {
            Debug.LogError(webRequest.error);
            return null;
        }
        else
        {
            // 获取下载的数据
            byte[] data = webRequest.downloadHandler.data;
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(data);
            Raw.texture = texture;
            Sprite sprite = TextureToSprite(texture);
            Debug.Log("图片下载并显示成功！");
            // 保存图片到本地
            // File.WriteAllBytes("localImageName.jpg", data);
            // Debug.Log("图片下载成功！");
            return sprite;
        }
    }

    private Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
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
