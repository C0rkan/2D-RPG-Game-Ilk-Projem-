using UnityEngine;
using UnityEngine.Rendering.Universal;

public interface ICounterable
{
    public bool CanBeCountered { get; }
    public void HandleCounter();
}
