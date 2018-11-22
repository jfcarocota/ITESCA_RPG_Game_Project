using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MenuController {

    protected override void setPause()
    {
        canPause = true;
    }

}
