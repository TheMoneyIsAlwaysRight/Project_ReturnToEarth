using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 제작 : 찬규 
/// 세이프 에리어를 찾아서 해당 범위 내부에만 UI가 위치하도록 하는 컴포넌트
/// </summary>
public class SafeArea : MonoBehaviour
{
    [SerializeField] RectTransform _rectTransform;
    Rect _safeArea;
    Vector2 _minAnchor;
    Vector2 _maxAnchor;

    void Awake()
    {
        // _rectTransform = GetComponent<RectTransform>();
        SizeSet();
    }

    [ContextMenu("Size Set")]
    void SizeSet()
    {
        _safeArea = Screen.safeArea;
        _minAnchor = _safeArea.position;
        _maxAnchor = _minAnchor + _safeArea.size;
        Debug.Log(_safeArea.size);

        //_minAnchor.x /= Screen.width;
        //_maxAnchor.x /= Screen.width;
        _minAnchor.x = _rectTransform.anchorMin.x;
        _maxAnchor.x = _rectTransform.anchorMax.x;


        _minAnchor.y /= Screen.height;
        _maxAnchor.y /= Screen.height;

        _rectTransform.anchorMin = _minAnchor;
        _rectTransform.anchorMax = _maxAnchor;
    }

    [ContextMenu("Size Set 10")]
    void SizeSetTest()
    {
        for (int i = 0; i < 10; i++)
            SizeSet();
    }
}
