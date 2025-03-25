using System.Collections.Generic;
using UnityEngine;

public class ConveyorMoving : MonoBehaviour
{
    public List<GameObject> spawnedProducts;
    [SerializeField] private TutorialAnimation tutorialAnimation;

    public Transform startPos;
    public Transform endPos;

    public int MaxProductsCount => 30;
    public bool IsConveyorMoving { get; set; } = false;

    [SerializeField] float moovingSpeed = 1f;

    private void Update()
    {
        if (tutorialAnimation.IsCanOther)
            IsConveyorMoving = true;

        if (IsConveyorMoving)
        MovingProducts();
    }

    private void MovingProducts()
    {
        for (int i = spawnedProducts.Count - 1; i >= 0; i--)
        {
            GameObject product = spawnedProducts[i];

            if (product != null)
            {
                product.transform.position = Vector3.MoveTowards(product.transform.position, endPos.position, moovingSpeed * Time.deltaTime);

                if (Vector3.Distance(product.transform.position, endPos.position) < 0.1f)
                {
                    spawnedProducts.RemoveAt(i);
                    Destroy(product);
                }
            }

        }
    }
}
