using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Develop.Runtime.UI.CommonViews
{
    public class IconTextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _icon;
        
        public void SetText(string text) => _text.text = text;
        
        public void SetIcon(Sprite icon) => _icon.sprite = icon;
    }
}