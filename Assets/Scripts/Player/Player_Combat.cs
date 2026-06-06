using UnityEngine;

public class Player_Combat : Entity_Combat {

    [Header("Counter Attack Details")]
    [SerializeField] private float counterRecovery = .1f;

    public bool CounterAttackPerformed() {

        bool hasPreformCounter = false;

        foreach (var target in GetDetectedColliders()) {
            ICounterable counterable = target.GetComponent<ICounterable>();
            if (counterable == null) {
                continue;
            }
            
            if (counterable.CanBeCountered) {
                counterable.HandleCounter();
                hasPreformCounter = true;
            }
        }

        return hasPreformCounter;
    }

    public float GetCounterRecoveryDuration() => counterRecovery;

}
