using TMPro;
using UnityEngine;

namespace WTUtils
{
    public class ExampleManaged : ManagedBehaviour
    {
        [SerializeField] TextMeshProUGUI _textDisplay;

        private float colorValue = 0f;
        public void Setup(string descriptionText)
        {
            _textDisplay.text = descriptionText;
            TryInitialize();
        }

        private void Update()
        {
            if (CanUpdate())
            {
                colorValue += 0.1f;
                if (colorValue > 1f)
                {
                    colorValue = 0f;
                }
                _textDisplay.color = new Color(colorValue, colorValue, colorValue);
            }
        }
    }
}

