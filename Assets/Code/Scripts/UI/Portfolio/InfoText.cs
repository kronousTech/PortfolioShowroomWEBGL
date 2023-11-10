using System;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class InfoText : MonoBehaviour
{
    [SerializeField][TextArea(10, 250)] private string textBeforeTime;
    [SerializeField][TextArea(10, 250)] private string textAfterTime;
    [SerializeField] private Color _color;

    private void Start()
    {
        SetText();
    }

#if UNITY_EDITOR
    private void Update()
    {
        SetText();
    }
#endif

    private void SetText()
    {
        var begginingOfCarrer = new DateTime(2019, 1, 21, 14, 20, 10);
        var carrerTime = DateTime.Now - begginingOfCarrer;
        var time = DateTime.MinValue + carrerTime;
        var carrerTimeString =
            "<color=#" + ColorUtility.ToHtmlStringRGBA(_color) + ">"
            + "<b>" + time.Year   + "</b>" + "Years " 
            + "<b>" + time.Month  + "</b>" + " Months " 
            + "<b>" + time.Day    + "</b>" + " Days " 
            /*+ "<b>" + time.Hour   + "</b>" + " Hours "
            + "<b>" + time.Minute + "</b>" + " Minutes "
            + "<b>" + time.Second + "</b>" + " Seconds" */
            + "</color>";

        GetComponent<TextMeshPro>().text = textBeforeTime + " " + carrerTimeString + " " + textAfterTime;
    }
}
