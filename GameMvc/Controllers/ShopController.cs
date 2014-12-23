using System.Linq;
using System.Web.Mvc;
using GameLogic.Characters.CharacterHelpers;
using GameLogic.Game;
using GameLogic.Shop;

namespace GameMvc.Controllers
{
    public class ShopController : Controller
    {
        [HttpPost]
        public ActionResult PurchaseEquipment(string name)
        {
            var g = (Game)Session["Game"];
            var s = new Shop();
            var e = s.Equipment.First(i => i.Name == name);
            if (e == null)
            {
                // TODO - Exception
            }
            EquipmentHelper.PurchaseEquipment(g.Player, e);
            s.AddPlayerToShop(g.Player);
            Session["Game"] = g;

            return View("~/Views/Home/Shop.cshtml", s);
        }

        [HttpPost]
        public ActionResult SellEquipment(string name)
        {
            var g = (Game)Session["Game"];
            var s = new Shop();
            var e = g.Player.CharacterEquipment.First(i => i.Name == name);
            if (e == null)
            {
                // TODO - Exception
            }
            EquipmentHelper.SellEquipment(g.Player, e);
            s.AddPlayerToShop(g.Player);
            Session["Game"] = g;

            return View("~/Views/Home/Shop.cshtml", s);
        }
    }
}