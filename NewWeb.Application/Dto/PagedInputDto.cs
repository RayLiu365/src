using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace NewWeb.Dto
{
    /// <summary>
    /// 支持分页的InputDto
    /// </summary>
    public class PagedInputDto : IPagedResultRequest
    {
        [Range(1, AppConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public PagedInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }
    }
}
