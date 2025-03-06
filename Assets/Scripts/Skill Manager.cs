using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private GameObject[] points;
    public int pointIndex;
    private void Awake(){
        for(int i=0;i<points.Length;i++){
            points[i].SetActive(false);
        }
    }
    public void addPoint(int add){
        while(pointIndex<points.Length && add>0){
            points[pointIndex].SetActive(true);
            add--;
            pointIndex++;
        }
    }
}
