using UnityEngine;
using Transitions;
using UnityEngine.UI;

[RequireComponent(typeof(TransitionTunnel))]
public class TransitionFadeSides : MonoBehaviour
{
    private Transform _selectedSide; 
    private void Awake()
    {
        var tunnel = GetComponent<TransitionTunnel>();
        tunnel.AddOnLeftEffect(FadeEffectLeft);  
        tunnel.AddOnRightEffect(FadeEffectRight);

        for (int i = 0; i < transform.childCount; i++)
        {
            var newMat = new Material(transform.GetChild(i).GetComponent<Image>().material);
            transform.GetChild(i).GetComponent<Image>().material = newMat;
        }
    }

    private void FadeEffectLeft(float value)
    {
        _selectedSide = transform.GetChild(0);
        FadeEffect(value);
    }
    private void FadeEffectRight(float value)
    {
        _selectedSide = transform.GetChild(1);
        FadeEffect(value);
    }
    private void FadeEffect(float value)
    {
        value = Mathf.Abs(value - 1f);
        _selectedSide.GetComponent<Image>().material.color =  new Color(1,1,1,value);
    }
}