using System;
using UnityEngine;

public class IKFootSolverMouse : MonoBehaviour
{
    [SerializeField] private LayerMask terrainLayer;
    [SerializeField] private Transform body;
    [SerializeField] private IKFootSolverMouse otherFoot;
    [SerializeField] private float speed = 5, stepDistance = .3f, stepLength = .3f, stepHeight = .3f;
    [SerializeField] private Vector3 footPosOffset, footRotOffset;

    private float _footSpacing, _lerp;
    private Vector3 _oldPos, _currentPos, _newPos;
    private Vector3 _oldNorm, _currentNorm, _newNorm;
    private bool _isFirstStep = true;
    
    void Start()
    {
        _footSpacing = transform.localPosition.x;
        _currentPos = _newPos = _oldPos = transform.position;
        _currentNorm = _newNorm = _oldNorm = transform.up;
        _lerp = 1;
    }

    void Update()
    {
        transform.position = _currentPos + footPosOffset;
        transform.rotation = Quaternion.LookRotation(_currentNorm) * Quaternion.Euler(footRotOffset);

        Ray ray = new Ray(body.position + (body.right * _footSpacing) + (Vector3.up * 2), Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 10, terrainLayer.value))
        {
            if (_isFirstStep || (Vector3.Distance(_newPos, hit.point) > stepDistance && !otherFoot.IsMoving() 
                && !IsMoving()))
            {
                _lerp = 0;
                int direction = body.InverseTransformPoint(hit.point).z > body.InverseTransformPoint(_newPos).z
                    ? 1
                    : -1;
                _newPos = hit.point + (body.forward * (direction * stepLength));
                _newNorm = hit.normal;
            }
        }

        if (_lerp < 1)
        {
            Vector3 tempPos = Vector3.Lerp(_oldPos, _newPos, _lerp);
            tempPos.y += Mathf.Sin(_lerp * Mathf.PI) * stepHeight;

            _currentPos = tempPos;
            _currentNorm = Vector3.Lerp(_oldNorm, _newNorm, _lerp);
            _lerp += Time.deltaTime * speed;
        }
        else
        {
            _oldPos = _newPos;
            _oldNorm = _newNorm;
        }

        _isFirstStep = false;
    }

    public bool IsMoving()
    {
        return _lerp < 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_newPos, 0.1f);
    }
}
