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
            Get["/stylists/{id}"]= parameters => {
                Stylist newStylist = Stylist.Find(parameters.id);
                Dictionary<string, object> model = ModelMaker();
                model.Add("Stylist Object", newStylist);
                model.Add("Client List", Client.GetByStylist(newStylist.GetId()));
                return View["stylist.cshtml", model];
            };

            Delete["/client/{id}/delete"] = parameters => {
              Client.DeleteClient(parameters.id);
              return View["client.cshtml", ModelMaker()];
            };

            Get["/clients/{id}"] = parameters => {
                Client newClient = Client.Find(parameters.id);
                Dictionary<string, object> model = ModelMaker();
                model.Add("Client Object", newClient);
                model.Add("Stylist Object", Stylist.Find(newClient.GetStylistId()));
                return View["client.cshtml", model];
            };

            Post["/stylist-add"] = _ => {
                Stylist newStylist = new Stylist(Request.Form["stylist_input"]);
                newStylist.Save();
                return View["stylist.cshtml", ModelMaker()];
            };

            Post["/client-add"] = _ => {
                string stylistInput = Request.Form["stylist_input"];
                int stylistId;

                if(Stylist.FindByName(stylistInput).GetName() == null)
                {
                    Stylist newStylist = new Stylist(stylistInput);
                    newStylist.Save();
                    stylistId = newStylist.GetId();
                }
                else
                {
                    stylistId = Stylist.FindByName(stylistInput).GetId();
                }

                Client newClient = new Client(Request.Form["client_input"], stylistId);
                newClient.Save();
                return View["client.cshtml", ModelMaker()];
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
