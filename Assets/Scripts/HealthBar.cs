using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Extensions;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;
    [SerializeField] private Image _fill;
    private IHealth _health;
    private IDeath _death;

    private void Start()
    {
        _health = _targetObject.GetComponent<IHealth>();
        _death = _targetObject.GetComponent<IDeath>();
        _health.Changed += OnHealthChanged;
        _death.Happened += OnDeathHappened;
        OnHealthChanged();
    }

    private void OnDestroy()
    {
        _health.Changed -= OnHealthChanged;
        _death.Happened -= OnDeathHappened;
    }

    public void Initialize(Vector3 cameraPosition) =>
        LookAtTarget(cameraPosition);

    private void LookAtTarget(Vector3 positionToLookAt)
    {
        positionToLookAt.SetX(transform.position.x);
        transform.rotation = Quaternion.LookRotation(positionToLookAt - transform.position);
    }

    private void OnHealthChanged() => 
        _fill.fillAmount = _health.Current / _health.Max;

    private void OnDeathHappened() => 
        Hide();

    private void Hide() => 
        transform.DOScale(Vector3.zero, 0.2f);
}