using UnityEngine;
using DG.Tweening;

public class TutorialAnimation : MonoBehaviour
{
    [SerializeField] private ConveyorMoving Conveyor;

    [Header("TutorialPaper")]
    [SerializeField] private GameObject tutorialPaper;
    [SerializeField] private Transform tutorialPaperPosition;
    [SerializeField] private float animationTime = 1f;
    [SerializeField] private float scaleChanger = 0.5f;
    [SerializeField] private float startTimeDelay = 2f;
    [SerializeField] private float fadeTime = 1f;

    [Header("TutorialHand")]
    [SerializeField] private GameObject tutorialHand;
    [SerializeField] private Transform handStartPosition;
    [SerializeField] private Transform handTargetPosition;
    [SerializeField] private Transform nextTarget;
    [SerializeField] private float handMoveTime = 1.5f;

    public bool IsCanDragging { get; private set; } = false;
    public bool IsCanOther { get; set; } = false;
    public bool IsHandFading { get; private set; } = false;

    private SpriteRenderer handRenderer;
    private Sequence handSequence;

    private void Start()
    {
        handRenderer = tutorialHand.GetComponent<SpriteRenderer>();
        ShowRecipe();
    }

    private void ShowRecipe()
    {
        SpriteRenderer[] spriteRenderers = tutorialPaper.GetComponentsInChildren<SpriteRenderer>();
        foreach (var spriteRenderer in spriteRenderers)
        {
            Color startColor = spriteRenderer.color;
            startColor.a = 0f;
            spriteRenderer.color = startColor;
            spriteRenderer.DOFade(1f, fadeTime);
        }

        DOVirtual.DelayedCall(startTimeDelay, () =>
        {
            MoveToTarget();
        });
    }

    private void MoveToTarget()
    {
        Vector3 targetScale = tutorialPaper.transform.localScale * scaleChanger;

        Sequence sequence = DOTween.Sequence();
        sequence.Join(tutorialPaper.transform.DOMove(tutorialPaperPosition.position, animationTime));
        sequence.Join(tutorialPaper.transform.DOScale(targetScale, animationTime));
        sequence.SetEase(Ease.InOutQuad);
        sequence.OnComplete(() =>
        {
            TutorialHand();
            IsCanDragging = true;
        });
    }

    private void TutorialHand()
    {
        if (handSequence.IsActive())
            handSequence.Kill();  // ”ничтожаем старую анимацию, если есть

        Color handColor = handRenderer.color;
        handColor.a = 0f;
        handRenderer.color = handColor;
        handRenderer.DOFade(1f, fadeTime);

        tutorialHand.transform.position = handStartPosition.position;

        handSequence = DOTween.Sequence();  // —оздаЄм новый Sequence

        handSequence.Append(tutorialHand.transform.DOMove(handTargetPosition.position, handMoveTime).SetEase(Ease.InOutSine))
                    .Append(tutorialHand.transform.DOMove(handStartPosition.position, handMoveTime).SetEase(Ease.InOutSine))
                    .SetLoops(-1, LoopType.Restart)
                    .OnUpdate(() => CheckForObject());
    }

    private void CheckForObject()
    {
        if (IsCanOther) return;

        DraggProduct foundObject = FindObjectOfType<DraggProduct>();

        if (foundObject.gameObject.name == "sauce(Clone)")
        {
            MoveHandToTarget(foundObject.transform, nextTarget);
        }
    }

    private void MoveHandToTarget(Transform start, Transform target)
    {
        if (handSequence.IsActive())
            handSequence.Kill();

        tutorialHand.transform.position = start.position;

        handSequence = DOTween.Sequence();

        handSequence.Append(tutorialHand.transform.DOMove(target.position, handMoveTime).SetEase(Ease.InOutSine))
                    .Append(tutorialHand.transform.DOMove(start.position, handMoveTime).SetEase(Ease.InOutSine))
                    .SetLoops(-1, LoopType.Yoyo);
    }

    public void FadeHand()
    {
        if (handSequence != null && handSequence.IsActive())
            handSequence.Kill();

        handRenderer.DOFade(0f, fadeTime);
    }
}
