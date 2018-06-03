using GoogleAPICore.API;
using GoogleAPICore.Controllers.Resources;
using GoogleAPICore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace GoogleAPICore.Controllers
{
    public class AdAccountsController : Controller
    {
        private readonly RestClientBase _clientBaseAddress;

        public AdAccountsController(IOptions<RestClientBase> clientBaseAddress)
        {
            _clientBaseAddress = clientBaseAddress.Value;
        }

        public IActionResult Index()
        {
            var model = new List<GetAdAccountsResponse>();
            model.Add(new GetAdAccountsResponse
            {
                CustomerId = 1,
                CustomerName = "Test Account"
            });

            return View(model);
        }
       
        public IActionResult DisplayPartialAccountList()
        {
            var model = new List<GetAdAccountsResponse>();
            var result  = AdAccountsAPI.GetAdAccounts(_clientBaseAddress.BaseURLApi);
            
            if (result == null)
            {
                model.Add(new GetAdAccountsResponse
                {
                    CustomerId = 1,
                    CustomerName = "Test Account"
                });
                model.Add(new GetAdAccountsResponse
                {
                    CustomerId = 2,
                    CustomerName = "Test Account 2"
                });
                model.Add(new GetAdAccountsResponse
                {
                    CustomerId = 3,
                    CustomerName = "Test Account 3"
                });
                return PartialView("DisplayListAccounts", model);
            }
            else
            {
                return PartialView("DisplayListAccounts", result);
            }
        }

        public IActionResult DisplayPartialAdsByAccount(long id)
        {
            var request = new GetAdsRequest();
            request.CustomerId = id;
            var result = AdsAPI.GetAds(request,_clientBaseAddress.BaseURLApi);

            return PartialView("Ads",result);        
        }
    }
}