using System;
using Unity.VisualScripting;
using UnityEngine;

public class ParalaxBackground : MonoBehaviour {
    
    private Camera mainCamera;
    private float lastCameraPositionX;
    private float cameraHalfWidth; 


    [SerializeField] private ParalaxLayer[] backgroundLayer;

    private void Awake() {
        mainCamera = Camera.main;
        cameraHalfWidth += mainCamera.orthographicSize * mainCamera.aspect;
        CalculateImageLenght();
    }


    private void FixedUpdate() {
        float currentCameraPositionX = mainCamera.transform.position.x;
        float distanceToMove = currentCameraPositionX - lastCameraPositionX;
        lastCameraPositionX = currentCameraPositionX;

        float cameraLeftEdge = currentCameraPositionX - cameraHalfWidth;
        float cameraRightEdge = currentCameraPositionX + cameraHalfWidth;

        foreach ( ParalaxLayer layer in backgroundLayer ) {
            layer.Move(distanceToMove);
            layer.LoopBackground(cameraLeftEdge, cameraRightEdge);
        }

    }

    private void CalculateImageLenght() {
        foreach (ParalaxLayer layer in backgroundLayer) {
            layer.CalculateImageWidth();
        }
    }

}
