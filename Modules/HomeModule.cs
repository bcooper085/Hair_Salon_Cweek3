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

            Get["/stylist/{id}/clients"]= parameters => {
                Stylist currentStylist = Stylist.Find(parameters.id);
                List<Client> stylistClients = currentStylist.GetClient();
                Dictionary<string, object> model = new Dictionary<string, object>(){{"stylist", currentStylist}, {"clients", stylistClients}};
                return View["stylists-clients.cshtml", model];
            };

            Post["/stylist/{id}/clients"] = parameters => {
                Client newClient = new Client(Request.Form["client_input"], parameters.id);
                newClient.Save();
                Stylist currentStylist = Stylist.Find(parameters.id);
                List<Client> stylistClients = currentStylist.GetClient();
                Dictionary<string, object> model = new Dictionary<string, object>(){{"stylist", currentStylist}, {"clients", stylistClients}};
                return View["stylists-clients.cshtml", newClient];
            };

            Post["/client/new"] = parameters => {
                Client newClient = new Client(Request.Form["client_input"], Request.Form["choose-stylist"]);
                newClient.Save();
                Stylist currentStylist = Stylist.Find(parameters.id);
                List<Client> stylistClients = currentStylist.GetClient();
                Dictionary<string, object> model = new Dictionary<string, object>(){{"stylist", currentStylist}, {"clients", stylistClients}};
                return View["stylists-clients.cshtml", newClient];
            };

            
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
