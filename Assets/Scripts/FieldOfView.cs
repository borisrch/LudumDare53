using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

  [SerializeField] LayerMask _layerMask;
  Mesh _mesh;
  Vector3 _origin;
  float _startingAngle;
  float _fov;
  float _viewDistance;

  void Start()
  {
    _origin = Vector3.zero;
    _fov = 90f;
    _viewDistance = 10f;

    _mesh = new Mesh();
    GetComponent<MeshFilter>().mesh = _mesh;
  }

  void LateUpdate()
  {
    int rayCount = 50;
    float angle = _startingAngle;
    float angleIncrease = _fov / rayCount;

    // Number of vertices = Ray Count + Orign + Ray 0
    Vector3[] vertices = new Vector3[rayCount + 1 + 1];
    Vector2[] uv = new Vector2[vertices.Length];
    // As many triangle as Rays, Triangles need 3 points
    int[] triangles = new int[rayCount * 3];

    vertices[0] = _origin;

    int vertexIndex;
    int triangleIndex = 0;
    for (int i = 0; i <= rayCount; i++)
    {
      vertexIndex = i + 1;
      Vector3 vertex;
      Vector3 angledDirection = GetVectorFromAngle(angle);
      RaycastHit2D raycastHit2D = Physics2D.Raycast(_origin, angledDirection, _viewDistance, _layerMask);

      if (raycastHit2D.collider == null)
      {
        // No hit
        vertex = _origin + angledDirection * _viewDistance;
      }
      else
      {
        // Hit object
        vertex = raycastHit2D.point;
      }


      vertices[vertexIndex] = vertex;

      // Make Pologon Triangle
      if (i > 0)
      {
        triangles[triangleIndex + 0] = 0;
        triangles[triangleIndex + 1] = vertexIndex - 1;
        triangles[triangleIndex + 2] = vertexIndex;

        triangleIndex += 3;
      }

      // Negative makes it go clockwise
      angle -= angleIncrease;
    }

    _mesh.vertices = vertices;
    _mesh.uv = uv;
    _mesh.triangles = triangles;

  }



  public void SetOrigin(Vector3 origin)
  {
    this._origin = origin;
  }

  public void SetViewDirection(Vector3 aimDirection)
  {
    this._startingAngle = GetAngleFromVectorFloat(aimDirection) - _fov / 2f;
  }

  public void setFoV(float fov)
  {
    this._fov = fov;
  }

  public void setViewDistance(float viewDistance)
  {
    this._viewDistance = viewDistance;
  }




  public static Vector3 GetVectorFromAngle(float angle)
  {
    float angleRad = angle * (Mathf.PI / 180f);
    return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
  }

  public static float GetAngleFromVectorFloat(Vector3 dir)
  {
    dir = dir.normalized;
    float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    if (n < 0) n += 360;

    return n;
  }

}
