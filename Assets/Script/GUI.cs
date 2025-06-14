using UnityEngine;

public class GUInterface : MonoBehaviour
{
    [SerializeField] private Rigidbody targetRigitbody;
    [SerializeField] private BodyPhysics bodyPhysics;
    private void OnGUI()
    {
        if (GUILayout.Button("Apply Force") && targetRigitbody != null)
        {
            targetRigitbody.AddForce(transform.right * 10, ForceMode.Force);
        }
        if (GUILayout.Button("Apply Custom Force") && bodyPhysics != null)
        {
            bodyPhysics.AddForce(transform.right * 10);
        }
    }

}
