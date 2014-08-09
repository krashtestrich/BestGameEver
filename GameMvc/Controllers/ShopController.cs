using System.Linq;
using System.Web.Mvc;
using GameLogic.Characters.Player;
using GameLogic.Shop;

namespace GameMvc.Controllers
{
    public class ShopController : Controller
    {
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

            return View("~/Views/Home/Shop.cshtml", s);
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

            return View("~/Views/Home/Shop.cshtml", s);
        }
    }
}