using UnityEngine;
using UnityEngine.Events;

public class Brick : MonoBehaviour
{
    public UnityEvent<int> onDestroyed;
    
    public int PointValue;

    [SerializeField] private Color color1 = Color.yellow;
    [SerializeField] private Color color2 = Color.green;
    [SerializeField] private Color color3 = new Color(1f, 0.666f, 0f);
    [SerializeField] private Color color4 = Color.red;



    void Start()
    {
        var renderer = GetComponentInChildren<Renderer>();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        switch (PointValue)
        {
            case 1 :
                block.SetColor("_BaseColor", color1);
                break;
            case 3:
                block.SetColor("_BaseColor", color2);
                break;
            case 5:
                block.SetColor("_BaseColor", color3);
                break;
            case 10:
                block.SetColor("_BaseColor", color4);
                break;
            default:
                block.SetColor("_BaseColor", Color.white);
                break;
        }
        renderer.SetPropertyBlock(block);
    }

    private void OnCollisionEnter(Collision other)
    {
        if(GameManager.Instance ? GameManager.Instance.isGameActive : true)
        {
            onDestroyed.Invoke(PointValue);

            //slight delay to be sure the ball have time to bounce
            Destroy(gameObject, 0.2f);

        }
        
    }


}
