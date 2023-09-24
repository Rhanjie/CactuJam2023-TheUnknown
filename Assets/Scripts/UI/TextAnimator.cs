using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextAnimator : MonoBehaviour
{
    [SerializeField]
    private Text textObject;

    [SerializeField]
    private float animationTime = 0.5f;
    
    [SerializeField]
    private string defaultText = "";

    public UnityEvent OnAnimationComplete { get; private set; }

    private void Start()
    {
        if (defaultText.Length != 0)
            Init(defaultText);
    }

    public void Init(string text)
    {
        textObject.text = "";
        
         textObject.DOText(text, animationTime)
            .OnStart(() => textObject.text = "")
            .OnComplete(() => OnAnimationComplete?.Invoke());
    }
}