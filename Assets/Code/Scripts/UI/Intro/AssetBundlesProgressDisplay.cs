using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AssetBundlesProgressDisplay : MonoBehaviour
{
    [SerializeField] private Slider _progress;

    private void OnEnable()
    {
        AssetsLoader.OnProgress += UpdateProgressBar;
        AssetsLoader.OnBundlesDownload += () => StartCoroutine(OnComplete());
    }
    private void OnDisable()
    {
        AssetsLoader.OnProgress -= UpdateProgressBar;
        AssetsLoader.OnBundlesDownload -= () => StartCoroutine(OnComplete());
    }

    private void UpdateProgressBar(int count, int total)
    {
        _progress.value = (float)count / (float)total;
    }

    private IEnumerator OnComplete()
    {
        yield return new WaitForSeconds(1.5f);

        gameObject.SetActive(false);
    }
}
