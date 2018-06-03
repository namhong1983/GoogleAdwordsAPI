using Google.Api.Ads.AdWords.Lib;
using Google.Api.Ads.AdWords.v201802;
using GoogleAPICore.Controllers.Resources;
using GoogleAPICore.Models;
using GoogleAPICore.Persistence.Repository.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleAPICore.Persistence.Repository
{
    public class AdAccountsRepository : IAdAccountsRepository
    {
        private readonly AdWordsConfig _config;

        public AdAccountsRepository(IOptions<AdWordsConfig> config)
        {
            _config = config.Value;
        }

        public IEnumerable<GetAdAccountsResponse> GetAdAccountsTree(AdWordsUser user)
        {
            using (ManagedCustomerService managedCustomerService = (ManagedCustomerService)user.GetService(AdWordsService.v201802.ManagedCustomerService))
            {
                // Create selector.
                Selector selector = new Selector();
                selector.fields = new String[]
                {
                    ManagedCustomer.Fields.CustomerId,
                    ManagedCustomer.Fields.Name
                };
                selector.paging = Paging.Default;

                // Map from customerId to customer node.
                Dictionary<long, ManagedCustomerTreeNode> customerIdToCustomerNode =
                    new Dictionary<long, ManagedCustomerTreeNode>();

                // Temporary cache to save links.
                List<ManagedCustomerLink> allLinks = new List<ManagedCustomerLink>();

                ManagedCustomerPage page = null;

                try
                {
                    do
                    {
                        page = managedCustomerService.get(selector);

                        if (page.entries != null)
                        {
                            // Create account tree nodes for each customer.
                            foreach (ManagedCustomer customer in page.entries)
                            {
                                ManagedCustomerTreeNode node = new ManagedCustomerTreeNode();
                                node.Account = customer;
                                customerIdToCustomerNode.Add(customer.customerId, node);
                            }

                            if (page.links != null)
                            {
                                allLinks.AddRange(page.links);
                            }
                        }
                        selector.paging.IncreaseOffset();
                    } while (selector.paging.startIndex < page.totalNumEntries);

                    // For each link, connect nodes in tree.
                    foreach (ManagedCustomerLink link in allLinks)
                    {
                        ManagedCustomerTreeNode managerNode =
                            customerIdToCustomerNode[link.managerCustomerId];
                        ManagedCustomerTreeNode childNode = customerIdToCustomerNode[link.clientCustomerId];
                        childNode.ParentNode = managerNode;
                        if (managerNode != null)
                        {
                            managerNode.ChildAccounts.Add(childNode);
                        }
                    }

                    // Find the root account node in the tree.
                    ManagedCustomerTreeNode rootNode = null;
                    foreach (ManagedCustomerTreeNode node in customerIdToCustomerNode.Values)
                    {
                        if (node.ParentNode == null)
                        {
                            rootNode = node;
                            break;
                        }
                    }

                    // Display account tree.
                    Console.WriteLine("CustomerId, Name");
                    Console.WriteLine(rootNode.ToTreeString(0, new StringBuilder()));
                }
                catch (Exception e)
                {
                    throw new System.ApplicationException("Failed to create ad groups.", e);
                }

                return null;
            }
        }

        public IEnumerable<GetAdAccountsResponse> GetAllAccounts(AdWordsUser user)
        {         
            ManagedCustomer customer = new ManagedCustomer();
            var result = new List<GetAdAccountsResponse>();
            using (ManagedCustomerService managedCustomerService = (ManagedCustomerService)user.GetService(AdWordsService.v201802.ManagedCustomerService))
            {
                try
                {
                    // Create selector.
                    Selector selector = new Selector();
                    selector.fields = new String[]
                    {
                        ManagedCustomer.Fields.CustomerId, ManagedCustomer.Fields.Name
                    };
                    selector.paging = Paging.Default;
                    ManagedCustomerPage accounts = null;
                    accounts = managedCustomerService.get(selector);

                    if (accounts.entries != null)
                    {
                        foreach (var account in accounts.entries)
                        {
                            result.Add(new GetAdAccountsResponse
                            {
                                CustomerId = account.customerId,
                                CustomerName = account.name
                            });
                        }
                    }
                    else
                    {
                        //Test Account
                        result.Add(new GetAdAccountsResponse
                        {
                            CustomerId = 3,
                            CustomerName = "Test account 3"
                        });
                        result.Add(new GetAdAccountsResponse
                        {
                            CustomerId = 4,
                            CustomerName = "Test account 4"
                        });
                    }
                }
                catch (Exception x)
                {
                    throw new Exception(x.Message);
                }
                return result;
            }
        }
    }
}
