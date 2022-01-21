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
        float time = Random.Range(1f, 3.5f);
        yield return new WaitForSeconds(time);
        GetComponent<SpriteRenderer>().color = broken;
        BreakManager.numOfBrokenCogs++;
        if(BreakManager.numOfBrokenCogs == 4)
        {
            GameManager.loseState = true;
        }
        BrokenStage++;
    }

    public void FixCog()
    {
        StopAllCoroutines();
        if(BrokenStage > 0)
            BrokenStage--;

        if (BrokenStage == 1)
        {
            BreakManager.numOfBrokenCogs--;
            Deteriorate();
        }

        else if(BrokenStage == 0)
        {
            GetComponent<SpriteRenderer>().color = GameManager.OffColor;
            bm.AddCog(gameObject);
        }
    }
}
