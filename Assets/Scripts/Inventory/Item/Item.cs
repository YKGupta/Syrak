using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject interactionUIGO;
    public new string name;
    public int id;
    public Sprite sprite;

    public event Action<Item> clicked;
    public event Action<Item> entered;
    public event Action<Item> exited;

    private void OnMouseOver()
    {
        if(!enabled || entered == null)
            return;

        entered(this);
    }

    private void OnMouseDown()
    {
        if(!enabled || clicked == null)
            return;

        clicked(this);
    }

    private void OnMouseExit()
    {
        if(!enabled || exited == null)
            return;
        
        exited(this);
    }
}
