using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private ScrollViewer _viewer;
    [SerializeField] private int _numberOfItems;

    private void OnEnable()
    {
        _viewer.ItemShowed += OnItemShowed;        
    }   

    private void OnDisable()
    {
        _viewer.ItemShowed -= OnItemShowed;
    }

    private void Start()
    {
        _viewer.SetData(_numberOfItems);
    }

    private void OnItemShowed(int index, ItemView view)
    {
        view.SetNumberView(index.ToString());
    }
}
