
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System;
using System.Net.Http.Json;

namespace EcommerceApp.Client.Services.TagService
{
    public class TagService : ITagService
    {
        private readonly HttpClient _http;
        private readonly IHttpClientFactory _httpFactory;

        public TagService(HttpClient http, IHttpClientFactory httpFactory)
        {
            _httpFactory = httpFactory;
            _http = http;
        }

        public Task<ServiceResponse<TagDto>> CreateTagAsync(TagDto tag)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<bool>> DeleteTagByIdAsync(Guid tagId, bool confirmCascadeDelete)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<TagDto>>> GetAllTagsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<TagDto>> GetTagByIdAsync(Guid tagId)
        {
            try
            {
                // Using server api http client - mediated 
                var result = await _http.GetFromJsonAsync<ServiceResponse<TagDto>>($"api/tags/{tagId}");
                if (result != null && result.Data != null)
                {
                    return new ServiceResponse<TagDto>
                    {
                        Data = result.Data,
                        Message = result.Message,
                        Success = result.Success
                    };
                }
                else
                {
                    return new ServiceResponse<TagDto>
                    {
                        Success = false,
                        Message = "Failed to locate tag."
                    };
                }
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TagDto>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }


        public Task<ServiceResponse<TagDto>> UpdateTagByIdAsync(Guid tagId, TagDto tag)
        {
            throw new NotImplementedException();
        }
    }
}
