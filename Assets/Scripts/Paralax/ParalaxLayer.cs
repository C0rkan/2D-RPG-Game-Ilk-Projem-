using UnityEngine;

[System.Serializable]
public class ParalaxLayer
{
    [SerializeField] private Transform background;
    [SerializeField] private float paralaxMultipiler;
    [SerializeField] private float iamgeWidthOffset = 10; 

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth() {
        imageFullWidth = background.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float DistanceToMove) {

        background.position += Vector3.right * (DistanceToMove * paralaxMultipiler);
    }

    public void LoopBackground(float cameraLeftEdge, float cameraRightEdge) {

        float imageRightEdge = (background.position.x + imageHalfWidth) - iamgeWidthOffset;
        float imageLeftEdge = (background.position.x - imageHalfWidth) + iamgeWidthOffset;

        if(imageLeftEdge > cameraRightEdge) {
            background.position += Vector3.right * -imageFullWidth;
        }

        if (imageRightEdge < cameraLeftEdge) {
            background.position += Vector3.right * imageFullWidth;
        } 
    }


}
