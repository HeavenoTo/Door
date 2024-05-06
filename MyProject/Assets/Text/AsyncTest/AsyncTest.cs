using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class AsyncTest : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public Button Clear;
    public Image clone;
    public Transform content;
    public Image move;
    public bool nor;
    public int moveDis;
    // Start is called before the first frame update
    void Start()
    {
        button1.onClick.AddListener(Button1Onclick);
        button2.onClick.AddListener(Button2Onclick);
        button3.onClick.AddListener(Button3Onclick);
        Clear.onClick.AddListener(ClearBtnOnClisk);

    }
    void FixedUpdate()
    {
        if (move.rectTransform.anchoredPosition.y > 2238)
        {
            nor = true;
        }
        if (move.rectTransform.anchoredPosition.y < 0)
        {
            nor = false;
        }
        move.rectTransform.anchoredPosition += new Vector2(0, nor ? -moveDis : moveDis);
    }
    private void ClearBtnOnClisk()
    {
        for (int i = content.childCount-1; i > 0; i--)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }
    private void Button1Onclick()
    {
        for (int i = 0; i < 100; i++)
        {
            Image item = Instantiate(clone, content);
            item.transform.GetComponentInChildren<Text>().text = i.ToString();
        }
    }
    private void Button2Onclick()
    {
        for (int i = 0; i < 100; i++)
        {
            StartCoroutine(LoadItem(100));
        }
    }
    private void Button3Onclick()
    {
        for (int i = 0; i < 100; i++)
        {
           LoadItem2(100);
        }
    }
    private async UniTask InitItem(int count)
    {
        await UniTask.NextFrame();
        for (int i = 0; i < count; i++)
        {
            Image item = Instantiate(clone, content);
            item.transform.GetComponentInChildren<Text>().text = i.ToString();
        }
    }

    IEnumerator LoadItem(int count)
    {
        yield return null;
        for (int i = 0; i < count; i++)
        {
            Image item = Instantiate(clone, content);
            item.transform.GetComponentInChildren<Text>().text = i.ToString();
        }
    }
    private async void LoadItem2(int count)
    {
        await InitItem(count);
    }
    // private unit
}
