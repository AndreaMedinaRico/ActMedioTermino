using UnityEngine;
using TMPro;

public class BulletUI : MonoBehaviour {

    public TextMeshProUGUI bulletText;

    private void OnEnable() {
        BulletCounter.OnBulletCountChanged += UpdateBulletCount;
    }

    private void OnDisable() {
        BulletCounter.OnBulletCountChanged -= UpdateBulletCount;
    }

    private void UpdateBulletCount() {
        bulletText.text = BulletCounter.Instance.GetBulletCount().ToString();
    }
}
