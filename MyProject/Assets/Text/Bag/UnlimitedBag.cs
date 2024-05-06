using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlimitedBag : MonoBehaviour
{
    public Image drag;
    public Transform content;
    public Image itemPrefab;
    public int itemCount;
    public bool H;
    public bool V;

    //速度衰減率
    public float decelerationRate = 0.9f;
    //滑动结束时的瞬时速度
    private Vector3 Speed = Vector3.zero;
    private RectTransform _dragRt;
    private bool _mouseDown;
    private Vector2 _mouseStartPos;

    private GridLayoutGroup _gridLayoutGroup;
    private RectTransform _content;
    private Vector2 ContentAnchoredPosition { get { return _content.anchoredPosition; } set { _content.anchoredPosition = value; } }
    void Start()
    {
        _gridLayoutGroup = content.GetComponent<GridLayoutGroup>();
        if (H)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            _gridLayoutGroup.constraintCount = 1;
        }
        if (V)
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = 18;
        }



        _dragRt = drag.GetComponent<RectTransform>();

        for (int i = 0; i < itemCount; i++)
        {
            PropsItem item = new PropsItem();
            item.Id = i.ToString();
            Image img = Instantiate(itemPrefab, content);
            img.transform.localScale = Vector3.one;
            img.name = i.ToString();
            img.GetComponentInChildren<Text>().text = i.ToString();
            img.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mouseDown = true;
            TouchBegin(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && _mouseDown)
        {
            TouchMove(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            TouchUp();
            revert = true;
            _mouseDown = false;
            _mouseStartPos = Vector3.zero;
        }
        InertiaMove();
        AutoRevert();
    }

    void TouchBegin(Vector2 screenPos)
    {
        Vector2 outPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, screenPos, Camera.main, out outPos);
        _mouseStartPos = outPos;
    }

    void TouchMove(Vector2 screenPos)
    {
        Vector2 outPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, screenPos, Camera.main, out outPos);
        Vector2 movePos = outPos - _mouseStartPos;
        if (H) movePos = new Vector2(movePos.x, 0);
        if (V) movePos = new Vector2(0, movePos.y);
        _dragRt.anchoredPosition += movePos;
        CameraIN();
        _mouseStartPos = outPos;
    }
    //限制
    private void CameraIN()
    {
        // Vector3 pos = Camera.main.WorldToScreenPoint(_dragRt.transform.position);
        // pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        // pos.y = Mathf.Clamp(pos.y, 0, Screen.height);
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(_dragRt.parent as RectTransform, pos, Camera.main, out Vector2 rectPos);
        // _dragRt.localPosition = rectPos;
    }
    private void TouchUp()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, Input.mousePosition, Camera.main, out Vector2 outPos);
        Vector2 movePos = outPos - _mouseStartPos;
        Speed = movePos;
        Debug.Log("Current Speed" + Vector3.Magnitude(Speed));
    }
    private void InertiaMove()
    {
        if (Vector3.Magnitude(Speed) <= 0.1 || _mouseDown) return;
        Speed *= decelerationRate;
        if (H) Speed = new Vector2(Speed.x, 0);
        if (V) Speed = new Vector2(0, Speed.y);
        _dragRt.anchoredPosition += (Vector2)Speed;


        CameraIN();
    }
    bool revert = false;
    private void AutoRevert()
    {
        if (!revert)
        {
            return;
        }
        if (H)
        {
            if (_dragRt.anchoredPosition.x > 0)
            {
                _dragRt.anchoredPosition = new Vector2(_dragRt.anchoredPosition.x - 10, _dragRt.anchoredPosition.y);
            }
            else if (_dragRt.anchoredPosition.x < itemCount * (_gridLayoutGroup.cellSize.x + _gridLayoutGroup.spacing.x))
            {
                _dragRt.anchoredPosition = new Vector2(_dragRt.anchoredPosition.x + 10, _dragRt.anchoredPosition.y);
            }
            else
            {
                revert = false;
            }
        }
        if (V)
        {
            if (_dragRt.anchoredPosition.y > 0)
            {
                _dragRt.anchoredPosition = new Vector2(_dragRt.anchoredPosition.x, _dragRt.anchoredPosition.y - 10);
            }
            else if (_dragRt.anchoredPosition.y < itemCount * (_gridLayoutGroup.cellSize.y + _gridLayoutGroup.spacing.y))
            {
                _dragRt.anchoredPosition = new Vector2(_dragRt.anchoredPosition.x, _dragRt.anchoredPosition.y + 10);
            }
            else
            {
                revert = false;
            }
        }

    }
}
