using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamilyTreeCanvasWindowController : CanvasWindowController
{
    public override void OpenWindow()
    {
        // Should first tween window to canvas size
        base.OpenWindow();
    }

    public override void CloseWindow()
    {
        // Should first tween window to original size
        base.CloseWindow();
    }
}
