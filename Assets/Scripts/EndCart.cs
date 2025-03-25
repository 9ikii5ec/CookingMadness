using UnityEngine;

public class EndCart : MonoBehaviour
{
    [SerializeField] private TutorialAnimation tutorialAnimation;
    [SerializeField] private GameObject endCartImage;
    [SerializeField] private GameObject endCartPanel;

    private float timer = 0f;
    private float maxWaitTime = 30f;

    private void Update()
    {
        if (tutorialAnimation.IsCanOther)
        {
            timer = 0f;
            return;
        }

        timer += Time.deltaTime;

        if (timer >= maxWaitTime)
        {
            HandleTimeout();
            timer = 0f;
        }
    }

    private void HandleTimeout()
    {
        Debug.Log("Прошло 30 секунд! Предлагаем помощь...");
        endCartImage.SetActive(true);
        endCartPanel.SetActive(true);
    }
}
