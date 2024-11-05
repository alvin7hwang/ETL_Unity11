using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] XRInputValueReader<Vector2> _tapStartPositionInput = new XRInputValueReader<Vector2>("Tap Start Position");
    [SerializeField] GameObject _ballPrefab;
    [SerializeField] Transform _xrCamera;

    private void Start()
    {
        _tapStartPositionInput.inputActionReference.action.started += OnTapStartPositionInputStarted;
        //_tapStartPositionInput.inputActionReference.action.started += (context) =>
        //{
        //    Vector2 tapPosition = context.ReadValue<Vector2>();
        //    Debug.Log($"Tapped {tapPosition}");
        //
        //    GameObject ball = Instantiate(_ballPrefab, _xrCamera.position + _xrCamera.forward * 0.5f, _xrCamera.rotation);
        //    ball.GetComponent<Rigidbody>().AddForce(_xrCamera.forward * 500f, ForceMode.Force);
        //};
    }

    private void OnTapStartPositionInputStarted(InputAction.CallbackContext context)
    {
        Vector2 tapPosition = context.ReadValue<Vector2>();
        Debug.Log($"Tapped {tapPosition}");

        GameObject ball = Instantiate(_ballPrefab, _xrCamera.position + _xrCamera.forward * 0.5f, _xrCamera.rotation);
        ball.GetComponent<Rigidbody>().AddForce(_xrCamera.forward * 500f, ForceMode.Force);
        // ForceMode
        // Force : F(��) = m(����) x a(���ӵ�)  // ������ �������� ���ӵ��� �������� �Ϲ����� ��
        // Acceleration : a(���ӵ�) // ������ ������� ���ӵ� ����
        // Impulse : I(��ݷ�) = F(��) x t(�ð�) = m(����) x a(���ӵ�) x t(�ð�) = m(����) x v(�ӵ�) // ������ �������� �ӵ��� �������� �Ϲ����� ��ݷ�
        // VelocityChange = v(�ӵ�) // ������ ������� �ӵ� ����
    }
}
