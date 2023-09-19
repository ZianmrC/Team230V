using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WireGenerator : MonoBehaviour
{
    public RectTransform startAnchor;
    public RectTransform endAnchor;
    public int numControlPoints = 5;

    private List<Vector2> controlPoints;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        controlPoints = GenerateRandomControlPoints();
        DrawWire();
    }

    List<Vector2> GenerateRandomControlPoints()
    {
        List<Vector2> points = new List<Vector2>();
        points.Add(startAnchor.anchoredPosition);

        for (int i = 0; i < numControlPoints; i++)
        {
            // Generate random control points within certain constraints
            Vector2 randomPoint = new Vector2(Random.Range(400f, 1200f), Random.Range(200f, 800f));
            points.Add(randomPoint);
            Debug.Log($"{randomPoint.x}, {randomPoint.y}");
        }

        points.Add(endAnchor.anchoredPosition);
        return points;
    }

    void DrawWire()
    {
        lineRenderer.positionCount = controlPoints.Count;

        for (int i = 0; i < controlPoints.Count; i++)
        {
            Vector2 worldPos = Vector2.zero;

            // Convert RectTransform anchored positions to world positions
            if (i == 0)
            {
                worldPos = startAnchor.TransformPoint(controlPoints[i]);
            }
            else if (i == controlPoints.Count - 1)
            {
                worldPos = endAnchor.TransformPoint(controlPoints[i]);
            }
            else
            {
                worldPos = controlPoints[i];
            }

            lineRenderer.SetPosition(i, worldPos);
        }
    }
    public Vector2 QuadraticCurve(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = Vector2.Lerp(a, b, t);
        Vector2 p1 = Vector2.Lerp(b, c, t);
        return Vector2.Lerp(p0, p1, t);
    }
}
