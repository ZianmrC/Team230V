using UnityEngine;
using UnityEngine.UI;

public class ParentMediator : MonoBehaviour
{
    private Parent parent;
    public void setParent(Parent Parent){
        parent = Parent;
    }
    public Parent getParent(){
        return parent;
    }
}