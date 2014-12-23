using System.Linq;
using System.Web.Mvc;
using GameLogic.Game;

namespace GameMvc.Controllers
{
    public class CharacterController : Controller
    {
        [HttpPost]
        public ActionResult SelectSkill(string skillName)
        {
            var g = (Game)Session["Game"];
            var skill = g.Player.SkillTree.Get().First(i => i.Name == skillName);
            g.Player.ChooseSkill(skill);
            //var tournamentPlayer = g.Tournament.Participants.First(p => p.Character is Player);
            //var skill = tournamentPlayer.Character.SkillTree.Get().First(i => i.Name == skillName);
            //((Player) tournamentPlayer.Character).ChooseSkill(skill);
            //g.Player = tournamentPlayer.Character as Player;
            Session["Game"] = g;
            return View("~/Views/Home/Character/SkillTree.cshtml", g.Player);
        }
    }
}