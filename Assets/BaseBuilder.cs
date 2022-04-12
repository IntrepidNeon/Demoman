using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilder : MonoBehaviour
{
    public GameObject Beam;

    public int StructureComplexityX;
    public int StructureComplexityY;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 Position;
        Quaternion RotationV = Quaternion.Euler(0f, 0f, 0f);
        Quaternion RotationH = Quaternion.Euler(0f, 0f, 90f);

        float BHeight = Beam.transform.localScale.y;
        float BWidth = Beam.transform.localScale.x;

        float ConstructPosX;
        float ConstructPosY;

        int[,] StablePoints = new int[StructureComplexityX, StructureComplexityY];

        for (int i = 0; i < StructureComplexityX; i++)
        {
            StablePoints[i, 0] = 1;
        }

        for (int y = 1; y < StructureComplexityY; y++)
        {
            ConstructPosY = (y - 1)*(BHeight + BWidth);

            for (int x = 0; x < StructureComplexityX-1; x++)
            {
                ConstructPosX = (int)BHeight * (x - (StructureComplexityX / 2));

                int Generator = Mathf.RoundToInt(Random.Range(0, 10));

                Position = new Vector3(transform.position.x + ConstructPosX, transform.position.y + ConstructPosY + (BHeight/2), transform.position.z);

                if (Generator != 5 && StablePoints[x, y - 1] == 1 && StablePoints[x + 1, y - 1] == 1)
                {
                    Instantiate(Beam, Position, RotationV);
                    GameObject HBeam = Instantiate(Beam, new Vector3(Position.x + (BHeight / 2), Position.y + (BHeight/2)+(BWidth/2), Position.z), RotationH);
                    HBeam.transform.localScale = new Vector3(BWidth, BHeight + BWidth, 0f);
                    Instantiate(Beam, new Vector3(Position.x + BHeight, Position.y, Position.z), RotationV);

                    StablePoints[x, y] = 1;

                    if(x+1< StructureComplexityX)
                    {
                        StablePoints[x + 1, y] = 1;
                    }
                    x++;
                }
                else
                {
                    StablePoints[x, y] = 0;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
