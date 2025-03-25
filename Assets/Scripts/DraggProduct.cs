using UnityEngine;

public class DraggProduct : MonoBehaviour
{
    [SerializeField] private TutorialAnimation tutorialAnimation;

    public GameObject mergedProductPrefab;
    public string productType;

    [HideInInspector] 
    public bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private bool isMerged = false;

    private void Start()
    {
        tutorialAnimation = FindAnyObjectByType<TutorialAnimation>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!tutorialAnimation.IsCanDragging) return;

        if (Input.GetMouseButtonDown(0))
        {
            TryGrabObject();
        }

        if (isDragging)
        {
            DragObject();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            ReleaseObject();
        }
    }

    private void TryGrabObject()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isDragging = true;
            offset = transform.position - mousePosition;
        }
    }

    private void DragObject()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        transform.position = mousePosition + offset;
    }

    private void ReleaseObject()
    {
        isDragging = false;

        if (!isMerged)
        {
            CheckForMerge();
        }
    }

    private void CheckForMerge()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (var collider in colliders)
        {
            if (collider.gameObject == gameObject) continue;

            DraggProduct otherProduct = collider.GetComponent<DraggProduct>();

            if (otherProduct.productType == productType && otherProduct.mergedProductPrefab == mergedProductPrefab)
            {
                MergeWith(otherProduct);
                break;
            }
        }
    }

    private void MergeWith(DraggProduct otherProduct)
    {
        isMerged = true;
        otherProduct.isMerged = true;

        Vector3 mergePosition = (transform.position + otherProduct.transform.position) / 2;
        GameObject newProduct = Instantiate(mergedProductPrefab, mergePosition, Quaternion.identity);

        SpriteRenderer newRenderer = newProduct.GetComponent<SpriteRenderer>();
        SpriteRenderer currentRenderer = GetComponent<SpriteRenderer>();

        newRenderer.sortingOrder = Mathf.Max(currentRenderer.sortingOrder, otherProduct.GetComponent<SpriteRenderer>().sortingOrder) + 1;     

        Destroy(otherProduct.gameObject);
        Destroy(gameObject);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Mathf.Abs(mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
