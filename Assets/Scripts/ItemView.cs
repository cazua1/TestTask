using UnityEngine;
using TMPro;

public class ItemView : MonoBehaviour
{
    [SerializeField] private TMP_Text _number;

    public void SetNumberView(string numberView)
    {
        _number.text = numberView;
    } 
}
