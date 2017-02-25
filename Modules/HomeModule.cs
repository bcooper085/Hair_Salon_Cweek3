using Nancy;

using System.Collections.Generic;

namespace BarksApp
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                return View["index.cshtml"];
            };

            Get["/stylists"] = _ => {
                List<Stylist> allStylists = Stylist.GetAll();
                return View["stylist.cshtml", allStylists];
            };

            Post["/stylist"] = _ => {
                Stylist newStylist = new Stylist(Request.Form["stylist_input"]);
                newStylist.Save();
                List<Stylist> allStylist = Stylist.GetAll();
                return View["stylist.cshtml", allStylist];
            };

            Get["/clients"] = _ => {
                List<Client> allClients = Client.GetAll();
                return View["client.cshtml", allClients];
            };
            // Post["/client"] = _ => {
            //     Client newClient = new Client(Request.Form["client_input"], Request.Form["choose-stylist"]);
            //     newClient.Save();
            //     return View["success.cshtml", newClient];
            // };
            // Get["/stylists/{id}"]= parameters => {
            //     Stylist foundStylist = Stylist.Find(parameters.id);
            //     Dictionary<string, object> model = ModelMaker();
            //     model.Add("Stylists", foundStylist);
            //     model.Add("Client", Stylist.GetClient());
            //     return View["stylist.cshtml", model];
            // };

            // Get["/client/{id}"] = parameters => {
            //
            // }


            // Get["/{stylistId}/clients/{id}"] = parameters => {
            //     Client foundClient = Client.Find(parameters.id);
            //     Dictionary<string, object> model = ModelMaker();
            //     model.Add("Clients", foundClient);
            //     return View["client.cshtml", model];
            // };

            Post["/delete-all"] = _ => {
                Stylist.DeleteAll();
                return View["index.cshtml"];
            };

            // Delete["/client/{id}/delete"] = parameters => {
            //     Client.DeleteClient(parameters.id);
            //     return View["client.cshtml", ModelMaker()];
            // };
        }
    }
}
