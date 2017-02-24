using Nancy;
using System.Collections.Generic;

namespace BarksApp
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml", ModelMaker()];
            };

            Post["/stylist-add"] = _ => {
                Stylist newStylist = new Stylist(Request.Form["stylist_input"]);
                newStylist.Save();
                return View["index.cshtml", ModelMaker()];
            };

        }

        public static Dictionary<string, object> ModelMaker()
        {
            Dictionary<string, object> model = new Dictionary<string, object>{};
            model.Add("Stylists", Stylist.GetAll());
            model.Add("Clients", Client.GetAll());

            return model;
        }
    }
}