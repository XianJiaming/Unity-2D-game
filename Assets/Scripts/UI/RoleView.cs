using UnityEngine;
using UnityEngine.UI;

//该类暂时没用
 public class RoleView
{
    GameObject go;
    Text nameText;
    Image iconImg;
   
    


    public void SetGo(GameObject go)
    {
        this.go = go;
        UIUtil.SetUIOnClick(go, OnClick);

        nameText = UIUtil.GetText(go, "name");
        iconImg = UIUtil.GetImage(go, "iconImg");
    }

    public void SetId(int id)
    {
        
    }

    void OnClick( GameObject go = null)
    {

    }


}

