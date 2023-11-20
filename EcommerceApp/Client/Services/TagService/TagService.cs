
using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;
using System;
using System.Net.Http.Json;

namespace EcommerceApp.Client.Services.TagService
{
    public class TagService : ITagService
    {
        private readonly HttpClient _http;
        // This event will be used to notify subscribers that the products have changed
        public event Action TagsChanged;
        public string Message { get; set; } = "Loading tags...";
        public List<TagDto> Tags { get; set; } = new List<TagDto>();
        public TagService(HttpClient http)
        {
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

        public async Task GetAllTagsAsync()
        {
            var result =
             await _http.GetFromJsonAsync<ServiceResponse<List<TagDto>>>("api/tags");

            if (result != null && result.Data != null)
            {
                Tags = result.Data;
            }

            // Could set some 404 Message "Nothing Here Yet" or something like that
            if (Tags.Count == 0)
                Message = "No tags found.";
            else
                Message = string.Empty;
            // Notify subscribers that the tags have changed
            TagsChanged?.Invoke();
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
