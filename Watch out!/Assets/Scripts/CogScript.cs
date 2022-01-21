using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogScript : MonoBehaviour
{
    [SerializeField] private BreakManager bm;
    [SerializeField] private Color bad, broken;
    public int BrokenStage { get; set; }

    public void Deteriorate()
    {
        GetComponent<SpriteRenderer>().color = bad;
        StartCoroutine(Break());
    }

    private IEnumerator Break()
    {
        float time = Random.Range(4f, 8f);
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = broken;
        BrokenStage++;
    }

    public void FixCog()
    {
        StopCoroutine(Break());
        if(BrokenStage > 0)
            BrokenStage--;

        if(BrokenStage == 1)
            Deteriorate();
        else if(BrokenStage == 0)
        {
            GetComponent<SpriteRenderer>().color = GameManager.OnColor;
            bm.AddCog(gameObject);
        }
    }
}
