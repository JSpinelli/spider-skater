using UnityEngine;

public class JointDetacher : MonoBehaviour
{
    public HingeJoint2D[] thingsToDisable = {};
    public DistanceJoint2D[] thingsToDisable2 = {};

    public void detachThings(){
        var  i = 0 ;
        while (i < thingsToDisable.Length){
            thingsToDisable[i].enabled = false;
            i++;
        }
        i = 0 ;
        while (i < thingsToDisable2.Length){
            thingsToDisable2[i].enabled = false;
            i++;
        }
    }
}
