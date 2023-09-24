using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private Text interactionText;
        
        [SerializeField]
        private Dictionary dictionary;

        public void ToggleInteractionText(bool value)
        {
            interactionText.gameObject.SetActive(value);
        }
        
        public void OpenDictionary(string title, string content)
        {
            dictionary.Open(title, content);
        }

        public void CloseDictionary()
        {
            dictionary.Close();
        }
    }
}
