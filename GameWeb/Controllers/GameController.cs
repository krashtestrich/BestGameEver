using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameWeb.Controllers
{
    public class GameController : Controller
    {
        public ActionResult CreateCharacter(string name)
        {
            GameLogic.Character c = new GameLogic.Character();
            c.SetName(name);
            c.AddSlot(new GameLogic.Slots.Hand());
            c.AddSlot(new GameLogic.Slots.Hand());
        }
    }
}
