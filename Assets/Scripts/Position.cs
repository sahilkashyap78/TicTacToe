using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] m_TextureList = new Texture2D[2];
    public int PositionStatus = 2;
    public int RowStatus;
    public int ColumnStatus;
    public MeshRenderer MeshRenderer;
    public bool IsFilled;
    // Start is called before the first frame update
    void Start()
    {
        IsFilled = false;
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetPositionTexture()
    {
        MeshRenderer.sharedMaterial.mainTexture = m_TextureList[PositionStatus];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
