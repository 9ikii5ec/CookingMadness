using UnityEngine;

public class ProductPlacement : MonoBehaviour
{
    [SerializeField] private string plateCellTag = "PlateCell";
    [SerializeField] private float placementRadius = 0.5f;
    [SerializeField] private TutorialAnimation tutorialAnimation;
    [SerializeField] private RecipeChecker recipeChecker;
    [SerializeField] private GameObject checkMark;

    private Transform plateCell;

    private void Start()
    {
        GameObject plateObject = GameObject.FindGameObjectWithTag(plateCellTag);

        plateCell = plateObject.transform;
    }

    private void Update()
    {
        CheckForPlacement();
    }

    private void CheckForPlacement()
    {
        float distance = Vector2.Distance(transform.position, plateCell.position);
        if (distance <= placementRadius)
        {
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        Collider2D collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        transform.position = plateCell.transform.position + new Vector3(0f,0.5f,0f);
        tutorialAnimation = FindFirstObjectByType<TutorialAnimation>();
        tutorialAnimation.FadeHand();

        tutorialAnimation.IsCanOther = true;

        tutorialAnimation.backGround.SetActive(false);
        checkMark.SetActive(true);
        recipeChecker.CheckMarkChecker();
    }
}
