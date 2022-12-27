using UnityEngine;
using UnityEngine.Events;

public class ScrollViewer : MonoBehaviour
{
    [SerializeField] private ItemView[] _views;
    [SerializeField] private RectTransform _content;

    private const int ItemHeight = 100;
    private const int Spacing = 10;
    private const int Top = 50;
    private const int Bottom = 50;
        
    private int _count;
    private int _lastIndex;
    private int _nextIndex;
    private RectTransform _item;
    private float _positionY;    

    public event UnityAction<int, ItemView> ItemShowed;

    private void Update()
    {
        Render();
    }

    public void SetData(int count)
    {
        float containerHeight;

        _count = count;
        containerHeight = ItemHeight * count + Top + Bottom + (count == 0 ? 0 : ((count - 1) * Spacing));
        _content.sizeDelta = new Vector2(_content.sizeDelta.x, containerHeight);
        var position = _content.anchoredPosition;        
        _content.anchoredPosition = position; 
        var positoinY = Top;

        for (int i = 0; i < _views.Length; i++)
        {
            bool isDisplayed = i < count;
            _views[i].gameObject.SetActive(isDisplayed);

            if (isDisplayed)
            {
                position = _views[i].GetComponent<RectTransform>().anchoredPosition;
                position.y = -positoinY;
                _views[i].GetComponent<RectTransform>().anchoredPosition = position;
                positoinY += Spacing + ItemHeight;
                ItemShowed(i, _views[i]);                    
            }
        }
    }

    private void Render()
    {
        int currentIndex;        
        int lastIndex;

        _positionY = _content.anchoredPosition.y - Spacing;

        if (_positionY < 0)
            return;

        currentIndex = Mathf.FloorToInt(_positionY / (ItemHeight + Spacing));

        if (_lastIndex == currentIndex)
            return;

        if (currentIndex > _lastIndex)
        {
            _nextIndex = currentIndex % _views.Length;
            _nextIndex--;

            if (_nextIndex < 0)
                _nextIndex = _views.Length - 1;

            lastIndex = currentIndex + _views.Length - 1;

            if (lastIndex < _count)            
                CalculateItemPosition(lastIndex);            
        }
        else
        {
            _nextIndex = currentIndex % _views.Length;
            CalculateItemPosition(currentIndex);
        }
        _lastIndex = currentIndex;
    }

    private void CalculateItemPosition(int index)
    {
        _item = _views[_nextIndex].GetComponent<RectTransform>();
        var position = _item.anchoredPosition;
        position.y = -(Top + index * Spacing + index * ItemHeight);
        _item.anchoredPosition = position;
        ItemShowed(index, _views[_nextIndex]);
    }
}