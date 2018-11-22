using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuController : MenuController {

    protected override void setPause()
    {
        canPause = false;
    }
}
