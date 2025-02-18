﻿using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using DermDiag.Models;
using DermDiag.DTO;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using static System.Net.WebRequestMethods;

namespace DermDiag.Repository
{
    public class PaymentRepository
    {
        private readonly DermDiagContext _context;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "ZXlKaGJHY2lPaUpJVXpVeE1pSXNJblI1Y0NJNklrcFhWQ0o5LmV5SmpiR0Z6Y3lJNklrMWxjbU5vWVc1MElpd2ljSEp2Wm1sc1pWOXdheUk2T1RneE9Ea3lMQ0p1WVcxbElqb2lhVzVwZEdsaGJDSjkuamxNYURZalY3eDF6ZXJnNEJrd09xZTU2dzJ4MnZINC1nQzBpSE9mTkxUZ00wMDNyMC1MUnBOY3RlSGk5eXhkdWNxOEtIZ3dobVhNTDVkVHVkb21HQUE=";
        private readonly int _integrationID = 4598284; 

        public PaymentRepository(DermDiagContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateAsync()
        {
            var data = new { api_key = _apiKey };
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://accept.paymob.com/api/auth/tokens", content);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseString);

            return jsonResponse.GetProperty("token").GetString();
        }

        public async Task<int> CreateOrderAsync(string token,decimal? Amount)
        {
            var data = new
            {
                auth_token = token,
                delivery_needed = "true",
                amount_cents = Amount * 100,
                currency = "EGP",
                is_live = false,
                payment_methods = new[] { _integrationID },
                items = new object[] { }
            };
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");


            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://accept.paymob.com/api/ecommerce/payment-links");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseString);

            return jsonResponse.GetProperty("id").GetInt32();
        }

        public async Task<string> GetPaymentStatusAsync(int paymentId)
        {
            var requestUrl = $"https://accept.paymobsolutions.com/api/acceptance/transactions/{paymentId}";
            var request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var paymentStatusResponse = await response.Content.ReadFromJsonAsync<string>();
            return paymentStatusResponse;
        }

        public async Task<Wallet> GetWalletByIdAsync(int walletId)
        {
            return await _context.Wallets.FindAsync(walletId);
        }

        public async Task<bool> UpdateWalletAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> AddPaymentRecordAsync(Payment payment)
        {
            _context.Payments.Add(payment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
