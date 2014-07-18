using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameLogic;
using GameLogic.Arena;
using GameLogic.Battle;
using GameLogic.Characters.Player;
using GameLogic.Shop;
using GameMvc.Models;

namespace GameMvc.Controllers
{
    public class GameController : Controller
    {
        //
        // GET: /Game/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCharacter(UICharacterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }
            var p = new Player();
            p.SetName(model.Name);
            Session["Player"] = p;
            return View("Character", p);
        }

        public ActionResult Character()
        {
            var p = (Player)Session["Player"];
            if (p == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }            
            return View("Character", p);            
        }

        public ActionResult Arena()
        {
            var p = (Player)Session["Player"];
            if (p == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }

            var a = new Arena(new Battle());
            a.BuildArenaFloor(5);
            a.AddCharacterToArena(p);
            Session["Arena"] = a;

            return View("Arena", a);
        }

        public ActionResult Shop()
        {
            var p = (Player)Session["Player"];
            if (p == null)
            {
                ModelState.AddModelError(string.Empty, "You must create a character first.");
                return View("Index");
            }    
            var s = new Shop();
            s.AddPlayerToShop(p);

            return View("Shop", s);
        }

        [HttpPost]
        public ActionResult PurchaseEquipment(string name)
        {
            var p = (Player)Session["Player"];
            var s = new Shop();
            var e = s.Equipment.First(i => i.Name == name);
            if (e == null)
            {
                // TODO - Exception
            }
            p.PurchaseEquipment(e);
            s.AddPlayerToShop(p);
            Session["Player"] = p;

            return View("Shop", s);
        }

        [HttpPost]
        public ActionResult SellEquipment(string name)
        {
            var p = (Player)Session["Player"];
            var s = new Shop();
            var e = p.CharacterEquipment.First(i => i.Name == name);
            if (e == null)
            {
                // TODO - Exception
            }
            p.SellEquipment(e);
            s.AddPlayerToShop(p);
            Session["Player"] = p;

            return View("Shop", s);
        }
    }
}
