using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridRenderer : Graphic
{

    public Vector2Int gridSize = new Vector2Int(1, 1);

    public float thickness = 10;

    UIVertex vert;

    float cellWidth;
    float cellHeight;

    protected override void OnPopulateMesh(VertexHelper vh)
    {

        vh.Clear();

        vert = UIVertex.simpleVert;

        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;

        cellWidth = width / gridSize.x;
        cellHeight = height / gridSize.y;

        int count = 0;
        for(int y=0; y<gridSize.y; y++)
        {
            for(int x=0; x<gridSize.x; x++)
            {
                DrawCell(x, y, count, vh);

                count++;
            };
        }
        /*
        //Draw outer vertices
        vert.position = new Vector3(0, 0);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(0, height);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(width, height);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(width, 0);
        vert.color = color;
        vh.AddVert(vert);

        //Draw inner vertices
        //The inner vertex is the diagonal position 
        //So we do some pythag to know how much to move by in x and y

        float widthSqr = thickness * thickness;
        float distanceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distanceSqr);

        //Debug.Log($"{widthSqr}, {distanceSqr}, {distance}");

        vert.position = new Vector3(distance, distance);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(distance, height-distance);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(width-distance, height-distance);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(width-distance, distance);
        vert.color = color;
        vh.AddVert(vert);

        //Left Edge
        vh.AddTriangle(0, 1, 5);
        vh.AddTriangle(5, 4, 0);

        //Top Edge
        vh.AddTriangle(1, 2, 6);
        vh.AddTriangle(6, 5, 1);

        //Right Edge
        vh.AddTriangle(2, 3, 7);
        vh.AddTriangle(7, 6, 2);

        //Bottom Edge
        vh.AddTriangle(3, 0, 4);
        vh.AddTriangle(4, 7, 3);
        */
    }

    void DrawCell(int x, int y, int index, VertexHelper vh)
    {

        float xPos = cellWidth * x;
        float yPos = cellHeight * y;

        //Draw outer vertices
        vert.position = new Vector3(xPos, yPos);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(xPos, yPos + cellHeight);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(xPos + cellWidth, yPos+cellHeight);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(xPos + cellWidth, yPos);
        vert.color = color;
        vh.AddVert(vert);

        //Draw inner vertices
        //The inner vertex is the diagonal position 
        //So we do some pythag to know how much to move by in x and y

        float widthSqr = thickness * thickness;
        float distanceSqr = widthSqr / 2f;
        float distance = Mathf.Sqrt(distanceSqr);

        //Debug.Log($"{widthSqr}, {distanceSqr}, {distance}");

        vert.position = new Vector3(xPos + distance, yPos + distance);
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(xPos + distance, yPos + (cellHeight-distance));
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(xPos + (cellWidth-distance), yPos + (cellHeight-distance));
        vert.color = color;
        vh.AddVert(vert);

        vert.position = new Vector3(xPos + (cellWidth-distance), yPos + distance);
        vert.color = color;
        vh.AddVert(vert);

        int count = index * 8;
        //Left Edge
        vh.AddTriangle(count + 0, count + 1,  count + 5);
        vh.AddTriangle(count + 5, count + 4, count + 0);

        //Top Edge
        vh.AddTriangle(count + 1, count + 2, count + 6);
        vh.AddTriangle(count + 6, count + 5, count + 1);

        //Right Edge
        vh.AddTriangle(count + 2, count + 3, count + 7);
        vh.AddTriangle(count + 7, count + 6, count + 2);

        //Bottom Edge
        vh.AddTriangle(count + 3, count + 0, count + 4);
        vh.AddTriangle(count + 4, count + 7, count + 3);
    }

}
