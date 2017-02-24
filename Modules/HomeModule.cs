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
                return View["stylist.cshtml", ModelMaker()];
            };

            Get["/stylists/{id}"]= parameters => {
                Stylist newStylist = Stylist.Find(parameters.id);
                Dictionary<string, object> model = ModelMaker();
                model.Add("Stylist Object", newStylist);
                model.Add("Client List", Client.GetByStylist(newStylist.GetId()));
                return View["stylist.cshtml", model];
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
