using AutoMapper;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class FlutterwaveService : IFlutterwaveService
    {
        private readonly CoopBankingDataContext _context;
        private readonly IMapper _mapper;

        public FlutterwaveService(CoopBankingDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> GetAccountName(AccountEnquiryDTO account)
        {
            // string apiBaseUrl = "https://api.flutterwave.com/v3/accounts/resolve"; https://drapi.flutterwave.com"
            string apiBaseUrl = "https://drapi.flutterwave.com/flwv3-pug/getpaidx/api/resolve_account";
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl;

                using (var Response = await client.PostAsync(endpoint, content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        string Account = JsonConvert.SerializeObject(account);
                        var contents = await Response.Content.ReadAsStringAsync();
                        var AccountResponseDTO = JsonConvert.DeserializeObject(contents);
                        var response = JsonConvert.DeserializeObject<Dictionary<string, Object>>(contents);
                        var Last = response;
                        var first = response.FirstOrDefault().Key[0];
                        var second = response.FirstOrDefault().Key[1];
                        var status = response.FirstOrDefault(x => x.Key == "status");
                        var message = response.FirstOrDefault(x => x.Key == "message");

                        if (status.Value.Equals("success") && message.Value.Equals("ACCOUNT RESOLVED"))
                        {
                            var data = response.FirstOrDefault(x => x.Key == "data");

                            var last = data.Value.ToString();
                            var myDetails = JsonConvert.DeserializeObject<COOP.Banking.Data.DTO.Data>(last);
                            var name = myDetails.data.accountname;
                            return name;

                        }
                        else
                        {
                            return string.Empty;
                        }


                    }
                    else
                    {
                        return string.Empty;

                    }

                }
            }
            throw new NotImplementedException();
        }
    }
}
