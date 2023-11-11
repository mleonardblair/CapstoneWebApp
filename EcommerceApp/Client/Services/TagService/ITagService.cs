using EcommerceApp.Shared.DTOs;
using EcommerceApp.Shared.Models;

namespace EcommerceApp.Client.Services.TagService
{
    public interface ITagService
    {
        public event Action TagsChanged;
        public string Message { get; set; }
        public List<TagDto> Tags { get; set; }
        Task GetAllTagsAsync();
        Task<ServiceResponse<TagDto>> GetTagByIdAsync(Guid tagId);
        Task<ServiceResponse<TagDto>> CreateTagAsync(TagDto tag);
        Task<ServiceResponse<TagDto>> UpdateTagByIdAsync(Guid tagId, TagDto tag);
        Task<ServiceResponse<bool>> DeleteTagByIdAsync(Guid tagId, bool confirmCascadeDelete);
    }
}
