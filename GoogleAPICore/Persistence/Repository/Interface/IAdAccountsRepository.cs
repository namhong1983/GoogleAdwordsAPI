using Google.Api.Ads.AdWords.Lib;
using GoogleAPICore.Controllers.Resources;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleAPICore.Persistence.Repository.Interface
{
    public interface IAdAccountsRepository
    {
        IEnumerable<GetAdAccountsResponse> GetAdAccountsTree(AdWordsUser user);
        IEnumerable<GetAdAccountsResponse> GetAllAccounts(AdWordsUser user);
    }
}
 